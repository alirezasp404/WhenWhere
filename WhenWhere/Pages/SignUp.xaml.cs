using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using WhenWhere.Models;

namespace WhenWhere.Pages;

public partial class SignUp : ContentPage
{
    public Register_model Register { get; set; } = new Register_model();

    public SignUp()
    {
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
        Shell.SetTabBarIsVisible(this, false);
        InitializeComponent();
        BindingContext = Register;

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
                await DisplayAlert("Error", "All fields are required.", "OK");

            }

            else if (Register.password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");

            }
            else if (!ValidateEmail(Register.email))
            {
                await DisplayAlert("Error", "format of email is not valid", "OK");

            }

            string json = JsonSerializer.Serialize(Register);
            StringContent content1 = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("http://127.0.0.1:8000/register", content1);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Done", "Your registration was successful. Please Sign In", "OK");

                await Shell.Current.GoToAsync("SignIn");

            }
            else
            {
                await DisplayAlert("Error", "email or password is wrong", "OK");

            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "An error occurred during Sign Up", "OK");

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
}