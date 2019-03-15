using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticsInt.Classes
{
   public class APIHendler
    {
        static HttpClient client = new HttpClient();
        
        async public static Task<List<string>> GetData(string Str_Uri)
        {
            try
            {
                Uri uristring = new Uri(Str_Uri);
                //client.DefaultRequestHeaders.Add("userguid", Helpers.Settings.UserGuid);
                HttpResponseMessage response = await client.GetAsync(uristring);
                string content = await response.Content.ReadAsStringAsync();
                return await Task.Run(() => new List<string>() { content });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async public static Task<List<string>> PostData(string Str_Uri, string Str_Param)
        {
            try
            {
                // HttpClient client = new HttpClient();
                Uri uristring = new Uri(Str_Uri);
                //client.DefaultRequestHeaders.Add("userguid", Helpers.Settings.UserGuid);
                StringContent stringcontent = new StringContent(Str_Param.Trim(), UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uristring, stringcontent);
                var content = await response.Content.ReadAsStringAsync();
                // var rawjson = new StreamReader(content).ReadToEnd();
                return await Task.Run(() => new List<string>() { content.ToString() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
