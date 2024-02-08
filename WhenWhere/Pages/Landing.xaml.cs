namespace WhenWhere.Pages;

public partial class Landing : ContentPage
{
    public Landing()
    {
        InitializeComponent();
    }


    private void Login_clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SignIn());
    }
    private void SignUp_clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SignUp());


    }
    
}