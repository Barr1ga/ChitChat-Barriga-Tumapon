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
          private readonly List<ContactInfo> userList = new List<ContactInfo>()
            {
                new ContactInfo { Username = "Horeb", Email = "horeb@gmail.com" },
                new ContactInfo { Username = "Van AJ", Email = "van@gmail.com" },
                new ContactInfo { Username = "CJ", Email = "cj@gmail.com" },
                new ContactInfo { Username = "AJ", Email = "aj@gmail.com" },
                new ContactInfo { Username = "Edwin", Email = "edwin@gmail.com" },
                new ContactInfo { Username = "Chris", Email = "chris@gmail.com" },
                new ContactInfo { Username = "Nikolai", Email = "nikolai@gmail.com" },
                new ContactInfo { Username = "Nina", Email = "nina@gmail.com" },
            };

         public SearchResult()
        {
            InitializeComponent();
            usersView.ItemsSource = userList;
        }

        async void UsersView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var select = ((ListView)sender).SelectedItem as ContactInfo;
            if(select == null)
            {
                return;
            }
            await DisplayAlert("Would you like to add", select.Username, "No", "Yes");
        }
        private void UsersView_ItemTapped(object sender, TappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((usersView.ItemsSource = userList.Where(s => s.Email.Contains(SearchBar.Text))) != null)
            {
                usersView.ItemsSource = userList.Where(s => s.Email.Contains(SearchBar.Text));
            }
            else
            {
                await DisplayAlert("", "User not found.", "OK");
            }
        }
    }

}