using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using AnalyticsInt.CommonLayout;
using Firebase;
using Firebase.Auth;
using Android.Support.Design.Widget;
using Android.Gms.Tasks;
using static Android.Views.View;
using static AnalyticsInt.Entities.ViewModels;
using System.IO;
using AnalyticsCore.Models;
using AnalyticsInt.Classes;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AppCenter.Push;
using Android.Content;
using Android.Support.V4.App;
using System.Linq;

namespace AnalyticsInt.Droid
{
    [Activity(Label = "AnalyticsInt", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        // Notification- Unique ID for  notification: 
        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";

        // Number of times the button is tapped (starts with first tap):
        int count = 1;

        LoginViewModel LoginVM = new LoginViewModel();
        public static FirebaseApp app;
        FirebaseAuth auth;
        NavigationPage nav;
        FirebaseOptions options = new FirebaseOptions.Builder()
              .SetApplicationId("1:241544303205:android:e2329da52aad3ca9")
              .SetApiKey("AIzaSyAW-iAByk1LHuG0Zll3u4NiACVIarH2gdk")
              .Build();
        PayPalManager MainManager;
        FlightService flightApis = new FlightService();


        public AirportFlightResponseVM Items { get; private set; }
      //  public FlightService flightApis { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            MainManager = new PayPalManager(this);
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            try
            {
                //Push.SetSenderId("241544303205");
                Items = new AirportFlightResponseVM();
                //    flightApis = new FlightService();
               // Items = flightApis.getFlightsFromAirport("SYD", 15, 04, 2019).Result.result;
                MessagingCenter.Subscribe<AirportFlights, string>(this, "ReadFlightSampleData", (sender, args) =>
                {
                    //var txtfiles = Assets.Open("AboutAssets.txt");
                    // Items = new AirportFlightResponseVM();
                    var Items = MyAirportFlights();
                });
                //CreateNotificationChannel(Items);
                // Display the "Hello World, Click Me!" button and register its event handler:
                //NotificationButtonOnClick(Items);
            }
            catch (Exception ex)
            {
                throw;
            }
            void CreateNotificationChannel(AirportFlightResponseVM model)
            {
                if (Build.VERSION.SdkInt < BuildVersionCodes.O)
                {
                    // Notification channels are new in API 26 (and not a part of the
                    // support library). There is no need to create a notification 
                    // channel on older versions of Android.
                    return;
                }

                var name = model.scheduledFlights[0].arrivalAirportFsCode;
                var description = model.scheduledFlights[0].flightNumber;
                var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
                {
                    Description = description
                };

                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(channel);

            }
            void NotificationButtonOnClick(AirportFlightResponseVM model)
            {
                // Pass the current button press count value to the next activity:
                var valuesForActivity = new Bundle();
                valuesForActivity.PutInt(COUNT_KEY, count);

                // When the user clicks the notification, SecondActivity will start up.
                var resultIntent = new Intent(this, typeof(AppNotification));

                // Pass some values to SecondActivity:
                resultIntent.PutExtras(valuesForActivity);

                // Construct a back stack for cross-task navigation:
                var stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
                //stackBuilder.AddParentStack(Class.FromType(typeof(AppNotification)));
                stackBuilder.AddNextIntent(resultIntent);

                // Create the PendingIntent with the back stack:            
                var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);
                for (int i = 0; i <= 5; i++)
                {
                    // Build the notification:
                    var builder = new NotificationCompat.Builder(this, CHANNEL_ID)
                                  .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                                  .SetContentIntent(resultPendingIntent) // Start up this activity when the user clicks the intent.
                                  .SetContentTitle($" {model.scheduledFlights[i].arrivalTime.Hour.ToString()} : {model.scheduledFlights[0].arrivalTime.Minute.ToString()}") // Set the title
                                  .SetNumber(count) // Display the count in the Content Info
                                  .SetSmallIcon(Resource.Drawable.flightpic) // This is the icon to display
                                  .SetContentText($"{model.appendix.airports.Where(o=>o.cityCode== model.scheduledFlights[i].departureAirportFsCode).FirstOrDefault().city.ToString()} At Terminal {model.scheduledFlights[0].arrivalTerminal}"); // the message to display.

                    // Finally, publish the notification:
                    var notificationManager = NotificationManagerCompat.From(this);
                    notificationManager.Notify(NOTIFICATION_ID+i, builder.Build());
                }
                // Increment the button press count:
                count++;
            }
            
            MessagingCenter.Subscribe<LoginViewModel, string>(this, "Login", (sender, args) =>
            {
                LoginVM = new LoginViewModel();
                LoginVM.Email = sender.Email.ToString();
                LoginVM.Password =sender.Password.ToString();
                Toast.MakeText(this,"asdfs",ToastLength.Short);
                InitFirebaseAuth(LoginVM,"LoginRequest");

            });
            MessagingCenter.Subscribe<LoginViewModel, string>(this, "Register", (sender, args) =>
            {
                LoginVM = new LoginViewModel();
                LoginVM.Email = sender.Email.ToString();
                LoginVM.Password = sender.Password.ToString();
                Toast.MakeText(this, "asdfs", ToastLength.Short);
                InitFirebaseAuth(LoginVM, "UserRegistrationRequest");

            });
            MessagingCenter.Subscribe<HomePage, string>(this, "PaymentPageRequest", (sender, args) =>
            {
                MainManager.BuySomething();
            });
            MessagingCenter.Subscribe<Subscriptions, string>(this, "PaymentPageRequest", (sender, args) =>
            {
                MainManager.BuySomething();
            });
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            MainManager.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnDestroy()
        {
            MainManager.Destroy();
            base.OnDestroy();
        }
        private void InitFirebaseAuth(LoginViewModel LoginModel,string RequestType)
        {//working for testing
            //var options = new FirebaseOptions.Builder()
            //   .SetApplicationId("AIzaSyDDkjTIE-LQMNCfPOwzR8kX0-IPENxl_xY")
            //   .SetApiKey("AIzaSyBmuAwrNEgENM40rnjUToHHMraFXQOyuPE")
            //   .Build();

           
            if (app == null)
                app = FirebaseApp.InitializeApp(this, options);
            auth = FirebaseAuth.GetInstance(app);
            if (RequestType == "LoginRequest")
            {
                LoginUser(LoginModel.Email, LoginModel.Password);
            }
            else if (RequestType == "UserRegistrationRequest")
            {
                SignUpUser(LoginModel.Email, LoginModel.Password);
            }
        }
        private void LoginUser(string email, string password)
        {
            auth.SignInWithEmailAndPassword(email, password).AddOnCompleteListener(this);
        }
        private void SignUpUser(string email, string password)
        {
            auth.CreateUserWithEmailAndPassword(email, password).AddOnCompleteListener(this, this);
        }
    
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
           
               MessagingCenter.Send<LoginViewModel, string>(LoginVM, "LoginSuccess", "success");
               // Finish();
            }
            else
            {
              //  Snackbar snackbar = Snackbar.Make(activity_main, "Login Failed ", Snackbar.LengthShort);
               // snackbar.Show();
            }
        }

        public void OnClick(Android.Views.View v)
        {
            throw new NotImplementedException();
        }
        public AirportFlightResponseVM MyAirportFlights()
        {
            Task<APIResponse<AirportFlightResponseVM>> flightforAirport = null;
            FlightService flightApis = new FlightService();
            var task = System.Threading.Tasks.Task.Run( async() =>
            {//to continue on this point until the execution completes
                flightforAirport = flightApis.getFlightsFromAirport("SYD", 14, 03, 2019);

            });

            while (task.Status != TaskStatus.RanToCompletion)
            {
                Console.WriteLine("Thread ID: {0}, Status: {1}", Thread.CurrentThread.ManagedThreadId, task.Status);
                flightforAirport = flightApis.getFlightsFromAirport("SYD", 14, 03, 2019);
            }
            // Items = new ObservableCollection<AirportFlightResponseVM>(flightforAirport.Result);
            //Items = 
            return flightforAirport.Result.result;
        }
    }
}

