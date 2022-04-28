using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ChitChat.Models
{
    class ContactInfo : INotifyPropertyChanged
    {

        public string _id
        {
            get
            { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(_id));
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
