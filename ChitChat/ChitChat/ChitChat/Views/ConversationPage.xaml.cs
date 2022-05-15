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

namespace ChitChat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Username), "username")]
    [QueryProperty(nameof(ContactID), "contactID")]
    public partial class ConversationPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        List<ConversationModel> conversationList = new List<ConversationModel>();

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
            InitializeComponent();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            LoadConversation();
            base.OnAppearing();
        }

        public async void LoadConversation()
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
                    conversationView.ItemsSource = conversationList;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            {
                await Task.Delay(1000);
                IsBusy = false;
                noMessage = false;
            }
            /*
            var document = await CrossCloudFirestore.Current
                .Instance
                .Collection("contacts")
                .Document(contactID)
                .Collection("conversations")
                .GetAsync();
            var model = document.ToObjects<ConversationModel>();
            conversationList = model.ToList();
            conversationView.ItemsSource = conversationList;
            conversationScroll.IsVisible = true;*/
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
            string ID = Guid.NewGuid().ToString();
            ConversationModel conversation = new ConversationModel()
            {
                id = ID,
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
                .Document(ID)
                .SetAsync(conversation);

            Message.Text = "";
        }
    }
}