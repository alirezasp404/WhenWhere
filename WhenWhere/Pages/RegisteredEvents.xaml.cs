using System.Collections.ObjectModel;
using WhenWhere.Models;
using WhenWhere.Services;

namespace WhenWhere.Pages;

public partial class RegisteredEvents : ContentPage
{
    private string? userId;
    public ObservableCollection<EventModel> AllEvents { get; set; } = new ObservableCollection<EventModel>();
    public RegisteredEvents()
    {
        userId = Preferences.Get("UserId", null);
        InitializeComponent();
        BindingContext = this;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Failed", "It seems that your internet connection has been lost. Please check your connection and try again.", "OK");
                throw new Exception();
            }
            var events = await EventsService.GetAllRegisterdEvents($"select_event/{userId}/");
            foreach (var eventModel in events)
            {
                if (!AllEvents.Contains(eventModel))
                {
                    AllEvents.Add(eventModel);
                }
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Failed", "An error occurred while loading your registered events", "OK");
        }
    }
}