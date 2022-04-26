using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            EmailFrame.BorderColor = Color.FromHex("#cfcfcf");
        }

        private void SendResetClicked(object sender, EventArgs e)
        {
            var email = Email.Text;

            if (email == "")
            {
                EmailFrame.BorderColor = Color.Red;
                DisplayAlert("Login Failed", "Your email is missing. Please try again.", "OK");
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

            DisplayAlert("Success", "An email has been sent to your email address.", "OK");
            LoginProceed();
        }

        private async void LoginProceed()
        {
            await Shell.Current.GoToAsync($"//{nameof(ChatPage)}");
        }
    }
}