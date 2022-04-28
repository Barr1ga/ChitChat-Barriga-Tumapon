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
    public partial class ChatPage : ContentPage
    {

        
        private readonly  List<ContactInfo> contactList = new List<ContactInfo>()
        {
            new ContactInfo { Username = "User_Sample_1", Email = "User_Sample_1@gmail.com" },
            new ContactInfo { Username = "User_Sample_2", Email = "User_Sample_2@gmail.com" },
            new ContactInfo { Username = "User_Sample_3", Email = "User_Sample_3@gmail.com" },
            new ContactInfo { Username = "User_Sample_4", Email = "User_Sample_4@gmail.com" },
            new ContactInfo { Username = "User_Sample_5", Email = "User_Sample_5@gmail.com" },
            new ContactInfo { Username = "User_Sample_6", Email = "User_Sample_6@gmail.com" },
            new ContactInfo { Username = "User_Sample_7", Email = "User_Sample_7@gmail.com" },
            new ContactInfo { Username = "User_Sample_8", Email = "User_Sample_8@gmail.com" },
            new ContactInfo { Username = "User_Sample_9", Email = "User_Sample_9@gmail.com" },
            new ContactInfo { Username = "User_Sample_10", Email = "User_Sample10@gmail.com" }
        };
        public ChatPage()
        {
            InitializeComponent();
            contactView.ItemsSource = contactList;
        }

        private async void SearchPressed(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SearchResult)}?input={ContactsSearchBar.Text}");
        }

        private async void ContactView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var contact = ((ListView)sender).SelectedItem as ContactInfo;
            if(contact == null)
            {
                return;
            }

            await DisplayAlert("error", contact.Username, "ok");
            await Shell.Current.GoToAsync($"/{nameof(ConversationPage)}?name={contact.Username}");
        }
        private void ContactView_ItemTapped(object sender, TappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}