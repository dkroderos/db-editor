using MerrMail.Maui.ViewModels;

namespace MerrMail.Maui.Views;

public partial class CreateEmailContextPage : ContentPage
{
    private readonly CreateEmailContextViewModel createEmailContextViewModel;

    public CreateEmailContextPage(CreateEmailContextViewModel createEmailContextViewModel)
    {
        InitializeComponent();

        this.createEmailContextViewModel = createEmailContextViewModel;
        BindingContext = this.createEmailContextViewModel;
    }
}