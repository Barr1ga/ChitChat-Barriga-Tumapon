using ChitChat.DependencyServices;
using ChitChat.Models;
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
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
    
        }

        private void EmailFocused(object sender, EventArgs e)
        {
            EmailFrame.BorderColor = Color.FromHex("#d8dae3");
        }

        private async void SendResetClicked(object sender, EventArgs e)
        {
            var email = Email.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            if (string.IsNullOrEmpty(Email.Text))
            {
                EmailFrame.BorderColor = Color.Red;
                await DisplayAlert("Login Failed", "Your email is missing. Please try again.", "OK");
                return;
            }

            if (!match.Success)
            {
                EmailFrame.BorderColor = Color.Red;
                await DisplayAlert("Sign up Failed", "Your email is badly formatted. Please try again.", "OK");
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

            //setloading = true;
            FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
            res = await DependencyService.Get<iFirebaseAuth>().ResetPassword(Email.Text);

            if (res.Status == false)
            {
                await DisplayAlert("Error", res.Response + ".", "OK");
                return;
            }

            await DisplayAlert("Success", "An email has been sent to your email address.", "OK");
            LoginProceed();
        }

        private async void LoginProceed()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}