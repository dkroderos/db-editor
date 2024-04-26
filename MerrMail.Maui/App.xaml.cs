using MerrMail.Maui.Views;

namespace MerrMail.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
