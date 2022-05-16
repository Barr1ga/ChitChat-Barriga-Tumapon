using System;
using System.Collections.Generic;
using System.Text;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using AndroidX.AppCompat.App;
using ChitChat.Droid;

namespace ChitChat
{
    [Activity(Label = "ChitChat", Icon = "@mipmap/ic_launcher", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(MainActivity));
        }
    }
}
