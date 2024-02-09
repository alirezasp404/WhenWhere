using WhenWhere.Models;
using WhenWhere.Services;

namespace WhenWhere.Pages;
[QueryProperty("EventModel", "EventModel")]
public partial class EventDetails : ContentPage
{


    private EventModel? eventModel;
    public EventModel? EventModel
    {
        get => eventModel;
        set
        {
            eventModel = value;
            BindingContext = eventModel;
        }
    }
    public EventDetails()
    {
        InitializeComponent();

    }
    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        try
        {

            var isRegistered = await EventsService.RegisterEvent(eventModel?.id, Preferences.Get("UserId", null));
            if (isRegistered)
                await DisplayAlert("Done", $"You Registered in \"{eventModel?.title}\"", "OK");

        }
        catch (Exception)
        {
            await DisplayAlert("Failed", "An error occurred while registering for this event. Please try again", "OK");
        }
        await Shell.Current.GoToAsync("..");
    }
}