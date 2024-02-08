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
                throw new Exception("All fields are required.");
            }

            if (Register.password != confirmPassword)
            {
                throw new Exception("Passwords do not match.");

            }
            if (!ValidateEmail(Register.email))
            {
                throw new Exception("format of email is not valid");

            }

            string json = JsonSerializer.Serialize(Register);
            StringContent content1 = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("http://127.0.0.1:8000/register", content1);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Done", "please login", "OK");

                await Shell.Current.GoToAsync("SignIn");

            }
            else
            {
                throw new Exception("email or password is wrong");

            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");

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