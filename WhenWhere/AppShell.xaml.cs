using WhenWhere.Pages;

namespace WhenWhere
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Landing", typeof(Landing));
        }
    }
}
