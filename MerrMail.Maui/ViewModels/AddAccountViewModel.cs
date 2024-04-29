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
public partial class AddAccountViewModel(IEmailContextService emailContextService, IAccountService accountService) : BaseViewModel
{
    [ObservableProperty]
    public string name;

    [ObservableProperty]
    public string password;

    [ObservableProperty]
    public string repeatPassword;

    [ObservableProperty]
    public string databasePassword;

    [RelayCommand]
    private async Task AddAccountAsync()
    {
        if (IsBusy)
            return;

        if (string.IsNullOrEmpty(Name))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Name cannot be empty",
                "Ok");
            return;
        }

        if (string.IsNullOrEmpty(Password))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Password cannot be empty",
                "Ok");
            return;
        }

        if (Password != RepeatPassword)
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Passwords do not match",
                "Ok");
            return;
        }

        bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert("Are you sure?",
            "Are you sure you want to create this new Account?",
            "Yes", "No");

        if (!isConfirmed)
            return;

        try
        {
            IsBusy = true;

            var dbPassword = await emailContextService.GetPasswordAsync();

            if (DatabasePassword != dbPassword)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Error",
                    "Database password incorrect",
                    "Ok");
                return;
            }

            var accounts = await accountService.GetAllAsync();

            var accountExists = accounts.Any(a => a.Name == Name);
            if (accountExists)
            {
                await Shell.Current.DisplayAlert("Error", $"Account named {Name} already exists", "Ok");
                return;
            }

            var account = new Account
            {
                Name = Name,
                Password = Password,
            };

            await accountService.AddAsync(account);
            await Shell.Current.DisplayAlert("Success", $"Account created", "Ok");

            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
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
