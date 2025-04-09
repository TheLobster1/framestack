using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using framestack.Models;
using framestack.Services;

namespace framestack.ViewModels;

public partial class HomePageViewModel : ViewModel
{
    [ObservableProperty]
    public string username;

    [ObservableProperty]
    public List<Picture> pictures;
    
    private readonly LocalUserStorage localUserStorage;

    public HomePageViewModel()
    {
        localUserStorage = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<LocalUserStorage>();
    }
}