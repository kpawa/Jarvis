using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace FirebaseSharpClientConsole
{
    public class OAuthController : ApiController
    {
        /// <summary>
        /// Called by Nest when user grants access
        /// </summary>
        /// <param name="state">Anti-cross-site forgery request token</param>
        /// <param name="code">Code to request access token</param>
        /// <returns>Access code</returns>
        public HttpResponseMessage Get(string state, string code)
        {
            if (!string.Equals("SUPERSECRETCODE", state))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var accessToken = GetAccessToken(code);
            Program.SubscribeToNestDeviceDataUpdates(accessToken);

            var response = new HttpResponseMessage(HttpStatusCode.OK) 
            {
                Content = new StringContent("Well done, you now have an access token which allows you to call Nest API on behalf of the user."
                    + "<br />Please return to the console application.") 
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }

        private string GetAccessToken(string authorizationCode)
        {
            var url = string.Format("https://api.home.nest.com/oauth2/access_token?code={0}&client_id={1}&client_secret={2}&grant_type=authorization_code",
                authorizationCode, ConfigurationManager.AppSettings["client-id"], ConfigurationManager.AppSettings["client-secret"]);

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.PostAsync(url, content: null).Result)
                {
                    var accessToken = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                    return (accessToken as dynamic).access_token;
                }
            }
        }
    }
}
