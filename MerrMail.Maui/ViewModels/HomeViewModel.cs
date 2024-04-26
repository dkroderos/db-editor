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
    private readonly ISettings settings;

    public HomeViewModel(ISettings settings)
    {
        this.settings = settings;
    }

    [RelayCommand]
    private async Task GoToEmailContextsAsync()
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

            settings.Path = file.FullPath;

            await Shell.Current.GoToAsync($"//{nameof(EmailContextsPage)}");
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
}
