using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

/// <summary>
/// Summary description for Response
/// </summary>
[DataContract]
public class Vehicles
{
    [DataMember(Name = "color")]
    public string Color { get; set; }
    [DataMember(Name = "display_name")]
    public string DisplayName { get; set; }
    [DataMember(Name = "id")]
    public string Id { get; set; }
    [DataMember(Name = "option_codes")]
    public string OptionCodes { get; set; }
    [DataMember(Name = "user_id")]
    public string UserId { get; set; }
    [DataMember(Name = "vehicle_id")]
    public string VehicleId { get; set; }
    [DataMember(Name = "vin")]
    public string Vin { get; set; }
}

[DataContract]
public class MobileStatus
{
    [DataMember(Name = "reason")]
    public string Reason { get; set; }
    [DataMember(Name = "result")]
    public string Result { get; set; }
}

[DataContract]
public class ClimateCondition
{
    [DataMember(Name = "inside_temp")]
    public string InsideTemp { get; set; }
    [DataMember(Name = "outside_temp")]
    public string OutsideTemp { get; set; }
    [DataMember(Name = "fan_status")]
    public string FanSpeed { get; set; }
    [DataMember(Name = "is_auto_conditioning_on")]
    public bool AutoAcCondition { get; set; }

}

[DataContract]
public class BatteryStatus
{
    [DataMember(Name = "charging_state")]
    public string ChargingState { get; set; }
    [DataMember(Name = "battery_level")]
    public string BatteryLevel { get; set; }
    [DataMember(Name = "fast_charger_present")]
    public string SuperCharger { get; set; }
    [DataMember(Name = "est_battery_range")]
    public string BatteryRange { get; set; }

}
[DataContract]
public class Gps
{
    [DataMember(Name = "latitude")]
    public string Latitude { get; set; }
    [DataMember(Name = "longitude")]
    public string Longitude { get; set; }
}
