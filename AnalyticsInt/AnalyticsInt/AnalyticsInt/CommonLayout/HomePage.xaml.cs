using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnalyticsInt.CommonLayout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            MessagingCenter.Send<HomePage, string>(this, "Subscriptions", "success");

          //  MessagingCenter.Send<HomePage, string>(this, "PaymentPageRequest", "success");
        }
    }
}