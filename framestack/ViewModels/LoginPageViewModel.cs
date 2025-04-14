using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using framestack.Models;
using framestack.Services;
using framestack.Views;

namespace framestack.ViewModels
{
    public partial class LoginPageViewModel : ViewModel
    {
        [ObservableProperty]
        public string username;
        [ObservableProperty]
        public string password;
        [ObservableProperty]
        public string errorMessage;

        private readonly LocalUserStorage localUserStorage;
        public LoginPageViewModel()
        {
            localUserStorage = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<LocalUserStorage>();

        }

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) return;   //if no username or password return, this should show an error message in the next version.
            var tempUser = new User(Username, Password, "", "", Username, DateTime.Now);    //create a temporary user instance to pass to the restservice which passes it to the API to check.
            var success = await RestService.VerifyPassword(tempUser);   //verify passwrod using API call
            if (!success) return; //TODO: show message on screen.
            var result = await RestService.GetUserDetails(tempUser); //if user exists and password is correct get further details.
            localUserStorage.User = result; //set the singleton user field
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage(new HomePageViewModel())); //navigate to homepage

        }
        
    }
}
