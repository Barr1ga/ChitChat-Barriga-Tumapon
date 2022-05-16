using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChitChat.Models;
using Plugin.CloudFirestore;
using ChitChat.Helpers;
using Newtonsoft.Json;

namespace ChitChat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResult : ContentPage
    {
        List<UserModel> userList = new List<UserModel>();

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

        /*private List<UserModel> Init_List()
        {
            List<UserModel> userList = new List<UserModel>()
              {
                    new UserModel { name = "Horeb", email = "horeb@gmail.com" },
                    new UserModel { name = "Van AJ", email = "van@gmail.com" },
                    new UserModel { name = "CJ", email = "cj@gmail.com" },
                    new UserModel { name = "AJ", email = "aj@gmail.com" },
                    new UserModel { name = "Edwin", email = "edwin@gmail.com" },
                    new UserModel { name = "Chris", email = "chris@gmail.com" },
                    new UserModel { name = "Nikolai", email = "nikolai@gmail.com" },
                    new UserModel { name = "Nina", email = "nina@gmail.com" },
              };

            return userList;
        }*/

        public SearchResult()
        {
            InitializeComponent();
            LoadUsers();
            BindingContext = this;
        }

        public async Task LoadUsers()
        {
            DataClass dataClass = DataClass.GetInstance;

            IsBusy = true;
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("users")
                                        .GetAsync();
            var model = document.ToObjects<UserModel>();
            userList = model.ToList();
            usersView.ItemsSource = userList;
            IsBusy = false;
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            var select = (UserModel)((Button)sender).CommandParameter;
            if (select == null)
            {
                return;
            }

            var answer = await DisplayAlert("Would you like to add", select.name, "YES", "NO");
            DataClass dataClass = DataClass.GetInstance;
            Console.WriteLine(dataClass.loggedInUser.contacts);
            try
            {
                if (answer)
                {
                    //check if current user selected himself
                    if (dataClass.loggedInUser.uid == select.uid)
                    {
                        await DisplayAlert("Error", "You cannot add yourself", "OK");
                        return;
                    }

                    if (dataClass.loggedInUser.contacts != null)
                    {
                        if (dataClass.loggedInUser.contacts.Contains(select.uid))
                        {
                            await DisplayAlert("Error", "You both already have a connection ", "OK");
                            return;
                        }
                    }
                    

                    //Yes
                    ContactModel contact = new ContactModel()
                    {
                        id = Guid.NewGuid().ToString(),
                        contactID = new string[] { dataClass.loggedInUser.uid, select.uid },
                        contactEmail = new string[] { dataClass.loggedInUser.email, select.email },
                        contactName = new string[] { dataClass.loggedInUser.name, select.name },
                        created_at = DateTime.UtcNow
                    };

                    // Set contact
                    await CrossCloudFirestore.Current
                        .Instance
                        .Collection("contacts")
                        .Document(contact.id)
                        .SetAsync(contact);

                    //create new list if current user has no contacts
                    if (dataClass.loggedInUser.contacts == null)
                    {
                        dataClass.loggedInUser.contacts = new List<string>();
                    }

                    //add to existing or new contact list (inside dataclass)
                    dataClass.loggedInUser.contacts.Add(select.uid);

                    //update contactlist of current user with updated list
                    await CrossCloudFirestore.Current
                        .Instance
                        .Collection("users")
                        .Document(dataClass.loggedInUser.uid)
                        .UpdateAsync(new { contacts = dataClass.loggedInUser.contacts });

                    //create new list if selected user has no contacts
                    if (select.contacts == null)
                    {
                        select.contacts = new List<string>();
                    }

                    //add to existing or new contact list (inside dataclass)
                    select.contacts.Add(select.uid);

                    //update contactlist of selected user in firebase with updated list
                    await CrossCloudFirestore.Current
                        .Instance
                        .Collection("users")
                        .Document(select.uid)
                        .UpdateAsync(new { contacts = dataClass.loggedInUser.contacts });

                    await DisplayAlert("Success", "Contact added!", "OK");
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }
        }
            
        private void UsersView_ItemTapped(object sender, TappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                IsBusy = true;
                if (userList.Where(s => s.email.Contains(SearchBar.Text)) != null)
                {
                    usersView.ItemsSource = userList.Where(s => s.email.Contains(SearchBar.Text));
                }
                else
                {
                    await DisplayAlert("", "User not found.", "OK");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                await Task.Delay(200);
                IsBusy = false;
            }

        }

    }

}