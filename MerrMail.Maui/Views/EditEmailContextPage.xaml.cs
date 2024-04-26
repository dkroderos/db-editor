using MerrMail.Maui.ViewModels;

namespace MerrMail.Maui.Views;

public partial class EditEmailContextPage : ContentPage
{
    private readonly EditEmailContextViewModel editEmailContextViewModel;

    public EditEmailContextPage(EditEmailContextViewModel editEmailContextViewModel)
	{
		InitializeComponent();

        this.editEmailContextViewModel = editEmailContextViewModel;
        BindingContext = this.editEmailContextViewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        editEmailContextViewModel.GetEmailContextCommand.ExecuteAsync(this);
    }
}