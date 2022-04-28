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
    [QueryProperty("ContactUsername", "username")]
    public partial class ConversationPage : ContentPage
    {
        public string _contactUsername;
        public ConversationPage()
        {
            InitializeComponent();
        }
        public string ContactUsername
        {
            get => _contactUsername;
            set => Header.Text = value;
        }

        private void ToggleSendButton(object sender, System.EventArgs e)
        {
            if(Message.Text != "")
            {
                SendButton.Source = "ic_send";
            }else
            {
                SendButton.Source = "ic_send_disabled";
            }
        }
    }
}