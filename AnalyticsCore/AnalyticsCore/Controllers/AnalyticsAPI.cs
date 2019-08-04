﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AnalyticsCore.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static AnalyticsCore.Models.AirportFlightResponseVM;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnalyticsCore.Controllers
{
    [Route("")]
    [EnableCors("*")]
    public class AnalyticsAPI : Controller
    {
        // GET: api/<controller>
        //[Route("GetFlightsForAirport")]
        //[HttpGet]
        //public IEnumerable<string> GetFlightsForAirport()
        //{
        //    var res = new string[] { "value1", "value2" };
        //}

        [Route("GetFlightsForAirport")]
        [HttpGet]
        public async Task<APIResponse> GetFlightsForAirport(string apiUrl)
        {
            var req = Request;
            APIResponse response = null;
            using (var client = new HttpClient())
            {
               // apiUrl = "https://api.flightstats.com/flex/schedules/rest/v1/json/to/SYD/arriving/2019/06/14/22?appId=" + appid + "&appKey=" + appKey;
                // SetupClient(client, "GET", apiUrl);
                //  response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                // response.EnsureSuccessStatusCode();
                var response1 = client.GetAsync(GlobalProperties.apiUrl).Result;  // Blocking call!  
                if (response1 != null)
                {
                    var customerJsonString = await response1.Content.ReadAsStringAsync();
                    Console.WriteLine("Your response data is: " + customerJsonString);

                    // Deserialise the data (include the Newtonsoft JSON Nuget package if you don't already have it)
                    var deserialized = JsonConvert.DeserializeObject<RootObject>(custome‌​rJsonString);
                    response = new APIResponse(true, "Sucess", deserialized);
                }
               
                //await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                //{
                //    if (x.IsFaulted)
                //        throw x.Exception;
                //    //result.Content = x.Result;
                //    result.Content = new StringContent(x.Result);
                //    //result = JsonConvert.DeserializeObject<AirportFlightResponseVM>(x.Result);
                //});
            }
            return response;
        }

        [Route("GetAllFlightsForAirport")]
        [HttpGet]
       

        public async Task<ResponseModel> GetAllFlightsForAirport(string apiUrl)
        {
            RootObject deserialized = new RootObject() ;
            var req = Request;
            APIResponse response = null;
           // string arrivalYear = DateTime.Now.Year.ToString();
           // string arrivalDate = DateTime.Now.Date.Day.ToString();
           // string arrivalMonth = DateTime.Now.Month.ToString().Length==1?"0"+DateTime.Now.Month.ToString(): DateTime.Now.Month.ToString();
            
            
           // string arrivalCity = usercity;
            //string arrivalAirport = usercityAirportCode;
             
            using (var client = new HttpClient())
            {
               // apiUrl = "https://api.flightstats.com/flex/schedules/rest/v1/json/to/SYD/arriving/"+GlobalProperties.arrivalYear +"/"+ GlobalProperties.arrivalMonth + "/"+ GlobalProperties.arrivalDate + "/22?appId="+ appid + "&appKey="+ appKey;
                // SetupClient(client, "GET", apiUrl);
                //  response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                // response.EnsureSuccessStatusCode();
                var response1 = client.GetAsync(GlobalProperties.apiUrl).Result;  // Blocking call!  
                if (response1 != null)
                {
                    var customerJsonString =  response1.Content.ReadAsStringAsync();
                    Console.WriteLine("Your response data is: " + customerJsonString);

                    // Deserialise the data (include the Newtonsoft JSON Nuget package if you don't already have it)
                    deserialized = JsonConvert.DeserializeObject<RootObject>(custome‌​rJsonString.Result);
                    response = new APIResponse(true, "Sucess", deserialized);
                    GlobalProperties.globalResponseModel.StatusCode = System.Net.HttpStatusCode.OK;
                    GlobalProperties.globalResponseModel.Result = JObject.Parse(custome‌​rJsonString.Result);
                }

                //await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                //{
                //    if (x.IsFaulted)
                //        throw x.Exception;
                //    //result.Content = x.Result;
                //    result.Content = new StringContent(x.Result);
                //    //result = JsonConvert.DeserializeObject<AirportFlightResponseVM>(x.Result);
                //});
            }
            //  return deserialized;
           
            return GlobalProperties.globalResponseModel;
        }

        //[Route("GetAllFlightsForAirport")]
        //[HttpGet]
        //public async Task<ResponseModel> GetAllFlightsForAirport(string apiUrl)
        //{
        //               return  GlobalProperties.globalResponseModel;
        //}
        


        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
