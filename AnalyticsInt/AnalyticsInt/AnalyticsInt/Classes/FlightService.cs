using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AnalyticsInt.Classes
{
    class FlightService
    {
        public async System.Threading.Tasks.Task GetAPIAsync()
        {
            using (var client = new HttpClient())
            {
                var postedData = "{ JSON Data for post }";
                client.BaseAddress = new Uri("url");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.PostAsync("api/values/", postedData);
                    if (response.IsSuccessStatusCode)
                    {
                        // decide if request succeed or not.
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
