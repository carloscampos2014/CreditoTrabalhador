using CreditoTrabalhador.Domain.Contracts.Resources;
using Dapper;
using DotNetEnv;
using System.Data;
using System.Data.SQLite;

namespace CreditoTrabalhador.Infra.Database.SqLite.Factories;

public class ConnectionFactory
{
    private readonly string _connectionString;
    public ConnectionFactory()
    {
        Env.Load();
        _connectionString = $"Data Source={Env.GetString("DB_NAME")}; Version=3;";
    }
    
    /// <summary>
    /// Creates a new SQLite connection.
    /// </summary>
    /// <returns>A new SQLite connection.</returns>
    public IDbConnection CreateConnection()
    {
        var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        EnsureUsuarioTable(connection);
        return connection;
    }

    /// <summary>
    /// Gets the connection string for the SQLite database.
    /// </summary>
    /// <returns>The connection string.</returns>
    public string GetConnectionString() => _connectionString;

    private static void EnsureUsuarioTable(IDbConnection connection)
    {
        var tableExists = connection.ExecuteScalar<int>(
            "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Usuarios';") > 0;

        if (tableExists)
        {
            return;
        }

        connection.Execute(TableScripts.UsuarioCreateSql);
    }
}
