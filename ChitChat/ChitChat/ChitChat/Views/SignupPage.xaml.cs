using ChitChat.DependencyServices;
using ChitChat.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChitChat.Helpers;

namespace ChitChat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
  
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Username.Text = "";
            Email.Text = "";
            Password.Text = "";
            ConfirmPassword.Text = "";
            /*
            if(dataClass.isSignedIn)
            {
                await Shell.Current.GoToAsync($"//{nameof(ChatPage)}");
            }*/
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

        [Obsolete]
        private async void SignupClicked(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email.Text);
            DataClass dataClass = DataClass.GetInstance;

            if (string.IsNullOrEmpty(Username.Text))
            {
                UsernameFrame.BorderColor = Color.Red;
            }

            if (string.IsNullOrEmpty(Email.Text))
            {
                EmailFrame.BorderColor = Color.Red;
            }

            if (string.IsNullOrEmpty(Password.Text))
            {
                PasswordFrame.BorderColor = Color.Red;
            }

            if (string.IsNullOrEmpty(ConfirmPassword.Text))
            {
                ConfirmPasswordFrame.BorderColor = Color.Red;
            }

            if (string.IsNullOrEmpty(Username.Text) 
                || string.IsNullOrEmpty(Email.Text) 
                || string.IsNullOrEmpty(Password.Text) 
                || string.IsNullOrEmpty(ConfirmPassword.Text))
            {
                await DisplayAlert("Sign up Failed", "Your username, email or password is missing. Please try again.", "OK");
                return;
            }

            if(string.IsNullOrEmpty(Email.Text))
            {
                if (!match.Success)
                {
                    EmailFrame.BorderColor = Color.Red;
                    await DisplayAlert("Sign up Failed", "Your email is badly formatted. Please try again.", "OK");
                    return;
                }
            }

            if (string.IsNullOrEmpty(Password.Text) || string.IsNullOrEmpty(ConfirmPassword.Text))
            {
                if (Password.Text != ConfirmPassword.Text)
                {
                    PasswordFrame.BorderColor = Color.Red;
                    ConfirmPasswordFrame.BorderColor = Color.Red;
                    await DisplayAlert("Sign up Failed", "Your passwords do not match. Please try again.", "OK");
                    return;
                }
            }


            //setloading = true;
            FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
            res = await DependencyService.Get<iFirebaseAuth>().SignUpWithEmailPassword(Username.Text, Email.Text, Password.Text);

            if (res.Status == false)
            {
                await DisplayAlert("Error", res.Response + "Please try again.", "OK");
            }

            try
            {
                await CrossCloudFirestore.Current
                    .Instance
                    .GetCollection("users")
                    .GetDocument(dataClass.loggedInUser.uid)
                    .SetDataAsync(dataClass.loggedInUser);

                await DisplayAlert("Success", res.Response + ".", "OK");
            } catch (Exception ex)
            {
                await DisplayAlert("Error", "Sign up successful. Verification email sent.", "OK");
            }

            //setloading = false
            //await DisplayAlert("Success", "Sign up successful. Verification email sent.", "OK");
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