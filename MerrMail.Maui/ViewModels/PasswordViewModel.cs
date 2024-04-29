using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MerrMail.Maui.Services;
using MerrMail.Maui.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MerrMail.Maui.ViewModels;

public partial class PasswordViewModel(IPasswordService passwordService) : BaseViewModel
{
    [ObservableProperty]
    public string? currentPassword;

    [ObservableProperty]
    public string? newPassword;

    [ObservableProperty]
    public string? repeatPassword;

    [RelayCommand]
    private async Task ChangePasswordAsync()
    {
        if (IsBusy) return;

        if (string.IsNullOrEmpty(CurrentPassword))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Current password cannot be empty",
                "Ok");
            return;
        }

        if (string.IsNullOrEmpty(NewPassword))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Password cannot be empty",
                "Ok");
            return;
        }

        if (NewPassword != RepeatPassword)
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Passwords do not match",
                "Ok");
            return;
        }

        bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert("Are you sure?",
            "Are you sure you want to change the password of the database?",
            "Yes", "No");

        if (!isConfirmed)
            return;

        try
        {
            IsBusy = true;

            var dbPassword = await passwordService.GetPasswordAsync();

            if (CurrentPassword != dbPassword)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Error",
                    "Database password incorrect",
                    "Ok");
                return;
            }

            await passwordService.ChangePasswordAsync(NewPassword);
            await Shell.Current.DisplayAlert("Success", "Password changed", "Ok");

            await Shell.Current.GoToAsync($"..");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to add email account: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
}