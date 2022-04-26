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
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private void UsernameFocused(object sender, EventArgs e)
        {
            EmailFrame.BorderColor = Color.FromHex("#cfcfcf");
        }

        private void EmailFocused(object sender, EventArgs e)
        {
            EmailFrame.BorderColor = Color.FromHex("#cfcfcf");
        }

        private void PasswordFocused(object sender, EventArgs e)
        {
            PasswordFrame.BorderColor = Color.FromHex("#cfcfcf");
        }

        private void ConfirmPasswordFocused(object sender, EventArgs e)
        {
            ConfirmPasswordFrame.BorderColor = Color.FromHex("#cfcfcf");
        }

        private void SignupClicked(object sender, EventArgs e)
        {
            var email = Email.Text;
            var password = Password.Text;
            var username = Username.Text;
            var confirmPassword = ConfirmPassword.Text;
            /*Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);*/

            if (username == "")
            {
                UsernameFrame.BorderColor = Color.Red;
            }

            if (email == "")
            {
                EmailFrame.BorderColor = Color.Red;
            }

            if (password == "")
            {
                PasswordFrame.BorderColor = Color.Red;
            }

            if (confirmPassword == "")
            {
                ConfirmPasswordFrame.BorderColor = Color.Red;
            }

            if (username == "" || email == "" || password == "" || confirmPassword == "")
            {
                DisplayAlert("Sign up Failed", "Your username, email or password is missing. Please try again.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                PasswordFrame.BorderColor = Color.Red;
                ConfirmPasswordFrame.BorderColor = Color.Red;
                DisplayAlert("Sign up Failed", "Your passwords do not match. Please try again.", "OK");
                return;
            }

            /*Check if exmail exists*/
            /*if ()
            {
                DisplayAlert("Sign up Failed", "This email already exists. Please try again.", "OK");
                return;
            }*/
            DisplayAlert("Success", "Sign up successful. Verification email sent.", "OK");
            LoginProceed();
        }

        private async void LoginProceed()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"/{nameof(LoginPage)}");
        }
        
    }
}