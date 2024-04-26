using MerrMail.Maui.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerrMail.Maui.Services;

public class EmailContextService : IEmailContextService
{
    private readonly ISettings settings;

    public EmailContextService(ISettings settings)
    {
        this.settings = settings;
    }

    public async Task AddAsync(EmailContext emailContext)
    {
        var connectionString = $@"Data Source=file:{settings.Path}";

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = "INSERT INTO EmailContext (Subject, Response, DateCreated, LastUpdated, Editor) " +
            "VALUES (@Subject, @Response, @DateCreated, @LastUpdated, @Editor)";

        using var command = new SqliteCommand(query, connection);

        command.Parameters.AddWithValue("@Subject", emailContext.Subject);
        command.Parameters.AddWithValue("@Response", emailContext.Response);
        command.Parameters.AddWithValue("@DateCreated", emailContext.DateCreated);
        command.Parameters.AddWithValue("@LastUpdated", emailContext.LastUpdated);
        command.Parameters.AddWithValue("@Editor", emailContext.Editor);

        await command.ExecuteNonQueryAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var connectionString = $@"Data Source=file:{settings.Path}";

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = "DELETE FROM EmailContext WHERE Id = @Id";
        using var command = new SqliteCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task EditAsync(EmailContext emailContext)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<EmailContext>> GetAllAsync()
    {
        var connectionString = $@"Data Source=file:{settings.Path}";
        var emailContexts = new List<EmailContext>();

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        using var command = new SqliteCommand($"SELECT * FROM EmailContext", connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var emailContext = new EmailContext
            {
                Id = reader.GetInt32(0),
                Subject = reader.GetString(1),
                Response = reader.GetString(2),
                DateCreated = reader.GetString(3),
                LastUpdated = reader.GetString(4),
                Editor = reader.GetString(5),
            };

            emailContexts.Add(emailContext);
        }

        return emailContexts;
    }

    public Task<EmailContext> GetAsync(int id)
    {
        throw new NotImplementedException();
    }
}
