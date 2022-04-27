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
            new ContactInfo { Name = "Chiz Beloy", Email = "raychrisbelarmino@gmail.com" },
            new ContactInfo { Name = "Nikolai Tumapon", Email = "franztumapon13@gmail.com" },
            new ContactInfo { Name = "Nikolai Tumapon", Email = "franztumapon13@gmail.com" },
            new ContactInfo { Name = "Nikolai Tumapon", Email = "franztumapon13@gmail.com" },
            new ContactInfo { Name = "Nikolai Tumapon", Email = "franztumapon13@gmail.com" },
            new ContactInfo { Name = "Nikolai Tumapon", Email = "franztumapon13@gmail.com" }
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

        async void ContactView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var contact = e.SelectedItem as ContactInfo;
            await Shell.Current.GoToAsync($"/{nameof(ConversationPage)}?name={contact.Name}");
        }
        private void ContactView_ItemTapped(object sender, TappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}