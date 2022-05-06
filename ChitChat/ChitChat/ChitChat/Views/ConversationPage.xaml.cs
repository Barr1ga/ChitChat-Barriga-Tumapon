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
        public string _id = "1";

        public class ModifiedConversationList
        {
            public string converseeID { get; set; }
            public string message { get; set; }
            public string backgroundColor { get; set; }
            public string margin { get; set; }
            public string imageVisibility { get; set; }
            public string horizontalOption { get; set; }


        }

        private List<ModifiedConversationList> Init_List()
        {
            List<ModifiedConversationList> modifiedConversationList = new List<ModifiedConversationList>();

            List<ConversationModel> conversationList = new List<ConversationModel>
                {
                    new ConversationModel { converseeID = "1", message = "Lorem ipsum dolor sit amet" },
                    new ConversationModel { converseeID = "2", message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                    new ConversationModel { converseeID = "2", message = "Lorem ipsum dolor sit amet" },
                    new ConversationModel { converseeID = "1", message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                    new ConversationModel { converseeID = "1", message = "Lorem ipsum dolor sit amet" },
                };

            conversationList.ForEach((conversation) =>
            {
                if (conversation.converseeID == _id)
                {
                    modifiedConversationList.Add(new ModifiedConversationList
                    {
                        converseeID = conversation.converseeID,
                        message = conversation.message,
                        backgroundColor = "#8c52ff",
                        margin = "30,0,0,3",
                        imageVisibility = "False",
                        horizontalOption = "EndAndExpand",
                    });
                }
                else
                {
                    modifiedConversationList.Add(new ModifiedConversationList
                    {
                        converseeID = conversation.converseeID,
                        message = conversation.message,
                        backgroundColor = "#edeefc",
                        margin = "0,0,80,3",
                        imageVisibility = "True",
                        horizontalOption = "StartAndExpand",
                    });
                }
            });

            return modifiedConversationList;
        }

        public ConversationPage()
        {
            InitializeComponent();
            List<ModifiedConversationList> modifiedConversationList = new List<ModifiedConversationList>();
            modifiedConversationList = Init_List();

            /*modifiedConversationList
                .ForEach((conversation) => Console.WriteLine(conversation.backgroundColor));*/

            conversationView.ItemsSource = modifiedConversationList;
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

        private void SendMessage(object sender, System.EventArgs e)
        {
        }
    }
}