using MerrMail.Maui.Views;

namespace MerrMail.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        Routing.RegisterRoute(nameof(EmailContextsPage), typeof(EmailContextsPage));
        Routing.RegisterRoute(nameof(CreateEmailContextPage), typeof(CreateEmailContextPage));
        Routing.RegisterRoute(nameof(EditEmailContextPage), typeof(EditEmailContextPage));
        Routing.RegisterRoute(nameof(PasswordPage), typeof(PasswordPage));
        Routing.RegisterRoute(nameof(AddAccountPage), typeof(AddAccountPage));
        Routing.RegisterRoute(nameof(ConfigurationPage), typeof(ConfigurationPage));

        InitializeComponent();
    }
}
