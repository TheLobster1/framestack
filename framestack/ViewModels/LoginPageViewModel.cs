using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using framestack.Models;
using framestack.Services;

namespace framestack.ViewModels
{
    public partial class LoginPageViewModel : ViewModel
    {
        [ObservableProperty]
        public string username;
        [ObservableProperty]
        public string password;
        public LoginPageViewModel()
        {
            
        }

        [RelayCommand]
        private async Task LoginCommand()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) return;
            var tempUser = new User(Username, Password, "", "", "", DateTime.Now, []);
            tempUser.setPassword(Password);
            var success = await RestService.VerifyPassword(tempUser);
            if (!success) return; //TODO: show message on screen.
            var result = await RestService.GetUserDetails(tempUser);

        }
        
    }
}
