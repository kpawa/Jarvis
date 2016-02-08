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

        // this will be used as my redirect URL
        public ActionResult RecieveAuthorization(string state, string code)
        {
            OAuthController oController = new OAuthController();
            oController.Get(state, code); // this will retugn HTTP RESPONSE METHOD

            return View();
        }
        
        // this accepts response from WEB API 
        public async Task<JsonResult> Get(string state, string code)
        {
            if (!string.Equals("dummy-random-value-for-anti-csfr", state))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            // this creates an access token we can use to get data, may want to cache this 
            //var accessToken = GetAccessToken(code);
            var accessToken = "c.EvTrrKWjwCoxrxDVaQrWI4siSt3FKMNEyCVx2PvnLvQQO6pHvWTytPvIRbKPM0MICFAFCddGWOFtzlFGy8UQ09K8O8p3LQ0SAObzAh2UHLHGKx97QEPZpasLCGVitKyxo2u3uCwhDuhnjYLR";
            // this will generate JSON for a particular item
            dynamic JSON = await GetJSON(accessToken);
            return JSON; // spits out JSON WE CAN USE
        }

        /// <summary>
        /// Returns a string which is your access token
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <returns></returns>
        private string GetAccessToken(string authorizationCode)
        {
            var url = string.Format("https://api.home.nest.com/oauth2/access_token?code={0}&client_id={1}&client_secret={2}&grant_type=authorization_code",
                authorizationCode, ConfigurationManager.AppSettings["client-id"], ConfigurationManager.AppSettings["client-secret"]);

            // opens up a client behind the scenes?
            // what is httpClient
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.PostAsync(url, content: null).Result)
                {
                    var accessToken = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                    return (accessToken as dynamic).access_token;
                }
            }
        }

        public async Task<JsonResult> GetJSON(string _accessToken)
        {
            // CREATE A CLIENT WE CAN ACCESS DATA THROUGH
            var url = "https://developer-api.nest.com";
            var fb = new Firebase(url, _accessToken);

            var devices = await fb.GetAsync("devices");
            var uri = fb.RootUri;
         
            var client = fb.ToString();
            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        // this was trying to do it using a live stream of data...
        // so was consantly making requests
        // NOT USED RIGHT NOW
        public async Task<JsonResult> GetDeviceJSON(string _accessToken, List<Thermostat> data )
        {
            GetJSON(_accessToken);
            // MAKE THE LSIT
            var deviceData = new List<Thermostat>();
            // this creates a firebase client
            // this is not a page actually, this is connected to the HOME DEVICE MANAGER APP THAT YOU CAN CHANGE STATE IN
            var firebaseClient = new Firebase("https://developer-api.nest.com", _accessToken); // maybe dont need to stream it???

            
            var response = await firebaseClient.GetStreamingAsync("devices",
                    changed: (s, e) =>
                    { // this is called everytime you change the state of of a device using the NEST simulator APP
                        
                        if (e.Path.Contains("ambient_temperature_f"))
                        {
                            deviceData.Add(new Thermostat(e.Data));
                        }

                        if (e.Path.Contains("hvac_state"))
                        {
                            // at the end of the lst         
                            // firebaseClient.GetAsync(e.Data);                                                                            
                        }
                    }); 
            return Json(data);
        }
    }
}