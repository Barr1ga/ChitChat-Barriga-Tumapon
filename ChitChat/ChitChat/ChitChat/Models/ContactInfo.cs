using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ChitChat.Models
{
    class ContactInfo : INotifyPropertyChanged
    {

        public string Id
        {
            get
            { return Id; }
            set
            {
                Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Username { 
            get
            {   return Username;    } 
            set { 
                Username = value;
                OnPropertyChanged(nameof(Username));
            } 
        }

        public string Email
        {
            get
            {   return Email;   }
            set
            {
                Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get
            { return Password; }
            set
            {
                Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
