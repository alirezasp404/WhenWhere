using System.Collections.ObjectModel;
using WhenWhere.Models;
using WhenWhere.Services;

namespace WhenWhere.Pages;

public partial class CreatedEvents : ContentPage
{
    private string? userId;
    public ObservableCollection<EventModel> AllEvents { get; set; } = new ObservableCollection<EventModel>();
    public CreatedEvents()
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
            var events = await EventsService.GetAllEvents($"create_event/{userId}/");
            if (events != null)
            {

                foreach (var eventModel in events)
                {
                    if (!AllEvents.Contains(eventModel))
                    {
                        AllEvents.Add(eventModel);
                    }
                }
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Failed", "An error occurred while loading your created events", "OK");
        }


    }
    private async void CreateEventButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CreateEvent");
    }
}