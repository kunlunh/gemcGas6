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
using Microsoft.Data.Sqlite;
using System.Text.Json;
using Microsoft.AspNetCore.Session;


namespace gemcGas.Controllers
{
    public class AdminController:Controller
    {
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
                ViewData["Title"] = "预测预报";
                return View();
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