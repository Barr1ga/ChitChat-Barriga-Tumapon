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
        private List<ContactInfo> contactList = new List<ContactInfo>()
        {
            new ContactInfo { Name = "Chiz Beloy", Email = "raychrisbelarmino@gmail.com" },
            new ContactInfo { Name = "Nikolai Tumapon", Email = "franztumapon13@gmail.com" }
        };
        public ChatPage()
        {
            InitializeComponent();
            contactView.ItemsSource = contactList;
        }
 
        //private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
       // {
            //contactView.ItemsSource = contactList.Where(s => s.Email.Contains(e.NewTextValue));
       // }

        private void SearchPressed(object sender, System.EventArgs e)
        {
            if ((contactView.ItemsSource = contactList.Where(s => s.Email.Contains(ContactsSearchBar.Text))) != null)
            {
                contactView.ItemsSource = contactList.Where(s => s.Email.Contains(ContactsSearchBar.Text));
            }
            else
            {
                DisplayAlert(" ", "User not found.", "OKAY");
            }
            
        }

        async void ContactView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var contact = e.SelectedItem as ContactInfo;
            await Shell.Current.GoToAsync($"/{nameof(ConversationPage)}");
        }
    }
}