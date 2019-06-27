using AnalyticsCore.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using static AnalyticsCore.Models.AirportFlightResponseVM;

namespace AnalyticsInt.Classes
{
    public class FlightService
    {
        //public async System.Threading.Tasks.Task GetAPIAsync()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var postedData = "{ JSON Data for post }";
        //        client.BaseAddress = new Uri("url");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        try
        //        {
        //            HttpResponseMessage response = await client.PostAsync("api/values/", postedData);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                // decide if request succeed or not.
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //}


        public async Task<APIResponse<AirportFlightResponseVM>> getFlightsFromAirport(string ArrivalAirport, int date, int month, int year)
        {
            try
            {
                APIResponse<AirportFlightResponseVM> airportFlights = null;
                AirportFlightResponseVM model = new AirportFlightResponseVM();
              //  var dates = Convert.ToInt32(DateTime.UtcNow.Date);
               var result = Task.Run(() => APIHendler.GetData(APIUrls.getFlightsArrivalAtAirport(ArrivalAirport, date, month, year))).Result;
                //var result = await APIHendler.GetData(APIUrls.getFlightsArrivalAtAirport(ArrivalAirport, date, month, year));
                string jsonString = result[0].ToString();
                var airportob = JsonConvert.DeserializeObject<Object>(jsonString);
                var parsed = JObject.Parse(airportob.ToString());
                try
                {
                    var testobj = parsed["result"];
                    // airportFlights = (parsed["result"].ToObject<APIResponse<AirportFlightResponseVM>>());
                    airportFlights = JsonConvert.DeserializeObject<APIResponse<AirportFlightResponseVM>>(jsonString);
                    var newResult = airportFlights;
                    airportFlights.result.scheduledFlights = newResult.result.scheduledFlights.FindAll(o => o.isCodeshare == false);

                }
                catch (Exception EX)
                {
            
                    var ADFASD = JsonConvert.DeserializeObject<APIResponse<AirportFlightResponseVM>>(jsonString);
                }
                if (airportFlights.result.Error != null)
                {
                    airportFlights.statusCode = HttpStatusCode.BadRequest;
                    airportFlights.errorMessage = airportFlights.result.Error.errorMessage;
                   
                }
                return airportFlights;
            }
            catch (Exception EX)
            {
                EX.Message.ToString();
                return null;
            }
        }
       
    }
}
