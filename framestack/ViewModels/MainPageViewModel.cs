using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //TODO: NAVIGATE TO LOGIN

        }
    }
}
