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
using ListView = Xamarin.Forms.ListView;

[assembly: ExportRenderer(typeof(ListView), typeof(CustomListViewRenderer))]

namespace ChitChat.Droid
{
    public class CustomListViewRenderer : ListViewRenderer
    {
        public CustomListViewRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.VerticalScrollBarEnabled = false;
            }
        }
    }
}