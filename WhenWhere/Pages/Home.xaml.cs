using System.Collections.ObjectModel;
using WhenWhere.Models;

namespace WhenWhere.Pages;

public partial class Home : ContentPage
{
	public ObservableCollection<EventModel> Events { get; set; }=new ObservableCollection<EventModel>();
	public Home()
	{
        Preferences.Get("UserID", null);
        InitializeComponent();
        BindingContext = this;
		var event1=new EventModel()
		{
			Title="eve1",
			Description= "In publishing and graphic design, Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content. Lorem ipsum may be used as a placeholder before the final copy is available.",
			Id=1,
			Location="wwoegjowjeg",
			DateTime=DateTime.Now,
			Capacity=10,
			Creator="owegjo"
		};
        var event2 = new EventModel()
        {
            Title = "eve2",
            Description = "In publishing and graphic design, Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content. Lorem ipsum may be used as a placeholder before the final copy is available.",
            Id = 1,
            Location = "wwoegjowjeg",
            DateTime = DateTime.Now,
            Capacity = 10,
            Creator = "owegjo"
        };
        var event3 = new EventModel()
        {
            Title = "eve3",
            Description = "owjgojw",
            Id = 1,
            Location = "wwoegjowjeg",
            DateTime = DateTime.Now,
            Capacity = 10,
            Creator = "owegjo"
        }; var event4 = new EventModel()
        {
            Title = "eve1",
            Description = "owjgojw",
            Id = 1,
            Location = "wwoegjowjeg",
            DateTime = DateTime.Now,
            Capacity = 10,
            Creator = "owegjo"
        };
        var event5 = new EventModel()
        {
            Title = "eve2",
            Description = "owjgojw",
            Id = 1,
            Location = "wwoegjowjeg",
            DateTime = DateTime.Now,
            Capacity = 10,
            Creator = "owegjo"
        };
        var event6 = new EventModel()
        {
            Title = "eve3",
            Description = "In publishing and graphic design, Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content. Lorem ipsum may be used as a placeholder before the final copy is available.",
            Id = 1,
            Location = "In publishing and graphic design, Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content. Lorem ipsum may be used as a placeholder before the final copy is available.",
            DateTime = DateTime.Now,
            Capacity = 10,
            Creator = "owegjo"
        };
        Events.Add(event1); 
        Events.Add(event2);
        Events.Add(event3);
        Events.Add(event4);
        Events.Add(event5);
        Events.Add(event6);
        
    }

    private void RegisterButton_Clicked(object sender, EventArgs e)
    {

    }
}