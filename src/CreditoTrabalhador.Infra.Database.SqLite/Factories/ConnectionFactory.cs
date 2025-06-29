using System.Data;
using System.Data.SQLite;

namespace CreditoTrabalhador.Infra.Database.SqLite.Factories;

public class ConnectionFactory : IConnectionFactory
{
    private readonly string _connectionString;

    /// <summary>
    /// Construtor da classe ConnectionFactory.
    /// </summary>
    /// <param name="connectionString">String de conexão com o banco de dados.</param>
    /// <exception cref="ArgumentNullException">Erro de conexão nula.</exception>
    public ConnectionFactory(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    /// <summary>
    /// Cria e abre uma conexão com o banco de dados SQLite.
    /// </summary>
    /// <returns>IDbConnection</returns>
    public IDbConnection CreateConnection()
    {
        var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
