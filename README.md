gemcGas6

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