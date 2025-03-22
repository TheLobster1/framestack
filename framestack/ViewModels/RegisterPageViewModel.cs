using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

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

        private CancellationTokenSource UsernameDatabaseCheckToken = new CancellationTokenSource();
        private CancellationTokenSource EmailDatabaseCheckToken = new CancellationTokenSource();
        
        [RelayCommand]
        private async Task UpdateUsernameValue()
        {
            await Application.Current.MainPage.Navigation.PopAsync();

        }
        
        // private async Task Update
    }
}
