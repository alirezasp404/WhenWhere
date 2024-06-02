
using System.Text.RegularExpressions;
using WhenWhere.Exceptions;
using WhenWhere.Models;
using WhenWhere.ServiceContracts;

namespace WhenWhere.Pages;

public partial class SignUp : ContentPage
{
    private readonly IAuthenticationService _authenticationService;

    public RegisterModel Register { get; set; } = new RegisterModel();

    public SignUp(IAuthenticationService authenticationService)
    {
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
        Shell.SetTabBarIsVisible(this, false);
        InitializeComponent();
        BindingContext = Register;
        this._authenticationService = authenticationService;
    }
    private async void SignUp_clicked(object sender, EventArgs e)
    {
        HttpClient _client = new HttpClient();

        string confirmPassword = confirm_password_entry.Text;
        try
        {

            if (string.IsNullOrWhiteSpace(Register.first_name) || string.IsNullOrWhiteSpace(Register.last_name) ||
                string.IsNullOrWhiteSpace(Register.last_name) || string.IsNullOrWhiteSpace(Register.email) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                throw new AuthenticationValidationException("All fields are required.");
            }
            else if (Register.password != confirmPassword)
            {
                throw new AuthenticationValidationException("Passwords do not match.");
            }
            else
            {
                ValidateEmail(Register.email);
                await _authenticationService.RegisterAsync(Register);

                await DisplayAlert("Done", "Your registration was successful. Please Sign In", "OK");

                await Shell.Current.GoToAsync("SignIn");

            }
        }
        catch (AuthenticationValidationException ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");

        }
        catch (Exception)
        {
            await DisplayAlert("Error", "An error occurred during Sign Up", "OK");

        }



    }
    public void ValidateEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";


        if (!Regex.IsMatch(email, emailPattern))
        {
            throw new AuthenticationValidationException("format of email is not valid.");
        }

    }
}