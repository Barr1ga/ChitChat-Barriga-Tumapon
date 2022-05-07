using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Plugin.CloudFirestore;
using ChitChat.Droid;
using Xamarin.Forms;
using ChitChat.DependencyServices;
using ChitChat.Helpers;
using ChitChat.Models;

[assembly: Dependency(typeof(FirebaseAuthService))]
namespace ChitChat.Droid
{
    internal class FirebaseAuthService : iFirebaseAuth
    {
        DataClass dataClass = DataClass.GetInstance;
        public FirebaseAuthResponseModel IsLoggedIn()
        {
            try
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = true, Response = "Currently logged in." };
                if (FirebaseAuth.Instance.CurrentUser.Uid == null)
                {
                    response = new FirebaseAuthResponseModel() { Status = false, Response = "Currently logged out." };
                    dataClass.isSignedIn = false;
                    dataClass.loggedInUser = new UserModel();
                }
                else
                {
                    dataClass.loggedInUser = new UserModel()
                    {
                        uid = FirebaseAuth.Instance.CurrentUser.Uid,
                        email = FirebaseAuth.Instance.CurrentUser.Email,
                        name = dataClass.loggedInUser.name,
                        userType = dataClass.loggedInUser.userType,
                        created_at = dataClass.loggedInUser.created_at
                    };
                    dataClass.isSignedIn = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
                dataClass.isSignedIn = false;
                dataClass.loggedInUser = new UserModel();
                return response;
            }
        }

        public Task<FirebaseAuthResponseModel> LoginWithEmailPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<FirebaseAuthResponseModel> ResetPassword(string email)
        {
            throw new NotImplementedException();
        }

        public FirebaseAuthResponseModel SignOut()
        {
            throw new NotImplementedException();
        }

        public Task<FirebaseAuthResponseModel> SignUpWithEmailPassword(string name, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}