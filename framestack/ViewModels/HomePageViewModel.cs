using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using framestack.Models;
using framestack.Services;
using LiveChartsCore.Behaviours.Events;

namespace framestack.ViewModels;

public partial class HomePageViewModel : ViewModel
{
    [ObservableProperty]
    public List<Picture> pictures;
    [ObservableProperty]
    private readonly LocalUserStorage localUserStorage;

    public HomePageViewModel()
    {
        localUserStorage = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<LocalUserStorage>();
        try
        {
            Pictures = localUserStorage.User.getPictureList();
        }
        catch (Exception ex)
        {
            //Something happened.
        }
        GetPictures();
    }

    private async Task GetPictures()
    {
        Pictures = await RestService.GetPictures(localUserStorage.User.eMail);
        localUserStorage.Pictures = Pictures;
    }

    [RelayCommand]
    public async Task AddPicture()
    {
        var picture = await MediaPicker.PickPhotoAsync();
        if (picture != null)
        {
            UploadPicture(picture);
        }
    }
    [RelayCommand]
    public async Task CollectionViewScrolled()
    {
        
    }

    private async Task UploadPicture(FileResult picture)
    {
        await RestService.UploadPicture(picture, localUserStorage.User, []);
        GetPictures();
    }
}