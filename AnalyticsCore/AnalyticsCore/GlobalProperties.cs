using AnalyticsCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsCore
{
    public static class GlobalProperties
    {
        static string appid = "e8726d9d";
        static string appKey = "f575e9130154ad85cba6bf85933faa97";

        public static ResponseModel globalResponseModel = new ResponseModel();
        public static string arrivalYear = DateTime.Now.Year.ToString();
        public static string arrivalDate = DateTime.Now.Date.Day.ToString();
        public static string arrivalMonth = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
        public static string apiUrl = "https://api.flightstats.com/flex/schedules/rest/v1/json/to/SYD/arriving/" + GlobalProperties.arrivalYear + "/" + GlobalProperties.arrivalMonth + "/" + GlobalProperties.arrivalDate + "/22?appId=" + appid + "&appKey=" + appKey;

    }
}
