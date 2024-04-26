using MerrMail.Maui.Views;

namespace MerrMail.Maui;

public partial class App
{
    public App()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(EmailContextsPage), typeof(EmailContextsPage));

        MainPage = new AppShell();
    }
}