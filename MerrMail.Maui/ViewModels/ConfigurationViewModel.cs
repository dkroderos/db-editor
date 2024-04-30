using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MerrMail.Maui.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MerrMail.Maui.ViewModels;

public partial class ConfigurationViewModel : BaseViewModel
{
    [ObservableProperty]
    public string header;

    [ObservableProperty]
    public string introduction;

    [ObservableProperty]
    public string conclusion;

    [ObservableProperty]
    public string closing;

    [ObservableProperty]
    public string signature;

    [ObservableProperty]
    public string oAuthClientCredentialsFilePath;

    [ObservableProperty]
    public string accessTokenDirectoryPath;

    [ObservableProperty]
    public string hostAddress;

    [ObservableProperty]
    public string hostPassword;

    [ObservableProperty]
    public string dataStorageAccess;

    [ObservableProperty]
    public string acceptanceScore = "0.35";

    [RelayCommand]
    private async Task ChooseDatabaseAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var database = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Choose the credentials file"
            });

            if (database is null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Cancelled",
                    "Choosing database file cancelled", "Ok");
                return;
            }

            if (!database.FileName.EndsWith("db", StringComparison.OrdinalIgnoreCase))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid File",
                    $"Credentials must be a database file", "OK");
                return;
            }

            DataStorageAccess = database.FullPath;

            await Shell.Current.CurrentPage.DisplayAlert("Success",
                $"Database set to: {DataStorageAccess}", "Ok");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to choose database: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ChooseOAuthClientCredentialsFilePathAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var credentials = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Choose the credentials file"
            });

            if (credentials is null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Cancelled",
                    "Choosing credentials file cancelled", "Ok");
                return;
            }

            if (!credentials.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid File",
                    $"Credentials must be a json file", "OK");
                return;
            }

            OAuthClientCredentialsFilePath = credentials.FullPath;

            await Shell.Current.CurrentPage.DisplayAlert("Success",
                $"Credentials set to: {OAuthClientCredentialsFilePath}", "Ok");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to choose credentials: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ChooseAccessTokenDirectoryAsync()
    {
        if (this.IsBusy)
            return;

        try
        {
            this.IsBusy = true;

            var accessTokenDirectory = await FolderPicker.PickAsync(default);

            if (accessTokenDirectory.Folder is null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Cancelled",
                    "Choosing folder cancelled", "Ok");
                return;
            }

            AccessTokenDirectoryPath = accessTokenDirectory.Folder.Path;

            await Shell.Current.CurrentPage.DisplayAlert("Success",
                $"Access token folder set to: {AccessTokenDirectoryPath}", "Ok");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Failed to choose folder: {ex.Message}", "Ok");
        }
        finally
        {
            this.IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task CreateConfigurationAsync()
    {
        if (IsBusy)
            return;

        if (string.IsNullOrEmpty(Header))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Header cannot be empty",
                "Ok");
            return;
        }

        if (string.IsNullOrEmpty(Introduction))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Introduction cannot be empty",
                "Ok");
            return;
        }

        if (string.IsNullOrEmpty(Conclusion))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Conclusion cannot be empty",
                "Ok");
            return;
        }

        if (string.IsNullOrEmpty(Closing))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Closing cannot be empty",
                "Ok");
            return;
        }

        if (string.IsNullOrEmpty(Signature))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Signature cannot be empty",
                "Ok");
            return;
        }

        if (string.IsNullOrEmpty(OAuthClientCredentialsFilePath))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "OAuthClientCredentialsFilePath cannot be empty",
                "Ok");
            return;
        }

        if (string.IsNullOrEmpty(AccessTokenDirectoryPath))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Access Token Directory Path cannot be empty",
                "Ok");
            return;
        }

        if (string.IsNullOrEmpty(HostAddress))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Host Address cannot be empty",
                "Ok");
            return;
        }

        try
        {
            _ = new MailAddress(HostAddress);
        }
        catch (FormatException)
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Invalid email address",
                "Ok");
            return;
        }

        if (string.IsNullOrEmpty(HostPassword))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "HostPassword cannot be empty",
                "Ok");
            return;
        }

        if (string.IsNullOrEmpty(DataStorageAccess))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "DataStorageAccess cannot be empty",
                "Ok");
            return;
        }

        if (!double.TryParse(AcceptanceScore, out _))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error",
                "Acceptance score must be between 0 and 1",
                "Ok");
            return;
        }

        bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert("Are you sure?",
            "Are you sure you want to create this configuration?",
            "Yes", "No");

        if (!isConfirmed)
            return;

        try
        {
            IsBusy = true;

            string configJson = $@"
{{
  ""Logging"": {{
    ""LogLevel"": {{
      ""Default"": ""Information"",
      ""Microsoft.Hosting.Lifetime"": ""Information""
    }}
  }},
  ""EmailReplyOptions"": {{
    ""Header"": ""{Header.Replace("\\", "\\\\")}"",
    ""Introduction"": ""{Introduction.Replace("\\", "\\\\")}"",
    ""Conclusion"": ""{Conclusion.Replace("\\", "\\\\")}"",
    ""Closing"": ""{Closing.Replace("\\", "\\\\")}"",
    ""Signature"": ""{Signature.Replace("\\", "\\\\")}""
  }},
  ""EmailApiOptions"": {{
    ""OAuthClientCredentialsFilePath"": ""{OAuthClientCredentialsFilePath.Replace("\\", "\\\\")}"",
    ""AccessTokenDirectoryPath"": ""{AccessTokenDirectoryPath.Replace("\\", "\\\\")}"",
    ""HostAddress"": ""{HostAddress.Replace("\\", "\\\\")}"",
    ""HostPassword"": ""{HostPassword.Replace("\\", "\\\\")}""
  }},
  ""DataStorageOptions"": {{
    ""DataStorageType"": 1,
    ""DataStorageAccess"": ""{DataStorageAccess.Replace("\\", "\\\\")}""
  }},
  ""EmailAnalyzerOptions"": {{
    ""AcceptanceScore"": {AcceptanceScore}
  }}
}}";

            using var stream = new MemoryStream(Encoding.Default.GetBytes(configJson));
            var result = await FileSaver.Default.SaveAsync("appsettings.json", stream, default);

            if (result.IsSuccessful)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Success",
                    "Configuration successfully created:", "Ok");
                await Shell.Current.GoToAsync($"..");
            }
            else
                await Shell.Current.CurrentPage.DisplayAlert("Failed",
                    "Configuration was not saved successfully", "Ok");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to create configuration: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
