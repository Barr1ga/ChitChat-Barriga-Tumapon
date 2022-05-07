﻿using System;
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

        private List<ContactModel> Init_List()
        {         
                List<ContactModel> contactList = new List<ContactModel>
                {
                    new ContactModel { contactName = "User_Sample_1", contactEmail = "User_Sample_1@gmail.com" },
                    new ContactModel { contactName = "User_Sample_2", contactEmail = "User_Sample_2@gmail.com" },
                    new ContactModel { contactName = "User_Sample_3", contactEmail = "User_Sample_3@gmail.com" },
                    new ContactModel { contactName = "User_Sample_4", contactEmail = "User_Sample_4@gmail.com" },
                    new ContactModel { contactName = "User_Sample_5", contactEmail = "User_Sample_5@gmail.com" },
                    new ContactModel { contactName = "User_Sample_6", contactEmail = "User_Sample_6@gmail.com" },
                    new ContactModel { contactName = "User_Sample_7", contactEmail = "User_Sample_7@gmail.com" },
                    new ContactModel { contactName = "User_Sample_8", contactEmail = "User_Sample_8@gmail.com" },
                    new ContactModel { contactName = "User_Sample_9", contactEmail = "User_Sample_9@gmail.com" },
                    new ContactModel { contactName = "User_Sample_10", contactEmail = "User_Sample10@gmail.com" }
                };

            return contactList;
        }

        public ChatPage()
        {
            InitializeComponent();
            LoadContacts();
            BindingContext = this;
        }

        private async Task LoadContacts()
        {
            try
            {
                IsBusy = true;
                List<ContactModel> contactList = Init_List();
                if(contactList.Count != 0) 
                {
                    contactView.ItemsSource = contactList;
                }
                else
                {
                    noContact = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                await Task.Delay(1000);
                IsBusy = false;
                noContact = false;
            }
        }

        private async void ContactView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var contact = ((ListView)sender).SelectedItem as ContactModel;
            if(contact == null)
            {
                return;
            }

            await Shell.Current.GoToAsync($"/{nameof(ConversationPage)}?username={contact.contactName}");
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