using WhenWhere.Pages;
using WhenWhere.ServiceContracts;
using WhenWhere.Services;

namespace WhenWhere
{
    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("CreateEvent", typeof(CreateEvent));
            Routing.RegisterRoute("SignIn", typeof(SignIn));
            Routing.RegisterRoute("SignUp", typeof(SignUp));
            Routing.RegisterRoute("event_details", typeof(EventDetails));
            CheckUserLoggedIn();
        }

        private async void CheckUserLoggedIn()
        {
            bool isLoggedIn = await SecureStorage.Default.GetAsync("TokenType") is not null;
            if (isLoggedIn)
            {
                landingflyout.IsVisible = false;
            }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            landingflyout.IsVisible = true;
            var authenticationService = new AuthenticationService(new HttpClientBuilder());
            await authenticationService.LogoutAsync();
            await Current.GoToAsync("//landing");

        }
    }
}
