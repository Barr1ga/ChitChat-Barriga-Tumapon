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
            var email = Email.Text;
            var password = Password.Text;

            if (email == "" || password == "")
            {
                PasswordFrame.BorderColor = Color.Red;
                EmailFrame.BorderColor = Color.Red;
                await DisplayAlert("Login Failed", "Your email or password is missing. Please try again.", "OK");
                return;
            }

            if (email == "")
            {
                EmailFrame.BorderColor = Color.Red;
                await DisplayAlert("Login Failed", "Your email is missing. Please try again.", "OK");
                return;
            }

            if (password == "")
            {
                PasswordFrame.BorderColor = Color.Red;
                await DisplayAlert("Login Failed", "Your password is missing. Please try again.", "OK");
                return;
            }

            /*If not verified*/
            /*if (email)
            {
                PasswordFrame.BorderColor = Color.Red;
                EmailFrame.BorderColor = Color.Red;
                DisplayAlert("Login Failed", "Your email is not verified. We have sent another verification email.", "OK");
                return;
            }*/

            if (email != "admin" && password != "123")
            {
                PasswordFrame.BorderColor = Color.Red;
                EmailFrame.BorderColor = Color.Red;
                await DisplayAlert("Login Failed", "Your email or password is incorrect. Please try again.", "OK");
                return;
            }

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