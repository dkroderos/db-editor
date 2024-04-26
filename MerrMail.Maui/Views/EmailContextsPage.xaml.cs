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

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        emailContextsViewModel.GetEmailContextsCommand.ExecuteAsync(this);
    }
}