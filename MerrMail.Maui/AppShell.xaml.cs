using MerrMail.Maui.Views;

namespace MerrMail.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        Routing.RegisterRoute(nameof(EmailContextsPage), typeof(EmailContextsPage));
        Routing.RegisterRoute(nameof(CreateEmailContextPage), typeof(CreateEmailContextPage));

        InitializeComponent();
    }
}
