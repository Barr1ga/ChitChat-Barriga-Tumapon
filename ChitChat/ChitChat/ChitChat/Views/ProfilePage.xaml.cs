using ChitChat.DependencyServices;
using ChitChat.Helpers;
using ChitChat.Models;
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
    public partial class ProfilePage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        public ProfilePage()
        {
            InitializeComponent();
        }

        private async void LogoutClicked(object sender, EventArgs e)
        {
            FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
            res = DependencyService.Get<iFirebaseAuth>().SignOut();

            if (res.Status == false)
            {
                await DisplayAlert("Error", res.Response + ".", "OK");
                return;
            }

            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}