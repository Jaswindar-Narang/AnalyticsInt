using AnalyticsInt.CommonLayout;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static AnalyticsInt.Entities.ViewModels;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace AnalyticsInt
{
	public partial class App : Application
	{
        public App()
        {
            InitializeComponent();
            NavigationPage nav = new NavigationPage();
          //  MainPage = new LoginPage();
            MainPage = new Subscriptions();
                       MessagingCenter.Subscribe<LoginViewModel, string>(this, "LoginSuccess", (loginModel, args) =>
            {
               

                nav = new NavigationPage(new HomePage());

                nav.BarBackgroundColor = Color.Black;
                nav.BarTextColor = Color.White;

                MainPage = nav;

            });

            MessagingCenter.Subscribe<HomePage, string>(this, "Subscriptions", (sender, args) =>
            {
               

                nav = new NavigationPage(new Subscriptions());

                nav.BarBackgroundColor = Color.Black;
                nav.BarTextColor = Color.White;

                MainPage = nav;
            });
            //MessagingCenter.Subscribe<HomePage, string>(this, "PaymentPageRequest", (loginModel, args) =>
            //{
            //    nav = new NavigationPage(new PaymentPage());

            //    nav.BarBackgroundColor = Color.Black;
            //    nav.BarTextColor = Color.White;

            //    MainPage = nav;
            //});
        }

       

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
