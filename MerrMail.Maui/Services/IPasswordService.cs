using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerrMail.Maui.Services;
public interface IPasswordService
{
    Task<string?> GetPasswordAsync();
    Task ChangePasswordAsync(string password);
}
