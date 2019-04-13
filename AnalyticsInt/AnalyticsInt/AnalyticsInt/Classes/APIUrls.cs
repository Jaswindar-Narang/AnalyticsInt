using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyticsInt.Classes
{
   public class APIUrls
    {
       // http://192.168.1.5:8080/
        #region staging env

        public static string envurl = "http://192.168.0.104:8082/";

        #endregion

        #region FlightUrls
       
        public static string getFlightsArrivalAtAirportUrl = envurl + "GetAllFlightsForAirport";

        public static string getFlightsArrivalAtAirport(string ArrivalAirport, int date, int month, int year)
        {
           // string basse = "GetAllFlightsForAirport?ArrivalAirport=" + ArrivalAirport + "&date=" + date + "&" + month +"=03&year=" + year;
            string apiUrl = getFlightsArrivalAtAirportUrl+ "?ArrivalAirport=" + ArrivalAirport + "&date=" + date + "&month=" + month + "&year=" + year;
            return apiUrl;
        }
        #endregion
    }
}
