using MerrMail.Maui.ViewModels;

namespace MerrMail.Maui.Views;

public partial class AddAccountPage : ContentPage
{
    private readonly AddAccountViewModel addAccountViewModel;

    public AddAccountPage(AddAccountViewModel addAccountViewModel)
	{
		InitializeComponent();
        this.addAccountViewModel = addAccountViewModel;
        BindingContext = this.addAccountViewModel;
    }
}