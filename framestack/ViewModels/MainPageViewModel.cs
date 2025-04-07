using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using framestack.Models;
using framestack.Services;
using framestack.Views;

namespace framestack.ViewModels
{
    public partial class MainPageViewModel : ViewModel
    {
        private Account Account { get; set; } = new Account([]);
        public MainPageViewModel()
        {
        }

        [RelayCommand]
        private async Task Login()
        {
            // await Application.Current.MainPage.Navigation.PushAsync(new LoginPage(new LoginPageViewModel()));
            // Account.addPhoto();
            // var pictures = Account.getPictureList();
            // User testUser = new User("bobert", "$2a$12$FC0Jxqoi5XeUiLo.5hH7a.vyU5IGw0O869FQVo1UiJNmkfTz0JyYG", "Rob",
            //     "Veldman", "rjeveldman@hotmail.nl", new DateTime(2003, 4, 9), new List<Album>());
            // var result = await RestService.CreateUser(testUser);
            var pictures = await RestService.GetPictures(1);
        }

        [RelayCommand]
        private async Task Register()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage(new RegisterPageViewModel()));


        }
    }
}
