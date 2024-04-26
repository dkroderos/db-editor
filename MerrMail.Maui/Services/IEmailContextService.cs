using MerrMail.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerrMail.Maui.Services;

public interface IEmailContextService
{
    Task<EmailContext> GetAsync(int id);
    Task<IEnumerable<EmailContext>> GetAllAsync();
    Task AddAsync(EmailContext emailContext);
    Task DeleteAsync(int id);
    Task EditAsync(EmailContext emailContext);
}
