using AnalyticsCore.Models;
using AnalyticsInt.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnalyticsInt.CommonLayout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AirportFlights : ContentPage
    {
        FlightService flightApis = new FlightService();
        // public ObservableCollection<string> Items { get; set; }
        public AirportFlightResponseVM Items { get; set; }

        public AirportFlights()
        {
            InitializeComponent();

            Items = new AirportFlightResponseVM();
            Items = MyAirportFlights();
          var  IteTms =  APIUrls.getFlightsArrivalAtAirport("SYD", 12, 03, 2019);
           // Items = FlightService.getFlightsFromAirport("SYD", 12, 03, 2019).T;
            var RESULT = JObject.Parse(IteTms);
            var result = JsonConvert.DeserializeObject<AirportFlightResponseVM>(IteTms);
           var Ite2ms = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };
			
			MyListView.ItemsSource = Items.scheduledFlights;
        }
        public  AirportFlightResponseVM MyAirportFlights()
        {

            var flightforAirport =  flightApis.getFlightsFromAirport("SYD", 14, 03, 2019);
           // Items = new ObservableCollection<AirportFlightResponseVM>(flightforAirport.Result);
            //Items = 
            return flightforAirport.Result.Result;
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
