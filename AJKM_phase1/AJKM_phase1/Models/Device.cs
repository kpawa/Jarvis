
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace AJKM_phase1.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Device
{

    public int deviceID { get; set; }

    public string category { get; set; }

    public string username { get; set; }

    public string accountID { get; set; }

    public Nullable<int> dataID { get; set; }



    public virtual ProviderAccount ProviderAccount { get; set; }

    public virtual DeviceData DeviceData { get; set; }

}

}
