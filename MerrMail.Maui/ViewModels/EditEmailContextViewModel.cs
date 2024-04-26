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
public partial class EditEmailContextViewModel(IEmailContextService emailContextService) : BaseViewModel
{
    [ObservableProperty]
    private EmailContext? emailContext;

    [ObservableProperty]
    public int id;

    [ObservableProperty]
    public string? subject;

    [ObservableProperty]
    public string? response;

    [ObservableProperty]
    public string? dateCreated;

    [ObservableProperty]
    public string? lastUpdated;

    [ObservableProperty]
    public string? editor;

    [RelayCommand]
    public async Task GetEmailContextAsync()
    {
        if (EmailContext is null || IsBusy)
            return;

        try
        {
            IsBusy = true;

            var emailContext = await emailContextService.GetAsync(EmailContext!.Id);

            if (emailContext is null) return;

            Id = emailContext.Id;
            Subject = emailContext.Subject;
            Response = emailContext.Response;
            DateCreated = emailContext.DateCreated;
            LastUpdated = EmailContext.LastUpdated;
            Editor = emailContext.Editor;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to get email context: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task EditEmailContextAsync()
    {
        if (EmailContext is null || IsBusy) return;

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

        if (string.IsNullOrEmpty(Editor))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Editor cannot be empty",
                "Ok");
            return;
        }

        bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert("Are you sure?",
            "Are you sure you want to edit this new Email Context?",
            "Yes", "No");

        if (!isConfirmed)
            return;

        try
        {
            IsBusy = true;

            var emailContext = new EmailContext
            {
                Id = Id,
                Subject = Subject,
                Response = Response,
                DateCreated = DateTime.Now.ToString(),
                LastUpdated = DateTime.Now.ToString(),
                Editor = Editor,
            };

            await emailContextService.EditAsync(emailContext);

            await Shell.Current.CurrentPage.DisplayAlert("Success",
                "Email Context successfully edited",
                "Ok");

            await Shell.Current.GoToAsync($"//{nameof(EmailContextsPage)}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);

            await Shell.Current.CurrentPage.DisplayAlert("Error",
                $"Unable to edit this Email Context: ${ex.Message}",
                "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
