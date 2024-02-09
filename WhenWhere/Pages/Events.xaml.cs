
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using WhenWhere.Models;
using WhenWhere.Services;

namespace WhenWhere.Pages;

public partial class Events : ContentPage
{
    public ObservableCollection<EventModel> AllEvents { get; set; } = new ObservableCollection<EventModel>();
    public Events()
    {
        InitializeComponent();
        BindingContext = this;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                throw new Exception("It seems that your internet connection has been lost. Please check your connection and try again.");
            var events = await EventsService.GetAllEvents("event_list/");
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
            await DisplayAlert("Failed", "An error occurred while loading all events", "OK");
        }

    }



    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        var eventModel = (EventModel)((Button)sender).BindingContext;
        await Shell.Current.GoToAsync("event_details", true, new Dictionary<string, object>
        {
            ["EventModel"] = eventModel,
            
        }) ; 
    }

    private async void CreateEventButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CreateEvent");
    }
}