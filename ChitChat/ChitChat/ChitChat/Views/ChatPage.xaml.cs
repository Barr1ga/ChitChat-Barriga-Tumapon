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
        public ChatPage()
        {
            InitializeComponent();
            List<ContactInfo> contactList = new List<ContactInfo>
            {
                new ContactInfo{Name = "Chiz Beloy", Email = "raychrisbelarmino@gmail.com"},
                new ContactInfo{Name = "Nikolai Tumapon", Email = "franztumapon13@gmail.com"}
            };
            contactView.ItemsSource = contactList;
        }
 
        async void ContactView_ItemSelected(object sender, ItemTappedEventArgs e)
        {
            var contact = e.Item as ContactInfo;
            await Shell.Current.GoToAsync($"/{nameof(ConversationPage)}?name={contact.Name}");
        }
    }
}