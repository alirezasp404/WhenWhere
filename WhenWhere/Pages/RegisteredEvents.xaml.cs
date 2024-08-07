using System.Collections.ObjectModel;
using WhenWhere.Models;
using WhenWhere.ServiceContracts;
using WhenWhere.Services;

namespace WhenWhere.Pages;

public partial class RegisteredEvents : ContentPage
{
    private string? userId;
    private readonly IEventsService _eventsService;

    public ObservableCollection<EventModel> AllEvents { get; set; } = new ObservableCollection<EventModel>();
    public RegisteredEvents(IEventsService eventsService)
    {
        InitializeComponent();
        BindingContext = this;
        this._eventsService = eventsService;
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
            var events = await _eventsService.GetRegisteredEvents();
            AllEvents?.Clear();
            foreach (var eventModel in events)
            {
                AllEvents?.Add(eventModel);
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Failed", "An error occurred while loading your registered events", "OK");
        }
    }
}