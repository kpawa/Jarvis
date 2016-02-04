using Owin;
using System.Web.Http;

namespace FirebaseSharpClientConsole
{
    /// <summary>
    /// Configures Web API for self-hosting
    /// 
    /// For more details see http://www.asp.net/web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }
    } 
}
