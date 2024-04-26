using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using MerrMail.Maui.Models;
using MerrMail.Maui.Services;
using MerrMail.Maui.Views;

namespace MerrMail.Maui.ViewModels;

public partial class EmailContextsViewModel(IEmailContextService emailContextService) : BaseViewModel
{
    public ObservableCollection<EmailContext> EmailContexts { get; } = [];

    [RelayCommand]
    public async Task GetEmailContextsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            EmailContexts.Clear();
            var emailContexts = await emailContextService.GetAllAsync();

            foreach (var emailContext in emailContexts)
            {
                EmailContexts.Add(emailContext);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to get email contexts: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task GoToCreateEmailContextAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(CreateEmailContextPage)}");
    }

    [RelayCommand]
    public async Task GoToEditEmailContextAsync(EmailContext emailContext)
    {
        await Shell.Current.GoToAsync($"{nameof(EmailContextDetailsPage)}",
            new Dictionary<string, object>
            {
                { "EmailContext", emailContext },
            });
    }
}