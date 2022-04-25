using Android.Graphics.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChitChat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
      
        private void LoginClicked(object sender, EventArgs e)
        {
            var email = Email.Text;
            var password = Password.Text;
            

            if (email == "admin" && password == "123")
            {
                LoginProceed();
            } else
            {
                DisplayAlert("Login Failed", "Your email or password is incorrect. Please try again.", "OK");
                
            }

            if (email == "" || password == "")
            {
                DisplayAlert("Login Failed", "Your email or password is missing. Please try again.", "OK");
                
            }
        }

        private async void LoginProceed()
        {
            await Shell.Current.GoToAsync($"//{nameof(ChatPage)}");
        }

        private async void SignupClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SignupPage)}");
        }


    }
}