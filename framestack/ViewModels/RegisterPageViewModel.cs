using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using framestack.Models;
using framestack.Services;
using framestack.Views;

namespace framestack.ViewModels
{
    public partial class RegisterPageViewModel : ViewModel
    {
        private bool allowedUsername = false;
        private bool allowedFirstname = false;
        private bool allowedLastname = false;
        private bool allowedEmail = false;
        private bool allowedPassword = false;
        private bool allowedDateOfBirth = false;

        [ObservableProperty]
        public string username;
        [ObservableProperty]
        public string firstname;
        [ObservableProperty]
        public string lastname;
        [ObservableProperty]
        public string email;
        [ObservableProperty]
        public string password;
        [ObservableProperty]
        public DateTime dateOfBirth;
        [ObservableProperty]
        public string errorMessage;
        
        private readonly LocalUserStorage localUserStorage;

        private CancellationTokenSource UsernameDatabaseCheckToken = new CancellationTokenSource();
        public RegisterPageViewModel()
        {
            localUserStorage = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<LocalUserStorage>();
        }

        partial void OnEmailChanged(string value)
        {
            //TODO: CHECK EMAIL
            CheckUsernameWithDelay();
        }

        partial void OnUsernameChanged(string value)
        {
            CheckUsernameWithDelay();
        }

        partial void OnFirstnameChanged(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                allowedFirstname = false;
                return;
            }
            allowedFirstname = true;
        }

        partial void OnLastnameChanged(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                allowedLastname = false;
                return;
            }
            allowedLastname = true;
        }

        partial void OnPasswordChanged(string value)
        {
            if (value.Length is < 8 or > 72)
            {
                allowedPassword = false;
                return;
            }
            allowedPassword = true;
        }

        partial void OnDateOfBirthChanged(DateTime value)
        {
            if (value > DateTime.Today - TimeSpan.FromDays(5840))
            {
                allowedDateOfBirth = false;
                return;
            }
            allowedDateOfBirth = true;
        }

        [RelayCommand]
        private async Task Register()
        {
            ErrorMessage = "";
            if (!allowedUsername || !allowedFirstname || !allowedLastname || !allowedEmail || !allowedPassword ||
                !allowedDateOfBirth) return;    //check if user filled in all fields
            var tempUser = new User(Username, Password, Firstname, Lastname, Email, DateOfBirth);
            tempUser.setPassword(Password);
            var result =
                await RestService.CheckUser(tempUser); //Checks if username and email have been used before.
            if (result.Count > 0)
            {
                foreach (var message in result)
                {
                    ErrorMessage += $"{message}\n";
                }

                return;
            }

            await RestService.CreateUser(tempUser); //wait for user to be created
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage(new LoginPageViewModel()));   //navigate to login page

        }

        private async Task CheckUsernameWithDelay()
        {
            var user = new User(Username, "", "", "", Email, DateOfBirth);  //create new user variable to check
            UsernameDatabaseCheckToken.Cancel();    //cancel previous task
            UsernameDatabaseCheckToken = new CancellationTokenSource(); //set new token
            await Task.Delay(1000, UsernameDatabaseCheckToken.Token);   //wait for one second before continuing
            var result = await RestService.CheckUser(user);   //check if user exists in database with returned error messages showing which error happend
            ErrorMessage = "";
            allowedUsername = true;
            allowedEmail = true;
            if (result.Count > 0)
            {
                foreach (var message in result)
                {
                    if (message.Contains("Username")) allowedUsername = false;  //if message with string "Username" returns the username is already taken
                    if (message.Contains("Email")) allowedEmail = false;        //if message with string "Email" returns, the email is already in use
                    ErrorMessage += $"{message}\n"; //show the error message on screen if the error message box was added to the UI
                }
            }
        }
    }
}
