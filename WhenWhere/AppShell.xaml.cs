using WhenWhere.Pages;

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
            var userId = Preferences.Get("UserId", null);
            if (userId != null)
            {
                landingflyout.IsVisible = false;
            }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            landingflyout.IsVisible = true;

            await Current.GoToAsync("//landing");
        }
    }
}
