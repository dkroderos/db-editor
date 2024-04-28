using MerrMail.Maui.ViewModels;

namespace MerrMail.Maui.Views;

public partial class PasswordPage : ContentPage
{
    private readonly PasswordViewModel passwordViewModel;

    public PasswordPage(PasswordViewModel passwordViewModel)
    {
        InitializeComponent();
        this.passwordViewModel = passwordViewModel;
        BindingContext = this.passwordViewModel;
    }
}