using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NestTest.Controllers;
using FirebaseSharpClientConsole;

namespace NestTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        // this will send the request to REST api
        public ActionResult Index()
        {
            var authorizationUrl = string.Format("https://home.nest.com/login/oauth2?client_id={0}&state={1}",
                ConfigurationManager.AppSettings["client-id"], "dummy-random-value-for-anti-csfr");

            using (var process = Process.Start(authorizationUrl))
            {
                Console.WriteLine("Awaiting response, please accept on the Works with Nest page to continue");
            }         

            return View();
        }

        // this will be used as my redirect URL
        public ActionResult RecieveAuthorization(string state, string code)
        {
            OAuthController oController = new OAuthController();
            oController.Get(state, code); // this will retugn HTTP RESPONSE METHOD

            return View();
        }
    }
}