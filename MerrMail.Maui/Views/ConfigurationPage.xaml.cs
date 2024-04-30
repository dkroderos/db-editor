using MerrMail.Maui.ViewModels;

namespace MerrMail.Maui.Views;

public partial class ConfigurationPage : ContentPage
{
    private readonly ConfigurationViewModel configurationViewModel;

    public ConfigurationPage(ConfigurationViewModel configurationViewModel)
	{
		InitializeComponent();
        this.configurationViewModel = configurationViewModel;
        BindingContext = this.configurationViewModel;
    }
}