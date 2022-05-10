using ChitChat.DependencyServices;
using ChitChat.Models;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Email.Text = "";
            Password.Text = "";
            /*
            if(dataClass.isSignedIn)
            {
                await Shell.Current.GoToAsync($"//{nameof(ChatPage)}");
            }*/
        }

        private void EmailFocused(object sender, EventArgs e)
        {
            EmailFrame.BorderColor = Color.FromHex("#d8dae3");
        }

        private void PasswordFocused(object sender, EventArgs e)
        {
            PasswordFrame.BorderColor = Color.FromHex("#d8dae3");
        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Email.Text) && string.IsNullOrEmpty(Password.Text))
            {
                PasswordFrame.BorderColor = Color.Red;
                EmailFrame.BorderColor = Color.Red;
                await DisplayAlert("Login Failed", "Your email and password is missing. Please try again.", "OK");
                return;
            }

            if (string.IsNullOrEmpty(Email.Text))
            {
                EmailFrame.BorderColor = Color.Red;
                await DisplayAlert("Login Failed", "Your email is missing. Please try again.", "OK");
                return;
            }

            if (string.IsNullOrEmpty(Password.Text))
            {
                PasswordFrame.BorderColor = Color.Red;
                await DisplayAlert("Login Failed", "Your password is missing. Please try again.", "OK");
                return;
            }

            //If not verified*/
            /*if (email)
            {
                PasswordFrame.BorderColor = Color.Red;
                EmailFrame.BorderColor = Color.Red;
                DisplayAlert("Login Failed", "Your email is not verified. We have sent another verification email.", "OK");
                return;
            }*/

            //setloading == true
            FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
            res = await DependencyService.Get<iFirebaseAuth>().LoginWithEmailPassword(Email.Text, Password.Text);

            if (res.Status == false)
            {
                PasswordFrame.BorderColor = Color.Red;
                EmailFrame.BorderColor = Color.Red;
                await DisplayAlert("Login Failed", res.Response + "Please try again.", "OK");
                return;
            }
            //setloading = false;
            LoginProceed();
        }

        private async void LoginProceed()
        {
            await Shell.Current.GoToAsync($"//{nameof(ChatPage)}");
        }

        private async void SignupClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SignupPage)}");
        }

        private async void ForgotClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ForgotPasswordPage)}");
        }
    }
}