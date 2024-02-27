using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gemcGas.Models
{
    public class GasStandard
    {
        public static int SO2_level2_year = 60;
        //GB
        public static int NO2_level2_year = 40;
        public static int CO_level2_day = 4;
        public static int O3_level2_day = 160;
        public static int PM25_level2_year = 35;
        public static int PM10_level2_year = 30;

    }

    public class StationList
    {
        //public string postionitem { get; set; }
        public static List<string> positionList = new List<string> {
           "01# 荔湾西村",
            "02# 海珠宝岗",
            "03# 公园前",
            "04# 天河体育西",
            "05# 越秀麓湖",
            "06# 海珠赤沙",
            "07# 黄埔大沙地",
            "08# 番禺市桥",
            "09# 花都新华",
            "10# 黄埔镇龙",
            "11# 从化良口",
            "12# 从化街口",
            "13# 花都梯面",
            "14# 白云竹料",
            "15# 白云嘉禾",
            "16# 增城荔城",
            "17# 黄埔科学城",
            "18# 番禺大学城",
            "19# 南沙黄阁",
            "20# 南沙街",
            "21# 帽峰山",
            "22# 花都花东",
            "23# 花都赤坭",
            "24# 增城派潭",
            "25# 增城中新",
            "26# 增城石滩",
            "27# 增城新塘",
            "28# 白云江高",
            "29# 白云石井",
            "30# 白云新市",
            "31# 白云山",
            "32# 黄埔永和",
            "33# 黄埔西区",
            "34# 黄埔文冲",
            "35# 凤凰山",
            "36# 天河龙洞",
            "37# 天河五山",
            "38# 天河奥体",
            "39# 荔湾芳村",
            "40# 海珠沙园",
            "41# 海珠湖",
            "42# 番禺大石",
            "43# 番禺南村",
            "44# 番禺亚运城",
            "45# 番禺大夫山",
            "46# 番禺沙湾",
            "47# 南沙榄核",
            "48# 南沙沙螺湾",
            "49# 南沙蒲州",
            "50# 南沙新垦",
            "51# 杨箕路边站",
            "52# 黄沙路边站"
        };
    }



    public class commandMSG
    {
        public string message { get; set; }
    }

    public class aqiheatmaprequest
    {
        public string daycount { get; set; }
    }

    public class aqiheatmapreuslt
    {
        public int site { get; set; }
        public List<int?[]> AQI { get; set; }
     
    }

    public class trenddatarequest
    {
        public string site { get; set; }
        public string daystart { get; set; }
        public string dayend { get; set; }
    }

    public class trenddataresult
    {
        public int? AQI { get; set; }
        public int? Day { get; set; }
        public int? NO2 { get; set; }
        public int? O3 { get; set; }
        public int? PM25 { get; set; }
    }

    public class monthavgdatarequest {
        public string site { get; set; }
    }

    public class dayavgdatarequest
    {
        public string position { get; set; }
        public string month { get; set; }
    }

    public class monthavgdataresult
    {
        public double? gas_general_index { get; set; }
        public string month { get; set; }
        public double? result_CO { get; set; }
        public double? result_CO_95 { get; set; }
        public double? result_NO2 { get; set; }
        public double? result_O3 { get; set; }
        public double? result_O3_90 { get; set; }
        public double? result_PM10 { get; set; }
        public double? result_PM25 { get; set; }
        public double? result_SO2 { get; set; }
        public string result_endday { get; set; }
        public double? success_days_rate { get; set; }
        public double passday { get; set; }
    }

    public class dayavgdataresult
    {
        public string PositionName { get; set; }
        public string Date { get; set; }
        public string? result_CO { get; set; }
        public string? result_NO2 { get; set; }
        public string? result_O3 { get; set; }
        public string? result_PM10 { get; set; }
        public string? result_PM25 { get; set; }
        public string? result_SO2 { get; set; }
        public string? result_AQI { get; set; }
        public string? result_PrimaryPollutant { get; set; }
        public string? result_OverPollutant { get; set; }
        public string? result_Level { get; set; }
    }
    public class hourdatarequest
    {
        public string position { get; set; }
        public string date { get; set; }
    }
    public class hourdataresult
    {
        public string PositionName { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public string? result_CO { get; set; }
        public string? result_NO2 { get; set; }
        public string? result_O3 { get; set; }
        public string? result_PM10 { get; set; }
        public string? result_PM25 { get; set; }
        public string? result_SO2 { get; set; }
    }
    public class davgdatarequest
    {
        public string date { get; set; }
    }
    public class davgdataresult
    {
        public string PositionName { get; set; }
        public string Date { get; set; }
        public string? result_CO { get; set; }
        public string? result_NO2 { get; set; }
        public string? result_O3 { get; set; }
        public string? result_PM10 { get; set; }
        public string? result_PM25 { get; set; }
        public string? result_SO2 { get; set; }
        public string? result_AQI { get; set; }
        public string? result_Level { get; set; }
        public string? PrimaryPollutant { get; set; }
        public string? OverPollutant { get; set; }
    }


    public class WeatherForecastdatarequest
    {
        public string site { get; set; }
    }

    public class Aqicalresult
    {
        public string PositionName { get; set; }
        public string Date { get; set; }
        public string? result_AQI { get; set; }
    }
    public class AQITable
    {
        public string PositionName { get; set; }
        public Dictionary<int, int?> DailyAQI { get; set; }
    }

}
