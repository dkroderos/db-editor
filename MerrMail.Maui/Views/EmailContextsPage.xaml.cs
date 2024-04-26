using MerrMail.Maui.ViewModels;

namespace MerrMail.Maui.Views;

public partial class EmailContextsPage : ContentPage
{
    private readonly EmailContextsViewModel emailContextsViewModel;

    public EmailContextsPage(EmailContextsViewModel emailContextsViewModel)
	{
		InitializeComponent();

        this.emailContextsViewModel = emailContextsViewModel;
        BindingContext = this.emailContextsViewModel;
    }
}