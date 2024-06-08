using WhenWhere.Models;
using WhenWhere.ServiceContracts;
using WhenWhere.Services;

namespace WhenWhere.Pages;

public partial class Profile : ContentPage
{
    private readonly IUserInfoService _userInfoService;

    public ProfileModel? profileModel { get; set; }
    public Profile(IUserInfoService userInfoService)
    {

        this._userInfoService = userInfoService;
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            profileModel = await _userInfoService.GetProfile();
        }
        catch (Exception)
        {
            await DisplayAlert("Failed", "An error occurred while loading your Profile", "OK");

        }
        BindingContext = profileModel;

    }

    private async void CreatedButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//home/created");

    }

    private async void RegisteredButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//home/registered");
    }
}