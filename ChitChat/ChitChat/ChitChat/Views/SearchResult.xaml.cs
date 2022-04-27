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
    [QueryProperty("SearchInput", "input")]
    public partial class SearchResult : ContentPage
    {
        public string _searchInput;
        private readonly List<ContactInfo> userList = new List<ContactInfo>()
        {
            new ContactInfo { Name = "Horeb", Email = "horeb@gmail.com" },
            new ContactInfo { Name = "Van AJ", Email = "van@gmail.com" }
        };
         public SearchResult()
        {
            InitializeComponent();


            if (userList.Where(s => s.Email.Equals(SearchInput)) != null)
            {
                usersView.ItemsSource = userList.Where(s => s.Email.Contains(SearchInput));
            }
            else
            {
                DisplayAlert("","User not found.", "OK");
            }
        }

        public string SearchInput
        {
            get => _searchInput;
            set => usersView.ItemsSource = value;
        }


        async void UsersView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await DisplayAlert("A Possible Connection!", "Would you like to add {name}.", "No", "Yes");
        }
    }

}