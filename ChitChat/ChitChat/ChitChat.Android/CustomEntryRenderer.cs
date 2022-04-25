using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ChitChat.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]

namespace ChitChat.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();

                //this line sets the bordercolor
                gd.SetCornerRadius(30f);
                gd.SetStroke(2, Android.Graphics.Color.Gray);
                Control.SetBackgroundColor(global::Android.Graphics.Color.White);
                Control.SetBackground(gd);
                Control.SetPadding(30, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
            }
        }
    }
}