using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ChitChat.Droid.DependencyServices;
using ChitChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseAuthService))]
namespace ChitChat.Droid.DependencyServices
{
    internal class FirebaseAuthService : iFirebaseAuth
    {
        Dataclass dataclass = DatabaseModel.GetInstance;
        public FirebaseAuthResponseModel IsLoggedIn()
        {
            try
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = true, Response = "Currently logged in." };
                if(FirebaseAuth.Instance.CurrentUser.Uid == null)
                {

                }
            }
        }
    }
}