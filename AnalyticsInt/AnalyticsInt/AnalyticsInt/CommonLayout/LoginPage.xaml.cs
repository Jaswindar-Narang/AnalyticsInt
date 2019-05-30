using AnalyticsInt.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static AnalyticsInt.Entities.ViewModels;

namespace AnalyticsInt.CommonLayout
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        NavigationPage nav = new NavigationPage();
        FlightService flightApis = new FlightService();

		public LoginPage ()
		{
			InitializeComponent ();
		}

        private void btn_forget_Clicked(object sender, EventArgs e)
        {
            myAPICALL();
            //var newres= await Task.Run(()=> flightApis.getFlightsFromAirport("SYD", 04, 03, 2019)));

        }

        public void myAPICALL()
        {
            var flightforAirport =  flightApis.getFlightsFromAirport("SYD", 14, 03, 2019);
        }

        private void login_img_Clicked(object sender, EventArgs e)
        {
            LoginViewModel LoginVM = new LoginViewModel();
            LoginVM.Email = txt_email.Text.ToString();
            LoginVM.Password = txt_pass.Text.ToString();
            MessagingCenter.Send<LoginViewModel, string>(LoginVM, "Login", "login");
            // StartActivity(new Android.Content.Intent(this, typeof(HomePage)));
            //MessagingCenter.Subscribe<LoginViewModel, string>(this, "LoginSuccess", (loginModel, args) =>
            //{
            //    nav = new NavigationPage(new HomePage());

            //    nav.BarBackgroundColor = Color.Black;
            //    nav.BarTextColor = Color.White;

            //    //MainPage = nav;

            //});
        }

        private void SignUp_img_Clicked(object sender, EventArgs e)
        {
            Entities.ViewModels.LoginViewModel LoginVM = new Entities.ViewModels.LoginViewModel();
            LoginVM.Email = txt_email.Text.ToString();
            LoginVM.Password = txt_pass.Text.ToString();
            MessagingCenter.Send<Entities.ViewModels.LoginViewModel, string>(LoginVM, "Register", "login");
        }
    }
}