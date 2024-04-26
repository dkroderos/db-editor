using MerrMail.Maui.ViewModels;

namespace MerrMail.Maui.Views;

public partial class HomePage : ContentPage
{
	private readonly HomeViewModel homeViewModel;

	public HomePage(HomeViewModel homeViewModel)
	{
		InitializeComponent();

		this.homeViewModel = homeViewModel;
		BindingContext = this.homeViewModel;
	}
}