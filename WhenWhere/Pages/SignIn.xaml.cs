

using System.Text.RegularExpressions;
using WhenWhere.Exceptions;
using WhenWhere.Models;
using WhenWhere.ServiceContracts;
using WhenWhere.Services;

namespace WhenWhere.Pages;

public partial class SignIn : ContentPage
{
    private readonly IAuthenticationService _authenticationService;

    public LoginModel Login { get; set; } = new LoginModel();

    public SignIn(IAuthenticationService authenticationService)
    {
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
        Shell.SetTabBarIsVisible(this, false);
        InitializeComponent();
        BindingContext = Login;
        this._authenticationService = authenticationService;
    }
    private async void Login_clicked(object sender, EventArgs e)
    {
        HttpClient _client = new HttpClient();

        try
        {
            if (string.IsNullOrWhiteSpace(Login.Email) || string.IsNullOrWhiteSpace(Login.Password))
            {
                throw new AuthenticationValidationException("All fields are required.");
            }
            else
            {
                ValidateEmail(Login.Email);
                await _authenticationService.LoginAsync(Login);
                await Shell.Current.GoToAsync("//home");
            }
        }
        catch (AuthenticationValidationException ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "An error occurred during Sign In", "OK");
        }
    }

    public void ValidateEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";


        if (!Regex.IsMatch(email, emailPattern))
        {
            throw new AuthenticationValidationException("Format of email is not valid");

        }
    }

    private async void SignUp_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SignUp");
    }
}