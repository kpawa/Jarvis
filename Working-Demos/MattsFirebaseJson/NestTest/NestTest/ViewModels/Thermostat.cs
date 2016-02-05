using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NestTest.ViewModels
{
    public class Thermostat
    {
        public string Temperature { get; set; }
        public Thermostat()
        {

        }
        public Thermostat(string tempeture)
        {
            this.Temperature = tempeture;
        }


    }
}