using WhenWhere.Models;
using WhenWhere.Services;

namespace WhenWhere.Pages;

public partial class Profile : ContentPage
{
    public ProfileModel? profileModel { get; set; }
    public Profile()
    {

        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            profileModel = await EventsService.GetProfileModel(Preferences.Get("UserId", null));
        } catch (Exception ex)
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