using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using gemcGas.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Builder.Extensions;
using MySqlConnector;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;


namespace gemcGas.Controllers
{
    public class AdminController:Controller
    {
        private readonly AppConfigOptions appoptions;

        public AdminController(IOptions<AppConfigOptions> options)
        {
            appoptions = options.Value;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                ViewData["username"] = HttpContext.Session.GetString("username");
                ViewData["departname"] = HttpContext.Session.GetString("departname");
                ViewData["timenow"] = DateTime.Now;
                return View();
            }
        }
        
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Aqiheatmap()
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                ViewData["Title"] = "AQI热力图";
                return View();
            }
        }
        public IActionResult Davgdata()
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                ViewData["Title"] = "国控点日均值";
                return View();
            }
        }
        public IActionResult Hourdata()
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                ViewData["Title"] = "小时";
                return View();
            }
        }
        public IActionResult Aqical()
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {

                List<Aqicalresult> aqiResults = new List<Aqicalresult>();
                string MySQLconnectURL = appoptions.MySQLconnectURL;
                using (var connection = new MySqlConnection(MySQLconnectURL))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    if (DateTime.Now.Day == 1) {
                        command.CommandText = @"SELECT PositionName, Date, AQI FROM my_station_day 
                                            WHERE DATE_FORMAT(Date, '%Y%m') = @month 
                                            and PositionName Like '%#%'Order by PositionName ASC";
                        string month = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
                        command.Parameters.AddWithValue("@month", month);
                    }  else
                    {
                        command.CommandText = @"SELECT PositionName, Date, AQI FROM my_station_day 
                                            WHERE DATE_FORMAT(Date, '%Y%m') = @month 
                                            and PositionName Like '%#%'Order by PositionName ASC";
                        string month = DateTime.Now.ToString("yyyyMM");
                        command.Parameters.AddWithValue("@month", month);
                    }
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            aqiResults.Add(new Aqicalresult
                            {
                                PositionName = reader.GetString(0),
                                Date = reader.GetDateTime(1).ToString("yyyy/MM/dd"),                               
                                result_AQI = reader.IsDBNull(2) ? null : reader.GetInt32(2).ToString()
                            });
                        }
                    }
                }
                // Organize the data
                Dictionary<string, AQITable> aqiTable = new Dictionary<string, AQITable>();
                foreach (var result in aqiResults)
                {
                    DateTime date = DateTime.Parse(result.Date);
                    int day = date.Day;
                    int aqi;
                    if (!int.TryParse(result.result_AQI, out aqi))
                    {
                        // Handle the situation where AQI is not a valid integer
                        continue;
                    }

                    if (!aqiTable.ContainsKey(result.PositionName))
                    {
                        aqiTable[result.PositionName] = new AQITable
                        {
                            PositionName = result.PositionName,
                            DailyAQI = new Dictionary<int, int?>()
                        };
                    }
                    aqiTable[result.PositionName].DailyAQI[day] = aqi;
                }
                ViewData["Title"] = "AQI日历";

                // Pass the organized data to the view
                return View(aqiTable.Values.ToList());
                //return View();
            }
        }

        public IActionResult Trendgraph()
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                ViewData["Title"] = "趋势图";
                return View();
            }
        }
        public IActionResult Dayavg()
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                ViewData["Title"] = "日均值";
                return View();
            }
        }

        public IActionResult Monthavg()
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                ViewData["Title"] = "月均值";
                return View();
            }
        }

        public IActionResult AQImap()
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Redirect("/admin/login");
            }
            else
            {
                ViewData["Title"] = "AQI插值图";
                return View();
            }
        }

        public IActionResult WeatherForecast()
        {
            if (false) 
            {
                // HttpContext.Session.GetInt32("authed") != 1
                return Redirect("/admin/login");
            }
            else
            {
                //ViewData["Title"] = "预测预报";
                //return View();
                return Redirect("/admin/WeatherForecastDB");
            }
        }

        public IActionResult WeatherForecastDB()
        {
            if (false)
            {
                // HttpContext.Session.GetInt32("authed") != 1
                return Redirect("/admin/login");
            }
            else
            {
                ViewData["Title"] = "预测预报DB";
                return View();
            }
        }




        //HttpContext.Session.SetInt32("authed",1);
        //return Json(new {message = "Authed!"});


    }
}