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
        public ObservableCollection<ScheduledFlight> _getAirportFlights = new ObservableCollection<ScheduledFlight>();

        FlightService flightApis = new FlightService();
        // public ObservableCollection<string> Items { get; set; }
        public AirportFlightResponseVM Items { get; set; }

        public AirportFlights()
        {
            try
            {

            InitializeComponent();

            Items = new AirportFlightResponseVM();
            //Items = MyAirportFlights();
           // MessagingCenter.Send<AirportFlights, string>(this, "ReadFlightSampleData", (sender, args) =>

             //MessagingCenter.Send(this, "ReadFlightSampleData", "success");
            //MessagingCenter.Send(this, "testData", "success");

          //  var IteTms =  APIUrls.getFlightsArrivalAtAirport("SYD", 12, 03, 2019);
             Items = flightApis.getFlightsFromAirport("SYD", 18, 06, 2019).Result.result;
                // var RESULT = JObject.Parse(Items);
                //  var result = JsonConvert.DeserializeObject<AirportFlightResponseVM>(Items.ToString());
                //var Ite2ms = new ObservableCollection<string>
                // {
                //     "Item 1",
                //     "Item 2",
                //     "Item 3",
                //     "Item 4",
                //     "Item 5"
                // };
         //       _getAirportFlights = new ObservableCollection<ScheduledFlight>(Items.scheduledFlights);

                MyListView.ItemsSource = Items.scheduledFlights;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public  AirportFlightResponseVM MyAirportFlights()
        {

            var flightforAirport =  flightApis.getFlightsFromAirport("SYD", 14, 03, 2019);
           // Items = new ObservableCollection<AirportFlightResponseVM>(flightforAirport.Result);
            //Items = 
            return flightforAirport.Result.result;

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
