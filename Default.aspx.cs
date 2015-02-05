using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

public partial class Default : System.Web.UI.Page
{
    readonly CookieContainer _cookieJar = new CookieContainer();
     Uri _baseAddress = new Uri("https://portal.vn.teslamotors.com/");
    protected void Page_Load(object sender, EventArgs e)
    {
          
    }


    private async void SetCookie(string username, string password)
    {
        try
        {
            var vehicletime = new Stopwatch();
            vehicletime.Start();
            var address = new Uri("https://portal.vn.teslamotors.com/login");

            var handler = new HttpClientHandler
            {
                CookieContainer = _cookieJar,
                UseCookies = true,
                UseDefaultCredentials = false
            };
            var client = new HttpClient(handler)
            {
                BaseAddress = address
            };
            HttpContent content = new FormUrlEncodedContent(new[]
	{
		new KeyValuePair<string, string>("user_session[email]", username),
		new KeyValuePair<string, string>("user_session[password]", password)
	});
            var response = await client.PostAsync(client.BaseAddress, content);
            vehicletime.Stop();
            Debug.WriteLine("final time authentication taken" + vehicletime.ElapsedMilliseconds);
            GetVehicle();
        }
        catch (Exception ex)
        {
            throw;

        }
    }

    private async void GetVehicle()
    {

        var vehicletime = new Stopwatch();
        vehicletime.Start();
        //Vehicle id and mobile status
        var vehicleStatus = new Dictionary<string, string>();
        var handler = new HttpClientHandler
        {
            CookieContainer = _cookieJar,
            UseCookies = true,
            UseDefaultCredentials = false
        };
        using (var httpClient = new HttpClient(handler) { BaseAddress = _baseAddress })
        {
            List<Vehicles> model;
            using (var response = await httpClient.GetAsync("vehicles"))
            {
                string responseData = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<List<Vehicles>>(responseData);

            }
            if (model.Count > 0)
            {
                foreach (var vehiclese in model)
                {
                    using (var response = await httpClient.GetAsync("vehicles/" + vehiclese.VehicleId + "/mobile_enabled"))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        var mobileEnabled = JsonConvert.DeserializeObject<MobileStatus>(responseData);
                        vehicleStatus.Add(vehiclese.VehicleId, mobileEnabled.Result);
                    }
                }
            }
        }
        vehicletime.Stop();
        Debug.WriteLine("final time vehicle taken" + vehicletime.ElapsedMilliseconds);
            foreach (var vehicle in vehicleStatus)
            {
                var s = new Stopwatch();
                s.Start();
                var downloads = new List<Task<object>>
                {
                    GetClimate(vehicle),
                    GetBatteryStatus(vehicle),
                    GetLocation(vehicle)
                };
                while (downloads.Count > 0)
                {
                    Task<object> finishedTask = await Task.WhenAny(downloads.ToArray());
                    downloads.Remove(finishedTask);
                  
                   var result = finishedTask.Result.GetType();

                        switch (result.Name)
                        {
                            case "BatteryStatus":
                                var battery = finishedTask.Result as BatteryStatus;
                                if (battery != null)
                                {
                                    lblCharging.Text = battery.ChargingState;
                                    lblBatteryLevel.Text = battery.BatteryLevel;
                                    lblRange.Text = battery.BatteryRange;
                                }
                                break;
                            case "ClimateCondition":
                                var climate = finishedTask.Result as ClimateCondition;
                                if (climate != null)
                                {
                                    insideTemp.Text = climate.InsideTemp + " <sup>o</sup>C";
                                    outsideTemp.Text = climate.OutsideTemp + " <sup>o</sup>C";
                                    fanSpeed.Text = climate.FanSpeed;
                                    acStatus.Text = climate.AutoAcCondition.ToString();
                                }
                                break;
                            case "Gps":
                                var gpsLocation = finishedTask.Result as Gps;
                                if (gpsLocation != null)
                                {
                                    Label1.Text = gpsLocation.Latitude;
                                    Label2.Text = gpsLocation.Longitude;
                                }
                                break;
                        }
                   
                 

                  
                }
                s.Stop();
                Debug.WriteLine("final time taken" + s.ElapsedMilliseconds);
            }
     
    }

    public async Task<object> GetBatteryStatus(KeyValuePair<string, string> vehicle)
    {
        var s = new Stopwatch();
        s.Start();
        var handler = new HttpClientHandler
        {
            CookieContainer = _cookieJar,
            UseCookies = true,
            UseDefaultCredentials = false
        };
        using (var httpClient = new HttpClient(handler) { BaseAddress = _baseAddress })
        {
        using (var response = await httpClient.GetAsync("vehicles/" + vehicle.Key + "/command/charge_state"))
        {
            string responseData = await response.Content.ReadAsStringAsync();
            var battery = JsonConvert.DeserializeObject<BatteryStatus>(responseData);
            s.Stop();
            Debug.WriteLine("time taken" + s.ElapsedMilliseconds);
         
            return battery;

        }
        } 

    }

    public async Task<object> GetClimate( KeyValuePair<string, string> vehicle)
    {
        var s = new Stopwatch();
        s.Start();
        var handler = new HttpClientHandler
        {
            CookieContainer = _cookieJar,
            UseCookies = true,
            UseDefaultCredentials = false
        };
        using (var httpClient = new HttpClient(handler) {BaseAddress = _baseAddress})
        {
            using (var response = await httpClient.GetAsync("vehicles/" + vehicle.Key + "/command/climate_state"))
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var climate = JsonConvert.DeserializeObject<ClimateCondition>(responseData);
                s.Stop();
                Debug.WriteLine("time taken" + s.ElapsedMilliseconds);
                return climate;

            }
        } 
         
    }
    public async Task<object> GetLocation( KeyValuePair<string, string> vehicle)
    {
        var s = new Stopwatch();
        s.Start();
        var handler = new HttpClientHandler
        {
            CookieContainer = _cookieJar,
            UseCookies = true,
            UseDefaultCredentials = false
        };
        using (var httpClient = new HttpClient(handler) {BaseAddress = _baseAddress})
        {
            using (var response = await httpClient.GetAsync("vehicles/" + vehicle.Key + "/command/drive_state"))
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var gpsLocation = JsonConvert.DeserializeObject<Gps>(responseData);

                s.Stop();
                Debug.WriteLine("time taken" + s.ElapsedMilliseconds);
                return gpsLocation;

            }
        }
         
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (mockAPI.Checked || string.IsNullOrEmpty(Label3.Text)|| string.IsNullOrEmpty(Label4.Text))
            _baseAddress =new Uri("https://private-anon-b1d05cb0a-timdorr.apiary-mock.com");
        SetCookie(Label3.Text, Label4.Text);
        
    }
}
