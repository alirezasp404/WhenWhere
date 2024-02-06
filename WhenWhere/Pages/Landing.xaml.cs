namespace WhenWhere.Pages;

public partial class Landing : ContentPage
{
	public Landing()
	{
		InitializeComponent();
		Preferences.Set("UserID", "2");
	}
}