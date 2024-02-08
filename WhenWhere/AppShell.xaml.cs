using WhenWhere.Pages;

namespace WhenWhere
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Landing", typeof(Landing));
            Routing.RegisterRoute("CreateEvent", typeof(CreateEvent));
            Routing.RegisterRoute("SignIn", typeof(SignIn));
            Routing.RegisterRoute("SignUp", typeof(SignUp));
        }
    }
}
