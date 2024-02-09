

namespace WhenWhere.Pages;

public partial class Landing : ContentPage
{

    public Landing()
    {
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
        Shell.SetTabBarIsVisible(this, false);
        InitializeComponent();


    }
  


    private async void Login_clicked(object sender, EventArgs e)
    {
       await Shell.Current.GoToAsync("SignIn");
    }
    private async void SignUp_clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SignUp");
        
    }

}