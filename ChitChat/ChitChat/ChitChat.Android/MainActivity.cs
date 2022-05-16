using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Firebase;
using Xamarin.Forms;

namespace ChitChat.Droid
{
    [Activity(Label = "ChitChat", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            var density = Resources.DisplayMetrics.Density;
            App.screenHeight = Resources.DisplayMetrics.WidthPixels / density;
            App.screenHeight = Resources.DisplayMetrics.HeightPixels / density;

            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Phone)
            {
                App.screenHeight = (16 * App.screenWidth) / 9;
            }

            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Phone)
            {
                App.screenWidth = (9 * App.screenHeight) / 16;
            }

            base.OnCreate(savedInstanceState);
            FirebaseApp.InitializeApp(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}