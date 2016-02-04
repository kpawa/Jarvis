using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var authorizationUrl = string.Format("https://home.nest.com/login/oauth2?client_id={0}&state={1}",
    ConfigurationManager.AppSettings["client-id"], "dummy-random-value-for-anti-csfr");

            using (var process = Process.Start(authorizationUrl))
            {
                Console.WriteLine("Awaiting response, please accept on the Works with Nest page to continue");
            }
        }
    }
}
