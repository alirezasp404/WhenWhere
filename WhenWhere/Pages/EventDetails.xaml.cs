using WhenWhere.Models;
using WhenWhere.ServiceContracts;
using WhenWhere.Services;

namespace WhenWhere.Pages;
[QueryProperty("EventModel", "EventModel")]
public partial class EventDetails : ContentPage
{


    private EventModel? eventModel;
    private readonly IEventsService _eventsService;

    public EventModel? EventModel
    {
        get => eventModel;
        set
        {
            eventModel = value;
            BindingContext = eventModel;
        }
    }
    public EventDetails(IEventsService eventsService)
    {
        InitializeComponent();
        this._eventsService = eventsService;
    }
    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        try
        {

            await _eventsService.RegisterEvent(eventModel?.Id);
            await DisplayAlert("Done", $"You Registered in \"{eventModel?.Title}\"", "OK");
            //else
            //    await DisplayAlert("Failed", "You have already registered for this event", "OK");

        }
        catch (Exception)
        {
            await DisplayAlert("Failed", "An error occurred while registering for this event. Please try again", "OK");
        }
        await Shell.Current.GoToAsync("..");
    }
}