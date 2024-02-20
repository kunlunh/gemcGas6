namespace gemcGas.Models
{
    public class AppConfigOptions
    {
        public const string AppConfig = "AppConfig";
        public string GZCMAconnectEmail { get; set; } = string.Empty;
        public string GZCMAconnectPassword { get; set; } = string.Empty;
        public string GZCMAconnectURL { get; set; } = string.Empty;
        public string WX_corp_id { get; set; } = string.Empty;
        public string WX_app_secret { get; set; } = string.Empty;
        public string WX_app_id { get; set; } = string.Empty;
        public string PGSQLconnectURL { get; set; } = string.Empty;

        public string MySQLconnectURL { get; set; } = string.Empty;
    }
}
