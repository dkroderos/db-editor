using MerrMail.Maui.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MerrMail.Maui.Services;

public class AccountService : IAccountService
{
    public async Task AddAsync(Account account)
    {
        var path = await SecureStorage.Default.GetAsync("database");
        var connectionString = $@"Data Source=file:{path}";

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = "INSERT INTO Accounts (Name, Password) VALUES (@Name, @Password)";

        using var command = new SqliteCommand(query, connection);

        command.Parameters.AddWithValue("@Name", account.Name);
        command.Parameters.AddWithValue("@Password", account.Password);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        var path = await SecureStorage.Default.GetAsync("database");
        var connectionString = $@"Data Source=file:{path}";
        var accounts = new List<Account>();

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = "SELECT * FROM Accounts";

        using var command = new SqliteCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var account = new Account
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Password = reader.GetString(2)
            };

            accounts.Add(account);
        }

        return accounts;
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        var path = await SecureStorage.Default.GetAsync("database");
        var connectionString = $@"Data Source=file:{path}";

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = "SELECT * FROM Accounts WHERE Id = @Id";

        using var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var account = new Account
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Password = reader.GetString(2)
            };

            return account;
        }

        return null;
    }

    public async Task RemoveAsync(int id)
    {
        var path = await SecureStorage.Default.GetAsync("database");
        var connectionString = $@"Data Source=file:{path}";

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = "DELETE FROM Accounts WHERE Id = @Id";

        using var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(Account account)
    {
        var path = await SecureStorage.Default.GetAsync("database");
        var connectionString = $@"Data Source=file:{path}";

        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var query = "UPDATE Accounts SET Name = @Name, Password = @Password WHERE Id = @Id";

        using var command = new SqliteCommand(query, connection);

        command.Parameters.AddWithValue("@Id", account.Id);
        command.Parameters.AddWithValue("@Name", account.Name);
        command.Parameters.AddWithValue("@Password", account.Password);

        await command.ExecuteNonQueryAsync();
    }
}
