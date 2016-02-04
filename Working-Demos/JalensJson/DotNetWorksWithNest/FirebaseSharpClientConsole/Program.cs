﻿using FirebaseSharp.Portable;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseSharpClientConsole
{
    class Program
    {
        private static string _accessToken;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Started.");

                using (WebApp.Start<Startup>(url: "http://localhost:9000/"))
                {
                    GetAccessToken();
                    Console.ReadLine();
                }

                SubscribeToNestDeviceDataUpdates();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        internal static void SubscribeToNestDeviceDataUpdates(string accessToken)
        {
            _accessToken = accessToken;

            Console.WriteLine("Press any key to start listening for Nest Device data updates.");
        }

        private static void SubscribeToNestDeviceDataUpdates()
        {
            var firebaseClient = new Firebase("https://developer-api.nest.com", _accessToken);
            var response = firebaseClient.GetStreaming("devices",
                changed: (s, args) => UpdatedItem(args)
                    /*changed: (s, e) => {
                        if (e.Path.EndsWith("name"))
                            Console.WriteLine("Name: {0}.", e.Data);
                        if (e.Path.Contains("last_connection"))
                            Console.WriteLine("Last connection: {0}.", e.Data);
                        if (e.Path.Contains("ambient_temperature_f"))
                            Console.WriteLine("Current temperature: {0}.", e.Data);
                        if (e.Path.Contains("target_temperature_f"))
                            Console.WriteLine("Target temperature: {0}.", e.Data);
                        if (e.Path.Contains("humidity"))
                            Console.WriteLine("Humidity: {0}.", e.Data);
                        if (e.Path.Contains("hvac_state"))
                            Console.WriteLine("Current mode: {0}.", e.Data);
                        
                    }*/
        );

            Console.WriteLine("Change the current temperature of the Nest Thermostat in the Nest Developer Chrome Extension to see the real-time updates.");
        }

        public class Thermostat{
            public string name { get; set; }
            public string last_connection { get; set; }
            public string ambient_temperature_f { get; set; }
            public string target_temperature_f { get; set; }
            public string Humidity { get; set; }
            public string hvac_state { get; set; }

            public Thermostat() { }

        }

        private static void UpdatedItem(ValueChangedEventArgs args)
        {

            Console.WriteLine(args.Path + ": " + args.OldData + ": " + args.Data);
        }

        private static void GetAccessToken()
        {
            Console.WriteLine("Press any key to browse to the Works with Nest page, to allow access to Nest device data.");
            Console.ReadLine();

            var authorizationUrl = string.Format("https://home.nest.com/login/oauth2?client_id={0}&state={1}",
                ConfigurationManager.AppSettings["client-id"], "SUPERSECRETCODE");

            using (var process = Process.Start(authorizationUrl))
            {
                Console.WriteLine("Awaiting response, please accept on the Works with Nest page to continue.");
            }
        }
    }
}