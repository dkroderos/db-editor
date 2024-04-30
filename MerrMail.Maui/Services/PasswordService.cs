using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerrMail.Maui.Services;

public class PasswordService : IPasswordService
{
    public async Task<string?> GetPasswordAsync()
    {
        var path = await SecureStorage.Default.GetAsync("database");
        var connectionString = $@"Data Source=file:{path}";

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = "SELECT Password FROM Password";
        using var command = new SqliteCommand(query, connection);

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var password = reader.GetString(0);
            return password;
        }

        return null;
    }

    public async Task ChangePasswordAsync(string password)
    {
        var path = await SecureStorage.Default.GetAsync("database");
        var connectionString = $@"Data Source=file:{path}";

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = "UPDATE Password SET Password = @NewPassword";
        using var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@NewPassword", password);

        await command.ExecuteNonQueryAsync();
    }
}
