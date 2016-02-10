using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NestTest.ViewModels
{
    public class Thermostat
    {
        public string Current_Temperature { get; set; }
        public string Target_Temperature { get; set; }
        public string Name_Long { get; set; }
        public string Device_Id { get; set; }
        public Thermostat()
        {

        }
        public Thermostat(string tempeture)
        {
            this.Current_Temperature = tempeture;
        }
        public Thermostat(string name, string tempeture)
        {
            this.Current_Temperature = tempeture;
            this.Name_Long = name;
        }

        public Thermostat(string device_id, string name, string curr_temp, string tar_temp)
        {
            this.Device_Id = device_id;
            this.Current_Temperature = curr_temp;
            this.Target_Temperature = tar_temp;
            this.Name_Long = name;
        }

    }
}