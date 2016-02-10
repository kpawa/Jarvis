using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Web.Mvc;
using FirebaseSharpClientConsole;
using FirebaseSharp.Portable;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NestTest.ViewModels;
using NestTest.Controllers;

namespace NestTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        // this will send the request to REST api
        public ActionResult Index(List<Thermostat> data)
        {
            //var authorizationUrl = string.Format("https://home.nest.com/login/oauth2?client_id={0}&state={1}",
            //     ConfigurationManager.AppSettings["client-id"], "dummy-random-value-for-anti-csfr");

            // using (var process = Process.Start(authorizationUrl))
            // {
            //     Console.WriteLine("Awaiting response, please accept on the Works with Nest page to continue");
            // }
            NestController nest = new NestController();
            nest.Register();

            var accessToken = System.Web.HttpContext.Current.Request.Cookies["accessToken"].Value;

            ViewBag.AccessToken = accessToken;

            return View();
        }
    }
}