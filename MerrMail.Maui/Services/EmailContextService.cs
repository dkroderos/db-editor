using MerrMail.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerrMail.Maui.Services;

public class EmailContextService(ISettings settings) : IEmailContextService
{
    public Task AddAsync(EmailContext emailContext)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(EmailContext emailContext)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<EmailContext>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<EmailContext> GetAsync(int id)
    {
        throw new NotImplementedException();
    }
}
