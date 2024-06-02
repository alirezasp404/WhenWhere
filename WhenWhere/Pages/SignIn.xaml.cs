
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using WhenWhere.Models;
using WhenWhere.ServiceContracts;
using WhenWhere.Services;

namespace WhenWhere.Pages;

public partial class SignIn : ContentPage
{
    public LoginModel Login { get; set; } = new LoginModel();
    private readonly IHttpClientBuilder _httpClientBuilder;

    public SignIn(IHttpClientBuilder httpClient)
    {
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
        Shell.SetTabBarIsVisible(this, false);
        InitializeComponent();
        BindingContext = Login;
        _httpClientBuilder = httpClient;
    }
    private async void Login_clicked(object sender, EventArgs e)
    {
        HttpClient _client = new HttpClient();

        try
        {

            if (string.IsNullOrWhiteSpace(Login.Email) || string.IsNullOrWhiteSpace(Login.Password))
            {
                await DisplayAlert("Error", "All fields are required.", "OK");
            }
            else if (!ValidateEmail(Login.Email))
            {
                await DisplayAlert("Error", "Format of email is not valid", "OK");

            }
            var result = await _httpClientBuilder.LoginAsync(Login);

            if (result)
            {

                await Shell.Current.GoToAsync("//home");
            }
            else
            {
                await DisplayAlert("Error", "email or password is wrong", "OK");
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "An error occurred during Sign In", "OK");
        }

    }

    public bool ValidateEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";


        if (!Regex.IsMatch(email, emailPattern))
        {
            return false;
        }

        return true;
    }

    private async void SignUp_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SignUp");
    }
}