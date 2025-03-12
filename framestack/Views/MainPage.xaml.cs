using framestack.ViewModels;

namespace framestack
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

    }

}
