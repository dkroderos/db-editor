using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MerrMail.Maui.Services;
using MerrMail.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerrMail.Maui.ViewModels;

public partial class PasswordViewModel(IEmailContextService emailContextService) : BaseViewModel
{
    [ObservableProperty]
    public string? inputPassword;

    [RelayCommand]
    public async Task CheckPasswordAsync()
    {
        var password = await emailContextService.GetPasswordAsync();

        if (InputPassword != password)
        {
            await Shell.Current.DisplayAlert("Incorrect Password",
                "The password you've entered was incorrect",
                "Ok");
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            return;
        }

        await Shell.Current.GoToAsync($"..");
        await Shell.Current.GoToAsync($"//{nameof(EmailContextsPage)}");
    }
}
