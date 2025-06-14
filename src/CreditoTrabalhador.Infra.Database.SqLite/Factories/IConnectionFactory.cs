using System.Data;

namespace CreditoTrabalhador.Infra.Database.SqLite.Factories;

public interface IConnectionFactory
{
    /// <summary>
    /// Creates a new SQLite connection.
    /// </summary>
    /// <returns>A new SQLite connection.</returns>
    IDbConnection CreateConnection();
    
    /// <summary>
    /// Gets the connection string for the SQLite database.
    /// </summary>
    /// <returns>The connection string.</returns>
    string GetConnectionString();
}
