gemcGas6 is a ambient air quality analyse system used by the Guangzhou Environment Center.

1. Data collection. (From 50+ stations, on its NO, O3, SO2, CO, PM2.5, PM10 and regular meteorological parameters.)

2. AQI analyse (yearly, monthly, daily, heatmap etc.).

3. Air quality analyse.

More Functions is on devepment.

...

Usage:

add a appsettings.json as :

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "AppConfig": {
    "WX_corp_id": "",
    "WX_app_secret": "",
    "WX_app_id": "",
    "GZCMAconnectEmail": "",
    "GZCMAconnectPassword": "",
    "GZCMAconnectURL": ""
  }
}

```