using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using FirebaseSharp.Portable;
using AJKM_phase1.ViewModels;
using System.Threading.Tasks;

namespace AJKM_phase1.Models
{
    public class ThermostatRepo
    {
        const string ACCESS_TOKEN = "c.gfUxq6YcnYOwVJ5yO1OaxfjWRZ7PDphyWXLfx6S7AqVMtPNNifIAY5SCn8d3C9Gw6ch7HsdJGBE6g7uSx1B2LSKidx67y3GDbmkzXCIvE0sZC7UK9AE29a34InT1KHNz2XN8aEKAeNYELvS0";
        public async Task<Thermostat> GetThermostat()
        {
            var url = "https://developer-api.nest.com";
            var fb = new Firebase(url, ACCESS_TOKEN);
            dynamic devices = await fb.GetAsync("devices");

            var jsonParsed = JsonConvert.DeserializeObject<dynamic>(devices);
            var thermostats = jsonParsed.thermostats;
            string dev_id = jsonParsed["thermostats"]["5TN0NLa65q3XoSjECHNUvI-BBzEt2ynq"].device_id;
            string name = jsonParsed["thermostats"]["5TN0NLa65q3XoSjECHNUvI-BBzEt2ynq"].name_long;
            string curr_temp = jsonParsed["thermostats"]["5TN0NLa65q3XoSjECHNUvI-BBzEt2ynq"].ambient_temperature_c;
            string tar_temp = jsonParsed["thermostats"]["5TN0NLa65q3XoSjECHNUvI-BBzEt2ynq"].target_temperature_c;
            Thermostat myThermostat = new Thermostat(dev_id, name, curr_temp, tar_temp);
            return myThermostat;
        }
    }
}