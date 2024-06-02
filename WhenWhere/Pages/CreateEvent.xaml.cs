
using WhenWhere.Models;
using WhenWhere.Services;

namespace WhenWhere.Pages;

public partial class CreateEvent : ContentPage
{

    public CreateEventModel EventModel { get; set; } = new();
    public string? Capacity { get; set; }
    public DateTime WhenDate { get; set; } = DateTime.Now;
    public TimeSpan WhenTime { get; set; } = TimeSpan.FromHours(1);

    private string? userId;


    public CreateEvent()
    {
        InitializeComponent();
        BindingContext = this;
    }

    //private async void SubmitButton_Clicked(object sender, EventArgs e)
    //{
    //    userId =Preferences.Get("UserId", null);

    //    EventModel.expired_at = new DateTime(WhenDate.Year, WhenDate.Month, WhenDate.Day, WhenTime.Hours, WhenTime.Minutes, WhenTime.Seconds);
    //    int capacity;
    //    bool isValidCapacity = int.TryParse(Capacity, out capacity);
    //    bool isValid = isValidCapacity && capacity > 0 && EventModel.title != null && EventModel.description != null && EventModel.location != null;

    //    try
    //    {
    //        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
    //        {
    //            await DisplayAlert("Failed", "It seems that your internet connection has been lost. Please check your connection and try again.", "OK");
    //        }
    //        else if (!isValid)
    //            await DisplayAlert("Failed", "Please enter valid input", "OK");
    //        else
    //        {
    //            EventModel.capacity = capacity;
    //            await EventsService.CreateEvent(EventModel, userId);
    //            await DisplayAlert("Done", "Your event has been successfully created", "OK");
    //            await Shell.Current.GoToAsync("..");
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        await DisplayAlert("Failed", "An error occurred while registering for this event. Please try again", "OK");
    //    }

    //}

}