
using System.Collections.ObjectModel;

using System.Text.Json;
using WhenWhere.Models;
using WhenWhere.Services;

namespace WhenWhere.Pages;

public partial class Events : ContentPage
{
    private readonly HttpClient _client = new HttpClient();
    private string? userId;
    public ObservableCollection<EventModel> AllEvents { get; set; } = new ObservableCollection<EventModel>();
    public Events()
    {
        InitializeComponent();
        BindingContext = this;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        userId = Preferences.Get("UserID", null);
        if (userId == null)
            await Shell.Current.GoToAsync("Landing");

        try
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                throw new Exception("It seems that your internet connection has been lost. Please check your connection and try again.");
            var events = await EventsService.GetAllEvents();
            foreach (var eventModel in events)
            {
                if (!AllEvents.Contains(eventModel))
                {
                    AllEvents.Add(eventModel);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Failed", $"{ex.Message}", "OK");
        }

    }



    private void RegisterButton_Clicked(object sender, EventArgs e)
    {
        var eventModel = (EventModel)((Button)sender).BindingContext;

    }

    private async void CreateEventButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"CreateEvent?UserId={userId}");
    }
}