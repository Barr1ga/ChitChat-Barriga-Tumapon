using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
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
            UsernameFrame.BorderColor = Color.FromHex("#d8dae3");
        }

        private void EmailFocused(object sender, EventArgs e)
        {
            EmailFrame.BorderColor = Color.FromHex("#d8dae3");
        }

        private void PasswordFocused(object sender, EventArgs e)
        {
            PasswordFrame.BorderColor = Color.FromHex("#d8dae3");
        }

        private void ConfirmPasswordFocused(object sender, EventArgs e)
        {
            ConfirmPasswordFrame.BorderColor = Color.FromHex("#636370");
        }

        private async void SignupClicked(object sender, EventArgs e)
        {
            var email = Email.Text;
            var password = Password.Text;
            var username = Username.Text;
            var confirmPassword = ConfirmPassword.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

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
                await DisplayAlert("Sign up Failed", "Your username, email or password is missing. Please try again.", "OK");
                return;
            }

            if(email != "")
            {
                if (!match.Success)
                {
                    EmailFrame.BorderColor = Color.Red;
                    await DisplayAlert("Sign up Failed", "Your email is badly formatted. Please try again.", "OK");
                    return;
                }
            }

            if (password != "" || confirmPassword != "")
            {
                if (password != confirmPassword)
                {
                    PasswordFrame.BorderColor = Color.Red;
                    ConfirmPasswordFrame.BorderColor = Color.Red;
                    await DisplayAlert("Sign up Failed", "Your passwords do not match. Please try again.", "OK");
                    return;
                }
            }

            await DisplayAlert("Success", "Sign up successful. Verification email sent.", "OK");
            LoginProceed();
        }

        private async void LoginProceed()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
        
    }
}