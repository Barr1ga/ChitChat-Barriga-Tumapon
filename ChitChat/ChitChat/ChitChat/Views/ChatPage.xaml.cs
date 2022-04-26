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
                new ContactInfo{Name = "Ray Beloy", Email = "raychrisbelarmino@gmail.com"}
            };
            contactView.ItemsSource = contactList;
        }
    }
}