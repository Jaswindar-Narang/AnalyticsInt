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

namespace AnalyticsInt.Droid
{
    [Activity(Label = "AnalyticsInt", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        LoginViewModel LoginVM = new LoginViewModel();
        public static FirebaseApp app;
        FirebaseAuth auth;
        NavigationPage nav;
        FirebaseOptions options = new FirebaseOptions.Builder()
              .SetApplicationId("1:241544303205:android:e2329da52aad3ca9")
              .SetApiKey("AIzaSyAW-iAByk1LHuG0Zll3u4NiACVIarH2gdk")
              .Build();
        PayPalManager MainManager;
        protected override void OnCreate(Bundle bundle)
        {
            
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            MainManager = new PayPalManager(this);
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            try
            {

            
            MessagingCenter.Subscribe<AirportFlights, string>(this, "ReadFlightSampleData", (sender, args) =>
            {
                //var txtfiles = Assets.Open("AboutAssets.txt");
               // Items = new AirportFlightResponseVM();
               var Items = MyAirportFlights();


            });
            }
            catch (Exception ex)
            {

                throw;
            }
            LoadApplication(new App());
            FlightService flightApis = new FlightService();
            


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

