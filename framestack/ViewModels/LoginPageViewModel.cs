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
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) return; //TODO: ERROR MESSAGES
            var tempUser = new User(Username, Password, "", "", Username, DateTime.Now);
            var success = await RestService.VerifyPassword(tempUser);
            if (!success) return; //TODO: show message on screen.
            var result = await RestService.GetUserDetails(tempUser);
            localUserStorage.User = result;
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage(new HomePageViewModel()));
            // Application.Current.MainPage.Navigation.NavigationStack.Reverse();


        }
        
    }
}
