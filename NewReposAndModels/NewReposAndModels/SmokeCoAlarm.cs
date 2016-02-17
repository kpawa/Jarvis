﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AJKM_phase1.ViewModels
{
    public enum Alarm_State { ok, warning, emergency }
    public class SmokeCoAlarm
    {
        public Alarm_State Smoke_Alarm_State { get; set; }
        public Alarm_State Co_Alarm_State { get; set; }
        public bool Is_Online { get; set; }
        public string Name_Long { get; set; }
        public string Device_Id { get; set; }
        public SmokeCoAlarm() { }

        public SmokeCoAlarm(string device_id, string name, bool is_online, Alarm_State smoke_alarm_state, Alarm_State co_alarm_state)
        {
            Device_Id = device_id;
            Is_Online = is_online;
            Smoke_Alarm_State = smoke_alarm_state;
            Co_Alarm_State = co_alarm_state;
            Name_Long = name;
        }
        public override string ToString()
        {
            return "Device Id: " + Device_Id + ", Name: " + Name_Long + ", Online: " + Is_Online + ", Smoke Alarm State: " + Smoke_Alarm_State + ", Co Alarm State: " + Co_Alarm_State;
        }
    }
}