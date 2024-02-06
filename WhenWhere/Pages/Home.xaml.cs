using System.Collections.ObjectModel;

using System.Text.Json;
using WhenWhere.Models;
using Windows.UI.Core;

namespace WhenWhere.Pages;

public partial class Home : ContentPage
{
    private readonly HttpClient _client = new HttpClient();
    public ObservableCollection<EventModel> Events { get; set; } = new ObservableCollection<EventModel>();
    public Home()
    {
        InitializeComponent();
        BindingContext = this;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var userId = Preferences.Get("UserID", null);
        if (userId == null)
            await Shell.Current.GoToAsync("Landing");
        var getEvents = await _client.GetStringAsync("http://127.0.0.1:8000/event_list/");
        var events = JsonSerializer.Deserialize<List<EventModel>>(getEvents);
        foreach (var eventModel in events)
        {
            Events.Add(eventModel);
        }

        
    }



    private void RegisterButton_Clicked(object sender, EventArgs e)
    {
        var eventModel = (EventModel)((Button)sender).BindingContext;

    }

    private void CreateEventButton_Clicked(object sender, EventArgs e)
    {

    }
}