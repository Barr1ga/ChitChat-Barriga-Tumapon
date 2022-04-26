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
    [QueryProperty("ContactName", "name")]
    public partial class ConversationPage : ContentPage
    {
        public string _contactName;
        public ConversationPage()
        {
            InitializeComponent();
            BindingContext = new ContactInfo();
        }
        public string ContactName
        {
            get => _contactName;
            set => Header.Text = value;
        }
    }
}