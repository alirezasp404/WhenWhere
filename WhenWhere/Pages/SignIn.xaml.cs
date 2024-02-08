using MauiApp9;
using MauiApp9.Models;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace WhenWhere.Pages;

public partial class SignIn : ContentPage
{
    public Login_model Login { get; set; } = new Login_model();
    public SignIn()
    {

        InitializeComponent();
        BindingContext = Login;
    }
    private async void Login_clicked(object sender, EventArgs e)
    {
        HttpClient _client = new HttpClient();


        if (string.IsNullOrWhiteSpace(Login.email) || string.IsNullOrWhiteSpace(Login.password))
        {
            await DisplayAlert("Error", "All fields are required.", "OK");
        }

        if (!ValidateEmail(Login.email))
        {
            await DisplayAlert("Error", "format of email is not valid", "OK");

        }


        try
        {
            string json = JsonSerializer.Serialize(Login);
            StringContent content1 = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("http://127.0.0.1:8000/login", content1);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var Items = JsonSerializer.Deserialize<Login_res>(content);
                Preferences.Set("UserID", Items.id.ToString());
                //await shell.current.gotoasync("home/events");
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