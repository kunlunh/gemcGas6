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
using Microsoft.Extensions.Options;

namespace gemcGas.Controllers
{
    public class GuideController:Controller
    {
        private readonly AppConfigOptions appoptions;
        public GuideController(IOptions<AppConfigOptions> options)
        {
            appoptions = options.Value;
        }
        public IActionResult Text()
        {
            
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Json(new {message = "no authed"});
            }
            else
            {
                var systemversion = System.Environment.OSVersion.ToString();
                var csharpversion = System.Environment.Version.ToString();
                var versioninfo = System.Environment.MachineName.ToString();
                return new ContentResult
                {
                    Content = "Hi there! From Guide ☺\n"+csharpversion+"@"+systemversion+" on "+versioninfo+" authed",
                    ContentType = "text/plain; charset=utf-8"
                };
            }
            
        }
        
        public IActionResult send(string message)
        {
            //string MSG = message;
            var corp_id = appoptions.WX_corp_id;
            var app_secret = appoptions.WX_app_secret;
            var app_id = appoptions.WX_app_id;
            var postresult = "";
            var Uri = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + corp_id + "&corpsecret=" + app_secret;
            HttpClient httpClient = new HttpClient();
            httpClient.GetAsync(Uri).ContinueWith(
               (requestTask) =>
               {
                   HttpResponseMessage response = requestTask.Result;
                   // 确认响应成功，否则抛出异常
                   response.EnsureSuccessStatusCode();
                   // 异步读取响应为字符串
                   response.Content.ReadAsStringAsync().ContinueWith(
                       (readTask) =>
                       {
                           var qywxres = readTask.Result;
                           string getsring = JsonSerializer.Deserialize<Qywxrespone>(qywxres).access_token;
                           var PostUri = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=" + getsring;

                           HttpClient postClient = new HttpClient();
                           var sendcontent = new Qywsendmessage();
                           var sendmessage = new Qywsendcontent();
                           sendmessage.content = message;
                           sendcontent.agentid = app_id;
                           sendcontent.touser = "@all";
                           sendcontent.msgtype = "text";
                           sendcontent.safe = 0;
                           sendcontent.text = sendmessage;
                           string json = JsonSerializer.Serialize(sendcontent);
                           HttpContent postContent = new StringContent(json);
                           httpClient.PostAsync(PostUri, postContent).ContinueWith(
                                  (postTask) => {
                                      HttpResponseMessage response = postTask.Result;
                                      response.Content.ReadAsStringAsync().ContinueWith(
                                          (readTask) => {
                                              postresult = readTask.Result;
                                          });
                                  });
                       }); 
               
               
               });

            return new ContentResult
            {
                Content = "Hi there! From Guide ☺\n" + message + " sent @" + DateTime.Now + "\n" + postresult,
                ContentType = "text/plain; charset=utf-8"
            };

        }
        
        public ActionResult<test> Getjson()
        {
            var result = new test
            {
                message = "good"
            };
            return result;
        }

        public IActionResult json()
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Json(new {message = "no authed"});
                
            }
            else
            {
                List<useritem> result = new List<useritem>();
                using (var connection = new SqliteConnection("Data Source=z_source.db"))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"SELECT * FROM userinfo";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new useritem() {
                                username= reader.GetString(1), 
                                departname=reader.GetString(2),
                                created=reader.GetString(3)
                            });
                        }
                    }
                    connection.Close();
                }
                return Json(new {message = result});
            }
        }
        
        public IActionResult auth()
        {
            HttpContext.Session.SetInt32("authed",1);
            return Json(new {message = "Authed!"});
        }

        public IActionResult Html()
        {
            var timenow = DateTime.Now;
            return new Microsoft.AspNetCore.Mvc.ContentResult
            {
                Content = "<h1>Hi there! From Guide ☺</h1></br><h2>" + timenow +"</h2>",
                ContentType = "text/html; charset=utf-8"
            };
        }
        
        public IActionResult Index()
        {
            var timenow = DateTime.Now;
            return new Microsoft.AspNetCore.Mvc.ContentResult
            {
                Content = "Hi there! From Guide Index ☺" + "\nServer Time: "+ timenow,
                ContentType = "text/plain; charset=utf-8"
            };
        }
        
        public IActionResult Sqltest()
        {
            var uid = 1;
            var name = "";
            var timenow = DateTime.Now;
            using (var connection = new SqliteConnection("Data Source=z_source.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                        SELECT username
                        FROM userinfo
                        WHERE uid = $uid
                    ";
                command.Parameters.AddWithValue("$uid", uid);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name = reader.GetString(0);
                    }
                }
                connection.Close();
            }
            return new Microsoft.AspNetCore.Mvc.ContentResult
            {
                Content = "Hi there! From Guide Index ☺" + name + "\nServer Time: "+ timenow,
                ContentType = "text/plain; charset=utf-8"
            };
        }
    }
}