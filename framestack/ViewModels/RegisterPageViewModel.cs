﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        private CancellationTokenSource UsernameDatabaseCheckToken = new CancellationTokenSource();
        private CancellationTokenSource EmailDatabaseCheckToken = new CancellationTokenSource();

        public RegisterPageViewModel()
        {
            
        }
        
        [RelayCommand]
        private async Task Register()
        {
            //TODO: Register user using the RestService
        }

        private async Task CheckUsernameWithDelay(string Username)
        {
            UsernameDatabaseCheckToken.Cancel();
            UsernameDatabaseCheckToken = new CancellationTokenSource();
            await Task.Delay(1000, UsernameDatabaseCheckToken.Token);
            //TODO: Check database for value
            var toast = Toast.Make(Username);
            toast.Show();
        }
        
        [RelayCommand]
        private async Task UpdatePasswordValue()
        {
            
        }
    }
}
