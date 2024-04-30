using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MerrMail.Maui.Models;
using MerrMail.Maui.Services;
using System.Diagnostics;

namespace MerrMail.Maui.ViewModels;

public partial class CreateEmailContextViewModel(IPasswordService passwordService, IAccountService accountService, IEmailContextService emailContextService) : BaseViewModel
{
    [ObservableProperty]
    public string? subject;

    [ObservableProperty]
    public string? response;

    [ObservableProperty]
    public string? name;

    [ObservableProperty]
    public string? password;

    [ObservableProperty]
    public string databasePassword;

    [RelayCommand]
    public async Task CreateEmailContextAsync()
    {
        if (IsBusy) return;

        if (string.IsNullOrEmpty(Subject))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Subject cannot be empty",
                "Ok");
            return;
        }

        if (string.IsNullOrEmpty(Response))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Response cannot be empty",
                "Ok");
            return;
        }

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

        if (string.IsNullOrEmpty(DatabasePassword))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Database password cannot be empty",
                "Ok");
            return;
        }

        bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert("Are you sure?",
            "Are you sure you want to create this new Email Context?",
            "Yes", "No");

        if (!isConfirmed)
            return;

        try
        {
            IsBusy = true;

            var accounts = await accountService.GetAllAsync();
            var account = accounts.Where(a => a.Name == Name).FirstOrDefault();

            if (account is null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Error",
                    "Account not found",
                    "Ok");
                return;
            }

            if (account.Password != Password)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Error",
                    "Incorrect password",
                    "Ok");
                return;
            }

            var dbPassword = await passwordService.GetPasswordAsync();

            if (DatabasePassword != dbPassword)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Error",
                    "Database password incorrect",
                    "Ok");
                return;
            }

            var emailContext = new EmailContext
            {
                Subject = Subject,
                Response = Response,
                DateCreated = DateTime.Now.ToString(),
                LastUpdated = DateTime.Now.ToString(),
                Editor = Name,
            };

            await emailContextService.AddAsync(emailContext);

            await Shell.Current.CurrentPage.DisplayAlert("Success",
                "Email Context successfully created",
                "Ok");

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);

            await Shell.Current.CurrentPage.DisplayAlert("Error",
                $"Unable to create this Email Context: ${ex.Message}",
                "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
}