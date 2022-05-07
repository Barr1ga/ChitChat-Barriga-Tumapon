using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using ChitChat.Models;

namespace ChitChat.DependencyServices
{
    public interface iFirebaseAuth
    {
        Task<FirebaseAuthResponseModel> LoginWithEmailPassword(string email, string password);
        Task<FirebaseAuthResponseModel> SignUpWithEmailPassword(string name, string email, string password);
        FirebaseAuthResponseModel SignOut();
        FirebaseAuthResponseModel IsLoggedIn();
        Task<FirebaseAuthResponseModel> ResetPassword(string email);
    }
}
