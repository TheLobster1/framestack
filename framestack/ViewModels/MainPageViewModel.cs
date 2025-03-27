using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using framestack.Models;
using framestack.Views;

namespace framestack.ViewModels
{
    public partial class MainPageViewModel : ViewModel
    {
        private Account Account { get; set; } = new Account(DateTime.Now, []);
        public MainPageViewModel()
        {
        }

        [RelayCommand]
        private async Task Login()
        {
            // await Application.Current.MainPage.Navigation.PushAsync(new LoginPage(new LoginPageViewModel()));
            Account.addPhoto();
            var pictures = Account.getPictureList();
        }

        [RelayCommand]
        private async Task Register()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage(new RegisterPageViewModel()));


        }
    }
}
