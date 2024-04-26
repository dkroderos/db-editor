using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MerrMail.Maui.Models;
using MerrMail.Maui.Services;
using MerrMail.Maui.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerrMail.Maui.ViewModels;

[QueryProperty(nameof(EmailContext), "EmailContext")]
public partial class EmailContextDetailsViewModel(IEmailContextService emailContextService) : BaseViewModel
{
    [ObservableProperty]
    private EmailContext? emailContext;

    [RelayCommand]
    public async Task GoToEditEmailContextAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(EditEmailContextPage)}");
    }

    [RelayCommand]
    public async Task RemoveEmailContextAsync()
    {
        if (EmailContext is null || IsBusy) return;

        bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert("Are you sure?",
            "Are you sure you want to remove this Email Context?",
            "Yes", "No");

        if (!isConfirmed)
            return;

        try
        {
            IsBusy = true;

            await emailContextService.RemoveAsync(EmailContext!.Id);

            await Shell.Current.CurrentPage.DisplayAlert("Success",
                "Successfully removed Email Context",
                "Ok");

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);

            await Shell.Current.CurrentPage.DisplayAlert("Error",
                $"Unable to remove Email Context: {ex.Message}",
                "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
