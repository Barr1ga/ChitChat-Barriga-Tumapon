using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChitChat.Models;
using ChitChat.Helpers;
using Plugin.CloudFirestore;
using System.Collections.ObjectModel;

namespace ChitChat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Username), "username")]
    [QueryProperty(nameof(ContactID), "contactID")]
    public partial class ConversationPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        ObservableCollection<ConversationModel> conversationList = new ObservableCollection<ConversationModel>();

        bool noMessage;
        bool isBusy;
        string username;
        string contactID;

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => username;
            set
            {
                username = value;
                ContactName.Text = value;
                Header.Text = value;
                OnPropertyChanged();
            }
        }

        public string ContactID
        {
            get => contactID;
            set
            {
                contactID = value;
            }
        }

        public bool NoMessage
        {
            get => noMessage;
            set
            {
                noMessage = value;
                OnPropertyChanged();
            }
        }

        public ConversationPage()
        {
            Console.WriteLine("test");
            Console.WriteLine(contactID);
            if (contactID != null)
            {
                LoadConversation();
            }
            InitializeComponent();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            LoadConversation();
            base.OnAppearing();
        }

        public void LoadConversation()
        {
            try
            {
                IsBusy = true;
                CrossCloudFirestore.Current
                .Instance
                .Collection("contacts")
                .Document(contactID)
                .Collection("conversations")
                .OrderBy("created_at", false)
                .AddSnapshotListener((snapshot, error) =>
                {
                    IsBusy = true;
                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            var obj = documentChange.Document.ToObject<ConversationModel>();
                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    conversationList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (conversationList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = conversationList.Where(c => c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (conversationList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = conversationList.Where(c => c.id == obj.id).FirstOrDefault();
                                        conversationList.Remove(item);
                                    }
                                    break;
                            }

                            var conv = conversationView.ItemsSource.Cast<object>().LastOrDefault();
                            conversationView.ScrollTo(conv, ScrollToPosition.End, false);
                        }
                    }

                    NoMessage = conversationList.Count == 0;
                    conversationScroll.IsVisible = !(conversationList.Count == 0);
                });
                
                conversationView.ItemsSource = conversationList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            {
                IsBusy = false;
                noMessage = false;
            }
      
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

        private async void SendMessage(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(Message.Text))
            {
                return;
            }

            ConversationModel conversation = new ConversationModel()
            {
                id = Guid.NewGuid().ToString(),
                converseeID = dataClass.loggedInUser.uid,
                message = Message.Text,
                created_at = DateTime.UtcNow
            };

            //conversation
            await CrossCloudFirestore.Current
                .Instance
                .Collection("contacts")
                .Document(contactID)
                .Collection("conversations")
                .Document(conversation.id)
                .SetAsync(conversation);

            Message.Text = "";
        }
    }
}