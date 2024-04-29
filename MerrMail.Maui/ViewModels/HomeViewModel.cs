using CommunityToolkit.Mvvm.Input;
using MerrMail.Maui.Models;
using MerrMail.Maui.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerrMail.Maui.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    [RelayCommand]
    private async Task SetDatabaseAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var file = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick a database file"
            });

            if (file is null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Cancelled",
                    "Editing email contexts cancelled", "Ok");
                return;
            }

            if (!file.FileName.EndsWith("db", StringComparison.OrdinalIgnoreCase))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid File!",
                    $"File chosen is not a database file", "OK");
                return;
            }

            await SecureStorage.Default.SetAsync("database", file.FullPath);

            var database = await SecureStorage.Default.GetAsync("database");

            await Shell.Current.CurrentPage.DisplayAlert("Success",
                $"Database set to: {database}", "Ok");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to edit email contexts {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GoToAddAccountAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var database = await SecureStorage.Default.GetAsync("database");

            if (string.IsNullOrWhiteSpace(database))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Error",
                    $"No database set", "Ok");
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(AddAccountPage)}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to add account: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GoToEmailContextsAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var database = await SecureStorage.Default.GetAsync("database");

            if (string.IsNullOrWhiteSpace(database))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Error",
                    $"No database set", "Ok");
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(EmailContextsPage)}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to edit email contexts: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GoToPasswordAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var database = await SecureStorage.Default.GetAsync("database");

            if (string.IsNullOrWhiteSpace(database))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Error",
                    $"No database set", "Ok");
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(PasswordPage)}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to change database password: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
