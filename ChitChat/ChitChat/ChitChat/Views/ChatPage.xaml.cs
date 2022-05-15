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
    public partial class ChatPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        List<ContactModel> contactList = new List<ContactModel>();
        bool noContact;
        bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public bool NoContact
        {
            get => noContact;
            set
            {
                noContact = value;
                OnPropertyChanged();
            }
        }

        public ChatPage()
        {
            InitializeComponent();
            LoadContacts();
            BindingContext = this;
        }

        private async void LoadContacts()
        {
            try
            {
                IsBusy = true;
                CrossCloudFirestore.Current
                    .Instance
                    .Collection("contacts")
                    .WhereArrayContains("contactID", dataClass.loggedInUser.uid)
                    .AddSnapshotListener((snapshot, error) =>
                    {
                        IsBusy = true;
                        if (snapshot != null)
                        {
                            foreach (var documentChange in snapshot.DocumentChanges)
                            {
                                var obj = documentChange.Document.ToObject<ContactModel>();
                                Console.WriteLine("test");
                                switch (documentChange.Type)
                                {
                                    case DocumentChangeType.Added:
                                        contactList.Add(obj);
                                        break;
                                    case DocumentChangeType.Modified:
                                        if (contactList.Where(c => c.id == obj.id).Any())
                                        {
                                            var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                            item = obj;
                                        }
                                        break;
                                     case DocumentChangeType.Removed:
                                        if (contactList.Where(c => c.id == obj.id).Any())
                                        {
                                            var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                            contactList.Remove(item);
                                        }
                                    break;
                                }
                            }
                         }
                         
                        IsBusy = false;
                        contactView.ItemsSource = contactList;
                        NoContact = contactList.Count == 0;
                        contactScroll.IsVisible = !(contactList.Count == 0);
                    });
            }
            catch
            {
                throw;
            }
            {
                await Task.Delay(1000);
                IsBusy = false;
                noContact = false;
            }
        }

        private async void ContactView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ContactModel contact = ((ListView)sender).SelectedItem as ContactModel;
            if(contact == null)
            {
                return;
            }

            string contactName;
            if (dataClass.loggedInUser.uid == contact.contactName[0])
            {
                contactName = contact.contactName[0];
            }
            else
            {
                contactName = contact.contactName[1];
            }
            
            string id = contact.id;
            await Shell.Current.GoToAsync($"/{nameof(ConversationPage)}?username={contactName}&contactID={id}");
        }

        private void ContactView_ItemTapped(object sender, TappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        async void SearchBar_Focused(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SearchResult)}");
        }
    }
}