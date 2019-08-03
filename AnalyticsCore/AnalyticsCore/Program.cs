using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AnalyticsCore.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace AnalyticsCore
{
    public class Program
    {
        //public static readonly string appid = "d60656ee";
        //public static readonly string appKey = "a283cf89995ace4fbf03d5cee06d6dcc";
        public static void Main(string[] args)
        {
           
           // string  apiUrl = "https://api.flightstats.com/flex/schedules/rest/v1/json/to/SYD/arriving/2019/06/22/22?appId=" + appid + "&appKey=" + appKey;

            // For Interval in Minutes 
            // This Scheduler will start at 22:00(set to datetime.now) and call after every 1 Minutes
            // IntervalInSeconds(start_hour, start_minute, minutes)
            FlightAPIScheduler.IntervalInMinutes(DateTime.Now.Hour, DateTime.Now.Minute, 15,
            async () => {

              var x = await GetAllFlightsForAirport(GlobalProperties.apiUrl);
                System.Diagnostics.Debug.WriteLine("//here write the code that you want to schedule");
            });
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        #region FlightAPICalls
        public async static Task<ResponseModel> GetAllFlightsForAirport(string apiUrl)
        {
            if (!Directory.Exists(@"C:\FlightData"))
            {
                Directory.CreateDirectory(@"C:\FlightData");
            }
            string fileName = @"C:\FlightData\FlightsData.txt";
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                
                //need to update 30+ days and days on the basis of months
            //    string month = "0"+DateTime.Now.Month.ToString();
            //string date = (DateTime.Now.Day+1).ToString();

            
            ResponseModel responseModel = new ResponseModel();
           // var req = Request;
            string response = null;

            using (var client = new HttpClient())
            {
                //apiUrl = "https://api.flightstats.com/flex/schedules/rest/v1/json/to/SYD/arriving/"+ GlobalProperties.arrivalYear+ "/"+ GlobalProperties .arrivalMonth+ "/"+ GlobalProperties.arrivalDate+ "/22?appId=" + appid + "&appKey=" + appKey;
                // SetupClient(client, "GET", apiUrl);

                //  response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                // response.EnsureSuccessStatusCode();

                var response1 = client.GetAsync(GlobalProperties.apiUrl).Result;  // Blocking call!  
                if (response1 != null)
                {
                    try
                    {
                        // original
                        var strResult = await response1.Content.ReadAsStringAsync();
                            using (FileStream fs = File.Create(fileName))
                            {
                                // Add some text to file    
                                //Byte[] title = new UTF8Encoding(true).GetBytes("FlightsData-" + DateTime.Now.Date.ToString());
                                //fs.Write(title, 0, title.Length);
                                byte[] author = new UTF8Encoding(true).GetBytes(strResult);
                                fs.Write(author, 0, author.Length);
                            }
                            // 
                            //var strResult =   System.IO.File.ReadAllText(@"c:\AirportFlightsResponse.txt");
                            responseModel.StatusCode = System.Net.HttpStatusCode.OK;
                        responseModel.Result = JObject.Parse(strResult);
                        //responseModel.Result = JsonConvert.SerializeObject(strResult,
                        //            new JsonSerializerSettings()).ToList();

                    }
                    catch (Exception ex)
                    {
                        responseModel.StatusCode = System.Net.HttpStatusCode.NoContent;
                        responseModel.ErrorMessage = ex.Message.ToString();
                        responseModel.Result = null;
                    }
                }

            }
            GlobalProperties.globalResponseModel = responseModel;
            }
            catch (Exception ex)
            {

            }

            return GlobalProperties.globalResponseModel;
        }
        #endregion  
    }
}
