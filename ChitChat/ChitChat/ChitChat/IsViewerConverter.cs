using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ChitChat.Helpers;
using ChitChat.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ChitChat
{
    public class IsViewerConverter : IValueConverter
    {
        DataClass dataClass = DataClass.GetInstance;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool retval = false;
            if (value != null)
            {
                ConversationModel conversation = value as ConversationModel;

                if (conversation.converseeID.Equals(dataClass.loggedInUser.uid))
                {
                    retval = true;
                }

            }
            return retval;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}