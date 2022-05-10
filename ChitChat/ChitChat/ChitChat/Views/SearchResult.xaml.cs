using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChitChat.Models;

namespace ChitChat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResult : ContentPage
    {
        bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        private List<UserModel> Init_List()
        {
            List<UserModel> userList = new List<UserModel>()
              {
                    new UserModel { name = "Horeb", email = "horeb@gmail.com" },
                    new UserModel { name = "Van AJ", email = "van@gmail.com" },
                    new UserModel { name = "CJ", email = "cj@gmail.com" },
                    new UserModel { name = "AJ", email = "aj@gmail.com" },
                    new UserModel { name = "Edwin", email = "edwin@gmail.com" },
                    new UserModel { name = "Chris", email = "chris@gmail.com" },
                    new UserModel { name = "Nikolai", email = "nikolai@gmail.com" },
                    new UserModel { name = "Nina", email = "nina@gmail.com" },
              };

            return userList;
        }
    

         public SearchResult()
        {
            InitializeComponent();
            LoadUsers();
            BindingContext = this;
        }

        public async Task LoadUsers()
        {
            try
            {
                IsBusy = true;
                List<UserModel> userList = Init_List();
                if (userList.Count != 0)
                {
                    usersView.ItemsSource = userList;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                await Task.Delay(1000);
                IsBusy = false;
            }
        }

        async void UsersView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var select = ((ListView)sender).SelectedItem as UserModel;
            if(select == null)
            {
                return;
            }
            await DisplayAlert("Would you like to add", select.name, "No", "Yes");
        }
        private void UsersView_ItemTapped(object sender, TappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                IsBusy = true;
                List<UserModel> userList = Init_List();
                if (userList.Where(s => s.email.Contains(SearchBar.Text)) != null)
                {
                    usersView.ItemsSource = userList.Where(s => s.email.Contains(SearchBar.Text));
                }
                else
                {
                    await DisplayAlert("", "User not found.", "OK");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                await Task.Delay(200);
                IsBusy = false;
            }
            
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            var select = ((ListView)sender).SelectedItem as UserModel;
            if (select == null)
            {
                return;
            }
            await DisplayAlert("Would you like to add", select.name, "No", "Yes");
        }

        /*private async void SearchPressed(object sender, EventArgs e)
        {
            searchList = (List<UserModel>)userList.Where(s => s.email.Contains(SearchBar.Text));
            if (searchList.Count == 0)
            {
                await DisplayAlert("", "User not found.", "OK");
            }
        }*/
    }

}