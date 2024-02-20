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
        public string? result_Level { get; set; }
    }

    public class WeatherForecastdatarequest
    {
        public string site { get; set; }
    }
}
