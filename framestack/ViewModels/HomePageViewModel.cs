using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using framestack.Models;
using framestack.Services;
using LiveChartsCore.Behaviours.Events;

namespace framestack.ViewModels;

public partial class HomePageViewModel : ViewModel
{
    [ObservableProperty]
    public ObservableCollection<Picture> pictures;
    [ObservableProperty]
    private LocalUserStorage localUserStorage;

    public HomePageViewModel()
    {
        localUserStorage = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<LocalUserStorage>(); //get singleton and add to local variable
        try
        {
            if (LocalUserStorage.Pictures == null)  //check if pictures has been set
            {
                LocalUserStorage.Pictures = new List<Picture>();
            }
            Pictures = LocalUserStorage.User.getPictureList().ToObservableCollection();
        }
        catch (Exception ex)
        {
            //Something happened.
        }
        GetPictures();  //get initial images from application
    }

    private async Task GetPictures()
    {
        var pictureList = await RestService.GetPictures(LocalUserStorage.User.eMail);
        Pictures = pictureList.ToObservableCollection();
        LocalUserStorage.Pictures = pictureList;
    }

    [RelayCommand]
    public async Task AddPicture()
    {
        var picture = await MediaPicker.PickPhotoAsync();
        if (picture != null)
        {
            UploadPicture(picture); //upload picture in separate task to let user continue doing other things (there is not much to do though)
        }
    }
    
    [RelayCommand]
    public async Task AddPictures()
    {
        var files = await FilePicker.PickMultipleAsync(PickOptions.Images);
        if (files == null) return; //TODO: tell user nothing was selected
        if (!files.Any()) return;
        UploadPictures(files.ToList());
    }

    private async Task UploadPicture(FileResult picture)
    {
        await RestService.UploadPicture(picture, LocalUserStorage.User, []);    //TODO: Allow user to add tags
        GetAllPictures();
    }

    private async Task UploadPictures(List<FileResult> files)
    {
        if (files == null) return;
        if (!files.Any()) return;
        await RestService.UploadPictures(files, LocalUserStorage.User);
        GetAllPictures();
    }

    private async Task GetAllPictures(int startPage = 0)
    {
        if (startPage == 0)
        {
            LocalUserStorage.Pictures.Clear();
            Pictures.Clear();
        }
        bool hasMoreContent = true;
        var tasks = new List<Task<List<Picture>>>();
        int amountOfPages = startPage + 10;
        for (startPage = startPage; startPage < amountOfPages; startPage++)
        {
            tasks.Add(RestService.GetPictures(LocalUserStorage.User.eMail, startPage));
        }
        await Task.WhenAll(tasks);
        foreach (var task in tasks)
        {
            foreach (var picture in task.Result)
            {
                Pictures.Add(picture);
            }
            LocalUserStorage.Pictures = task.Result;
            
            if (task.Result.Count < 20)
            {
                hasMoreContent = false;
            }
        }

        if (hasMoreContent)
        {
            GetAllPictures(startPage);
        }
    }
}