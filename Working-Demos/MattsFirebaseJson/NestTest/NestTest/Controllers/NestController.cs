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
using System.Web;
using Newtonsoft.Json.Linq;

namespace NestTest.Controllers
{
    public class NestController : Controller
    {

        const string CLIENT_ID = "2620b4d6-630f-4951-869a-3f00c66ccd83";
        const string CLIENT_SECRET = "RlhpodZBgYvSvZEJZDbvMuBGM";
        const string STATE = "SOMETHINGSALTY";

        // doesnt work when set these as part of the global class variables
        public string AuthCode { get; set; }
        public string AccessToken { get; set; }
        

        // GET: Nest
        public ActionResult Index()
        {
            return View();
        }

        // GET: Nest/Register
        // should navigate to Nest sign in page so user can grant access to a device
        // once grant permission will send app back to the Redirect URL specified for device
        public void Register()
        {
            var authorizationUrl = string.Format("https://home.nest.com/login/oauth2?client_id={0}&state={1}",
                CLIENT_ID, STATE);
            // want to check if the cookie has been set, if not then go get accesToken
            // will need to store this in DB eventually

            HttpCookie accessTokenCookie = System.Web.HttpContext.Current.Request.Cookies["accessToken"];
            if(accessTokenCookie != null) // if already have token dont make another request to NEST
            {
                return;
            }
            // otherwise make request and wait for response
            using (var process = Process.Start(authorizationUrl))
            {
                Console.WriteLine("Awaiting response, please accept on the Works with Nest page to continue");                  
            }
            WaitForResponse(); // probably a built in function which can do this...
        }

        // need to wait fr local vaiable to be set before can get data
        // need to wait for cookie to be set
        void WaitForResponse()
        {
            // keeps throwing an error
            // HttpContext.Current.Response.Cookies.AllKeys.Contains("myCookie")
            HttpCookie accessTokenCookie = System.Web.HttpContext.Current.Request.Cookies["accessToken"];
            if (accessTokenCookie == null)
            {
                // check for cookie again
                System.Threading.Thread.Sleep(2000);
                WaitForResponse();
            } else
            { // otherwise your good
                return;
            }
            
        }

        // recieves callback from NEST site
        // should set local vairables used to make requests for device data
        // do you always need to go through here to get data?
        // this is where need to make use of the CODE and ACCESS TOKEN
        public void RecieveAuthCode(string state, string code)
        {
            if (!string.Equals(STATE, state))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            // set auth code
            this.AuthCode = code;
            // set the access token
            var accessToken = GetAccessToken(code);
            this.AccessToken = accessToken;

            // create cookie
            HttpCookie accessCookie = new HttpCookie("accessToken");
            accessCookie.Expires = DateTime.Now.AddHours(1); // cookie will expire in an hour
            accessCookie.Value = accessToken;

            // add it to current response   
            System.Web.HttpContext.Current.Response.Cookies.Add(accessCookie);   

            // this is where can send requests for other data, not just Register...
        }

        /// <summary>
        /// Returns a string which is your access token
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <returns></returns>
        private string GetAccessToken(string authorizationCode)
        {
            var url = string.Format("https://api.home.nest.com/oauth2/access_token?code={0}&client_id={1}&client_secret={2}&grant_type=authorization_code",
                authorizationCode, CLIENT_ID, CLIENT_SECRET);

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

        public async Task<JsonResult> GetDeviceData(string accessToken)
        {
            // CREATE A CLIENT WE CAN ACCESS DATA THROUGH
            var url = "https://developer-api.nest.com";
            var fb = new Firebase(url, accessToken);

            
            dynamic devices = await fb.GetAsync("devices");

            var jsonParsed = JsonConvert.DeserializeObject<dynamic>(devices);
            var thermostats = jsonParsed.thermostats;
            string dev_id = jsonParsed["thermostats"]["5TN0NLa65q3XoSjECHNUvI-BBzEt2ynq"].device_id;
            string name = jsonParsed["thermostats"]["5TN0NLa65q3XoSjECHNUvI-BBzEt2ynq"].name_long;
            string curr_temp = jsonParsed["thermostats"]["5TN0NLa65q3XoSjECHNUvI-BBzEt2ynq"].ambient_temperature_c;
            string tar_temp = jsonParsed["thermostats"]["5TN0NLa65q3XoSjECHNUvI-BBzEt2ynq"].target_temperature_c;
            Thermostat myThermostat = new Thermostat(dev_id, name, curr_temp, tar_temp);
            
            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetThermostatData(string accessToken)
        {
            var url = "https://developer-api.nest.com";
            var fb = new Firebase(url, accessToken);

            dynamic devices = await fb.GetAsync("devices/thermostats");

            var jsonParsed = JsonConvert.DeserializeObject<dynamic>(devices);
            string name = jsonParsed["5TN0NLa65q3XoSjECHNUvI-BBzEt2ynq"].name_long;
            string temp = jsonParsed["5TN0NLa65q3XoSjECHNUvI-BBzEt2ynq"].ambient_temperature_f;
            Thermostat myThermostat = new Thermostat(name, temp);

            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetCameraData(string accessToken)
        {
            var url = "https://developer-api.nest.com";
            var fb = new Firebase(url, accessToken);

            dynamic devices = await fb.GetAsync("devices/cameras");
            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetSmokeAlarmData(string accessToken)
        {
            var url = "https://developer-api.nest.com";
            var fb = new Firebase(url, accessToken);

            dynamic devices = await fb.GetAsync("devices/smoke_co_alarms");
            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetThermoStatTemp(string accessToken, string id )
        {
            var url = "https://developer-api.nest.com";
            var fb = new Firebase(url, accessToken);

            dynamic devices = await fb.GetAsync("devices/thermostats/" + id );
            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> SetThermostatTemp(string accessToken, string id, float temp)
        {
            // ambient_temperature_c
            var url = "https://developer-api.nest.com";
            var fb = new Firebase(url, accessToken);
            /*

    string path = "/path";
string data = "{{\"value\": \"Hello!\"}}";
 
string id = fb.Post(path, data);

*/
            string data = "{{\"ambient_tempeture_c\" : \"" + temp + "\"}}";
            dynamic JSON = await fb.PostAsync("devices/thermostats/" + id , data);
            return Json(JSON, JsonRequestBehavior.AllowGet);

        }
        public void SetThermostatName(string accessToken, string id)
        {
            // ambient_temperature_c
            var url = "https://developer-api.nest.com";
            var fb = new Firebase(url, accessToken);
            /*

    string path = "/path";
string data = "{{\"value\": \"Hello!\"}}";
 
string id = fb.Post(path, data);

*/
            string data = "{{\"value\" : \"Jim\"}}";
            fb.PostAsync("devices/thermostats/" + id + "/name_long", data);

        }

    }
}