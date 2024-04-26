using MerrMail.Maui.ViewModels;

namespace MerrMail.Maui.Views;

public partial class EmailContextDetailsPage : ContentPage
{
    private readonly EmailContextDetailsViewModel emailContextDetailsViewModel;

	public EmailContextDetailsPage(EmailContextDetailsViewModel emailContextDetailsViewModel)
	{
		InitializeComponent();

        this.emailContextDetailsViewModel = emailContextDetailsViewModel;
        BindingContext = this.emailContextDetailsViewModel;
    }
}