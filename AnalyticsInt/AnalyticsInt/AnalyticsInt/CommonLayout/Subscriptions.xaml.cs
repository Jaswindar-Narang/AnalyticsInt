using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnalyticsInt.CommonLayout
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Subscriptions : ContentPage
	{
		public Subscriptions ()
		{
			InitializeComponent ();
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Send<Subscriptions, string>(this, "PaymentPageRequest", "success");
        }
    }
}