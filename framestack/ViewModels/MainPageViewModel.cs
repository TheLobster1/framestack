using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using framestack.Views;

namespace framestack.ViewModels
{
    public partial class MainPageViewModel : ViewModel
    {
        //[ObservableProperty]
        //private int number;

        public MainPageViewModel()
        {
        }

        [RelayCommand]
        private async Task Login()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage(new LoginPageViewModel()));

        }

        [RelayCommand]
        private async Task Register()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage(new RegisterPageViewModel()));


        }
    }
}
