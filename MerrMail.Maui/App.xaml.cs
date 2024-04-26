using MerrMail.Maui.Views;

namespace MerrMail.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(EmailContextsPage), typeof(EmailContextsPage));

        MainPage = new AppShell();
    }
}
