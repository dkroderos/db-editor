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
    public async Task AddAsync(EmailContext emailContext)
    {
        var path = await SecureStorage.Default.GetAsync("database");
        var connectionString = $@"Data Source=file:{path}";

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = "INSERT INTO EmailContexts (Subject, Response, DateCreated, LastUpdated, Editor) " +
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
        var path = await SecureStorage.Default.GetAsync("database");
        var connectionString = $@"Data Source=file:{path}";

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = "DELETE FROM EmailContexts WHERE Id = @Id";
        using var command = new SqliteCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task EditAsync(EmailContext emailContext)
    {
        var path = await SecureStorage.Default.GetAsync("database");
        var connectionString = $@"Data Source=file:{path}";

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = "UPDATE EmailContexts SET Subject = @Subject, Response = @Response, DateCreated = @DateCreated, LastUpdated = @LastUpdated, Editor = @Editor WHERE Id = @Id";
        var command = new SqliteCommand(query, connection);

        command.Parameters.AddWithValue("@Id", emailContext.Id);
        command.Parameters.AddWithValue("@Subject", emailContext.Subject);
        command.Parameters.AddWithValue("@Response", emailContext.Response);
        command.Parameters.AddWithValue("@DateCreated", emailContext.DateCreated);
        command.Parameters.AddWithValue("@LastUpdated", DateTime.Now.ToString());
        command.Parameters.AddWithValue("@Editor", emailContext.Editor);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<EmailContext>> GetAllAsync()
    {
        var path = await SecureStorage.Default.GetAsync("database");
        var connectionString = $@"Data Source=file:{path}";
        var emailContexts = new List<EmailContext>();

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = $"SELECT * FROM EmailContexts";
        using var command = new SqliteCommand(query, connection);
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

    public async Task<EmailContext?> GetAsync(int id)
    {
        var path = await SecureStorage.Default.GetAsync("database");
        var connectionString = $@"Data Source=file:{path}";

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = "SELECT * FROM EmailContexts WHERE Id = @Id";
        using var command = new SqliteCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

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

            return emailContext;
        }

        return null;
    }
}
