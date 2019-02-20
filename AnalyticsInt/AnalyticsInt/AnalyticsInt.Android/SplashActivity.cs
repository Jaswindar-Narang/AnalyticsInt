﻿
using Android.App;
using Android.OS;

namespace AnalyticsInt.Droid
{
    [Activity(Theme = "@style/Theme.Splash", Icon = "@drawable/NewLogo", Label = "AnalyticsInt", MainLauncher = true, NoHistory = true)]
   public class SplashActivity :Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            StartActivity(typeof(MainActivity));
        }
    }
}