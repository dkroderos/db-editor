using MerrMail.Maui.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MerrMail.Maui.Services;
public interface IAccountService
{
    Task AddAsync(Account account);
    Task<IEnumerable<Account>> GetAllAsync();
    Task<Account?> GetByIdAsync(int id);
    Task RemoveAsync(int id);
    Task UpdateAsync(Account account);
}
