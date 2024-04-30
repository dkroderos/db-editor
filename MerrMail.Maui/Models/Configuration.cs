using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerrMail.Maui.Models;

public class Configuration
{
    public string Header { get; set; } = string.Empty;
    public string Introduction { get; set; } = string.Empty;
    public string Conclusion { get; set; } = string.Empty;
    public string Closing { get; set; } = string.Empty;
    public string Signature { get; set; } = string.Empty;
    public string OAuthClientCredentialsFilePath { get; set; } = string.Empty;
    public string AccessTokenDirectoryPath { get; set; } = string.Empty;
    public string HostAddress { get; set; } = string.Empty;
    public string HostPassword { get; set; } = string.Empty;
    public string DataStorageAccess { get; set; } = string.Empty;
    public string AcceptanceScore { get; set; } = string.Empty;
}
