using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using gemcGas.Models;
using gemcGas.Common;
using System.Net;
using System.Xml;
using Microsoft.Extensions.Options;
using Npgsql;
using MySql.Data.MySqlClient;
using System.Security.Policy;

namespace gemcGas.Controllers
{
    public class ActionController : Controller
    {
        private readonly AppConfigOptions appoptions;
        public ActionController(IOptions<AppConfigOptions> options)
        {
            appoptions = options.Value;
        }

        [HttpPost]
        public IActionResult doLogin(useritem user)
        {

            if (user.username != null)
            {
                var userfind = new useritem();
                using (var connection = new SqliteConnection("Data Source=z_source.db"))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"SELECT * FROM userinfo WHERE username = $username";
                    command.Parameters.AddWithValue("username", user.username);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userfind.username = reader.GetString(1);
                            userfind.departname = reader.GetString(2);
                            userfind.created = reader.GetString(3);
                            userfind.password = reader.GetString(4);
                        }
                    }
                    connection.Close();
                }

                if (userfind.password == user.password)
                {
                    HttpContext.Session.SetInt32("authed", 1);
                    HttpContext.Session.SetString("username", userfind.username);
                    HttpContext.Session.SetString("departname", userfind.departname);
                    var result = new commandMSG
                    {
                        message = "登录成功"
                    };
                    return Json(new { result });
                }
                else
                {
                    var result = new commandMSG
                    {
                        message = "登录失败"
                    };
                    return Json(new { result });
                }


            }
            else
            {
                var result = new commandMSG
                {
                    message = "No Input"
                };
                return Json(new { result });
            }
        }

        [HttpPost]
        public IActionResult get_aqiheatmapdata(aqiheatmaprequest request)
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                if (request.daycount != null)
                {
                    List<aqiheatmapreuslt> result_all = new List<aqiheatmapreuslt> ();
                    int[] check_sites = { 9, 8, 10, 7, 4, 1, 6, 2, 5, 3, 4401 };
                    using (var connection = new SqliteConnection("Data Source=Gas2020.db"))
                    {
                        connection.Open();
                        foreach (int SameSiteMode in check_sites) {
                            aqiheatmapreuslt aqiheatmap_result = new aqiheatmapreuslt();                            
                            var command = connection.CreateCommand();
                            command.CommandText = @"select AQI,day from DayS where site == $site order by day desc limit $check_days";
                            command.Parameters.AddWithValue("check_days", request.daycount);
                            command.Parameters.AddWithValue("site", SameSiteMode.ToString());
                            //Console.WriteLine(command.CommandText);
                            aqiheatmap_result.site = SameSiteMode;
                            aqiheatmap_result.AQI = new List<int?[]> { };
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int?[] AQI_list = { reader.IsDBNull(0) ? (int?)null : reader.GetInt32(0), reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1) };
                                    aqiheatmap_result.AQI.Add(AQI_list);     
                                }
                            }
                            result_all.Add(aqiheatmap_result);
                        }                        
                        connection.Close();
                        //string json = JsonSerializer.Serialize(result_all);
                        //Console.WriteLine(json);
                        return Json(result_all);
                    }
                }
                else
                {
                    var result = new commandMSG
                    {
                        message = "No Input"
                    };
                    return Json(new { result });
                }

            }

        }

        [HttpPost]
        public IActionResult get_trenddata(trenddatarequest request)
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                if (request.site != null)
                {
                    List<trenddataresult> result_all = new List<trenddataresult>();
                    string daystart = request.daystart + "01";
                    string dayend = request.dayend + "31";
                    string site = request.site;
                    using (var connection = new SqliteConnection("Data Source=Gas2020.db"))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = @"select Day,AQI,O3,NO2,PM25 from days where site == $site AND day >= $daystart AND day <= $dayend";
                        command.Parameters.AddWithValue("site", site);
                        command.Parameters.AddWithValue("daystart", daystart);
                        command.Parameters.AddWithValue("dayend", dayend);
                        //Console.WriteLine(command.CommandText);
                        //Console.WriteLine($"select Day,AQI,O3,NO2,PM25 from days where site =={site} AND day >= {daystart} AND day <= {dayend}");
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                trenddataresult trenddata_result = new trenddataresult();
                                trenddata_result.AQI = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1);
                                trenddata_result.Day = reader.IsDBNull(0) ? (int?)null : reader.GetInt32(0);
                                trenddata_result.NO2 = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);
                                trenddata_result.O3 = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                                trenddata_result.PM25 = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4);
                                result_all.Add(trenddata_result);
                            }
                        }                       
                        connection.Close();
                        //string json = JsonSerializer.Serialize(result_all);
                        //Console.WriteLine(json);
                        return Json(result_all);
                    }
                }
                else
                {
                    var result = new commandMSG
                    {
                        message = "No Input"
                    };
                    return Json(new { result });
                }

            }

        }
        [HttpPost]
        public IActionResult get_monthavgdata(monthavgdatarequest request)
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                if (request.site != null)
                {
                    DateTime startt = DateTime.Now;
                    List<monthavgdataresult> result_all = new List<monthavgdataresult>();
                    string site = request.site;
                    string start = "20120101";
                    //string start = "20150101";
                    string end = "20201201";
                    DateTime daystart = DateTime.ParseExact(start, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime dayend = DateTime.ParseExact(end, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    IEnumerable<string> month_list = CommonFunctions.DateRange(daystart, dayend);                                  
                      
                    using (var connection = new SqliteConnection("Data Source=Gas2020.db"))
                    {
                        connection.Open();
                        foreach (string month in month_list)
                        {
                            var command = connection.CreateCommand();
                            monthavgdataresult dayavgdata_result = new monthavgdataresult();
                            dayavgdata_result.month = month;
                            command.CommandText = @"select avg(NO2),avg(SO2),avg(O3),avg(PM25),avg(PM10),avg(CO) from DayS where day like $month AND site == $site";
                            command.Parameters.AddWithValue("month", month+"%");
                            command.Parameters.AddWithValue("site", site);
                            //Console.WriteLine($"select avg(NO2),avg(SO2),avg(O3),avg(PM25),avg(PM10),avg(CO) from DayS where day like '{month}%' AND site == {site}");
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    dayavgdata_result.result_NO2 = reader.IsDBNull(0) ? (double?)null : Math.Round(reader.GetDouble(0),2);
                                    dayavgdata_result.result_SO2 = reader.IsDBNull(1) ? (double?)null : Math.Round(reader.GetDouble(1),2);
                                    dayavgdata_result.result_O3 = reader.IsDBNull(2) ? (double?)null : Math.Round(reader.GetDouble(2),2);
                                    dayavgdata_result.result_PM25 = reader.IsDBNull(3) ? (double?)null : Math.Round(reader.GetDouble(3),2);
                                    dayavgdata_result.result_PM10 = reader.IsDBNull(4) ? (double?)null : Math.Round(reader.GetDouble(4),2);
                                    dayavgdata_result.result_CO = reader.IsDBNull(5) ? (double?)null : Math.Round(reader.GetDouble(5),2);
                                }
                            }
                            var o3_90_sql = connection.CreateCommand();
                            o3_90_sql.CommandText = @"SELECT O3 FROM DayS WHERE day like $month AND Site == $site ORDER BY O3 ASC LIMIT 1 OFFSET (SELECT COUNT(*) FROM DayS WHERE day like $month AND Site == $site) * 9 / 10 - 1";
                            o3_90_sql.Parameters.AddWithValue("month", month+"%");
                            o3_90_sql.Parameters.AddWithValue("site", site);
                            using (var reader = o3_90_sql.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    dayavgdata_result.result_O3_90 = reader.IsDBNull(0) ? (double?)null : Math.Round(reader.GetDouble(0),2);
                                }
                            }
                            
                            var co_95_sql = connection.CreateCommand();
                            co_95_sql.CommandText = @"SELECT CO FROM DayS WHERE day like $month AND Site == $site ORDER BY O3 ASC LIMIT 1 OFFSET (SELECT COUNT(*) FROM DayS WHERE day like $month AND Site == $site) * 95 / 100 - 1";
                            co_95_sql.Parameters.AddWithValue("month", month+"%");
                            co_95_sql.Parameters.AddWithValue("site", site);
                            using (var reader = co_95_sql.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    dayavgdata_result.result_CO_95 = reader.IsDBNull(0) ? (double?)null : Math.Round(reader.GetDouble(0),2);
                                }
                            }
                            
                            var last_day_sql = connection.CreateCommand();
                            last_day_sql.CommandText = @"select day from DayS where day like $month order by day desc limit 1";
                            last_day_sql.Parameters.AddWithValue("month", month+"%");
                            last_day_sql.Parameters.AddWithValue("site", site);
                            using (var reader = last_day_sql.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    dayavgdata_result.result_endday = reader.GetString(0);
                                    dayavgdata_result.passday = Double.Parse(dayavgdata_result.result_endday.Substring(6,2));
                                }
                            }
                            var success_days_sql = connection.CreateCommand();
                            success_days_sql.CommandText = @"SELECT count(AQI) FROM DayS WHERE day like $month AND site = $site AND AQI <= 100";
                            success_days_sql.Parameters.AddWithValue("month", month+"%");
                            success_days_sql.Parameters.AddWithValue("site", site);
                            using (var reader = success_days_sql.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    double Success_days = reader.GetDouble(0);
                                    dayavgdata_result.success_days_rate = Math.Round((Success_days/dayavgdata_result.passday) ,4);
                                }
                            }
                            result_all.Add(dayavgdata_result);
                            
                        }
                        connection.Close();

                    }
                    string json = JsonSerializer.Serialize(result_all);
                    Console.WriteLine(json);
                    DateTime endt = DateTime.Now;
                    TimeSpan passt = endt - startt;
                    Console.WriteLine($"{passt.Seconds}s+{passt.Milliseconds}ms");
                    return Json(result_all);
                }
                else
                {
                    var result = new commandMSG
                    {
                        message = "No Input"
                    };
                    return Json(new { result });
                }

            }

        }
        [HttpPost]
        public IActionResult get_dayavgdata(dayavgdatarequest request)
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                if (request.position != null)
                {
                    DateTime startt = DateTime.Now;
                    string MySQLconnectURL = appoptions.MySQLconnectURL;
                    string position = request.position;
                    string month = request.month;
                    Console.WriteLine(position+"|"+month);
                    List<dayavgdataresult> result_all = new List<dayavgdataresult>();

                    using (var connection = new MySqlConnection(MySQLconnectURL))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = @"select * from my_aqi_day where DATE_FORMAT(Date, '%Y%m') = @month AND PositionName = @position";
                        command.Parameters.AddWithValue("@month", month);
                        command.Parameters.AddWithValue("@position", position);

                        using (var reader = command.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {
                                dayavgdataresult dayavgdata_result = new dayavgdataresult();
                                dayavgdata_result.PositionName = reader.GetString(4);
                                dayavgdata_result.Date = reader.GetDateTime(3).ToString("yyyy/MM/dd");
                                dayavgdata_result.result_SO2 = reader.IsDBNull(5) ? null : reader.GetDouble(5).ToString();
                                dayavgdata_result.result_PM25 = reader.IsDBNull(6) ? null : reader.GetDouble(6).ToString(); 
                                dayavgdata_result.result_PM10 = reader.IsDBNull(7) ? null : reader.GetDouble(7).ToString();
                                dayavgdata_result.result_O3 = reader.IsDBNull(8) ? null : reader.GetDouble(8).ToString();
                                dayavgdata_result.result_NO2 = reader.IsDBNull(9) ? null : reader.GetDouble(9).ToString();
                                dayavgdata_result.result_CO = reader.IsDBNull(10) ? null : reader.GetDouble(10).ToString("0.0");
                                dayavgdata_result.result_AQI = reader.IsDBNull(11) ? null : reader.GetDouble(11).ToString();
                                dayavgdata_result.result_PrimaryPollutant = reader.IsDBNull(12) ? null : reader.GetString(12);
                                dayavgdata_result.result_Level = reader.IsDBNull(13) ? null : reader.GetString(13);
                                result_all.Add(dayavgdata_result);
                            }
                            
                        }



                        connection.Close();

                    }
                    string jsonReuslt = JsonSerializer.Serialize(result_all);
                    Console.WriteLine(jsonReuslt);
                    DateTime endt = DateTime.Now;
                    TimeSpan passt = endt - startt;
                    Console.WriteLine($"{passt.Seconds}s+{passt.Milliseconds}ms");
                    return Json(result_all);
                }
                else
                {
                    var result = new commandMSG
                    {
                        message = "No Input"
                    };
                    return Json(new { result });
                }

            }

        }
        [HttpPost]
        public IActionResult get_dbforecastdata(WeatherForecastdatarequest request)
        {
            if (false) //HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                if (request.site != null)
                {
                    string site = request.site;
                    DateTime startt = DateTime.Now;
                    List<WeatherForcastbyCMA> result_all = new List<WeatherForcastbyCMA>();
                    string jsontext = string.Empty;
                    List<WeatherForcastbyCMA> weatherForecast = new List<WeatherForcastbyCMA>();
                    string PGSQLconnectURL = appoptions.PGSQLconnectURL;

                    using (var connection = new NpgsqlConnection(PGSQLconnectURL))
                    {
                        connection.Open();
                        using (var cmd = new NpgsqlCommand("select * from public.WeatherForcastfromLatest_VIEW", connection))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetString(0) == site)
                                    {
                                        Console.WriteLine(reader.GetString(0));
                                        WeatherForcastbyCMA byforcast = new WeatherForcastbyCMA();
                                        byforcast.AREA = reader.GetString(0);
                                        byforcast.FTIME = reader.GetInt32(1);
                                        byforcast.MINT = reader.GetInt32(2);
                                        byforcast.MAXT = reader.GetInt32(3);
                                        byforcast.MINRH2M = reader.GetString(4);
                                        byforcast.MAXRH2M = reader.GetString(5);
                                        byforcast.RAIN = reader.GetInt32(6);
                                        byforcast.F12WEATHER = reader.GetString(7);
                                        byforcast.F12WINDD = reader.GetString(8);
                                        byforcast.F12WINDS = reader.GetString(9);
                                        byforcast.L12WEATHER = reader.GetString(10);
                                        byforcast.L12WINDD = reader.GetString(11);
                                        byforcast.L12WINDS = reader.GetString(12);
                                        byforcast.RTIME = reader.GetString(13);
                                        result_all.Add(byforcast);
                                    }

                                }

                            }
                                
                        }
                        connection.Close();
                    }
                    string json = JsonSerializer.Serialize(result_all);
                    Console.WriteLine(json);
                    DateTime endt = DateTime.Now;
                    TimeSpan passt = endt - startt;
                    Console.WriteLine(request.site);
                    //Console.WriteLine(weatherForecast);
                    Console.WriteLine($"{passt.Seconds}s+{passt.Milliseconds}ms");
                    return Json(result_all);
                }
                else
                {
                    var result = new commandMSG
                    {
                        message = "No Input"
                    };
                    return Json(new { result });
                }

            }

        }

        [HttpPost]
        public IActionResult get_forecastdata(WeatherForecastdatarequest request)
        {
            if (false) //HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                if (request.site != null)
                {
                    string site = request.site;
                    Console.WriteLine(site);
                    DateTime startt = DateTime.Now;
                    List<WeatherForcastbyCMA> result_all = new List<WeatherForcastbyCMA>();
                    string jsontext = string.Empty;
                    List<WeatherForcastbyCMA> weatherForecast = new List<WeatherForcastbyCMA>();
                    string CMAconnectionEmail = appoptions.GZCMAconnectEmail;
                    string CMAconnectionPassword = appoptions.GZCMAconnectPassword;
                    string CMAconnectionURL = appoptions.GZCMAconnectURL;
                    var jsonpayload = "{ \"interfaceId\":\"SEVENDAYSFORECAST\",\"account\":\""+ CMAconnectionEmail+ "\",\"pwd\":\"" + CMAconnectionPassword + "\"}";

                    var Uri = CMAconnectionURL + jsonpayload;
                    HttpWebRequest getrequest = (HttpWebRequest)WebRequest.Create(Uri);
                    using (HttpWebResponse response = (HttpWebResponse)getrequest.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string xml = reader.ReadToEnd();
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(xml);
                        XmlNodeList name = doc.GetElementsByTagName("ax21:content");
                        jsontext = name[0].InnerText;
                        //Console.WriteLine(jsontext);

                    }
                    weatherForecast = JsonSerializer.Deserialize<List<WeatherForcastbyCMA>>(jsontext);
                    foreach (WeatherForcastbyCMA byforcast in weatherForecast) {
                        if(byforcast.AREA == site)
                        {
                            Console.WriteLine(site);
                            result_all.Add(byforcast);
                        }
                        
                    }
                    string json = JsonSerializer.Serialize(result_all);
                    Console.WriteLine(json);
                    DateTime endt = DateTime.Now;
                    TimeSpan passt = endt - startt;
                    Console.WriteLine(request.site);
                    //Console.WriteLine(weatherForecast);
                    Console.WriteLine($"{passt.Seconds}s+{passt.Milliseconds}ms");
                    return Json(result_all);
                }
                else
                {
                    var result = new commandMSG
                    {
                        message = "No Input"
                    };
                    return Json(new { result });
                }

            }

        }
    }
}
