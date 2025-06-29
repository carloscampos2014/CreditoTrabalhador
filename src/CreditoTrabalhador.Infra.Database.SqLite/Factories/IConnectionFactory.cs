using System.Data;

namespace CreditoTrabalhador.Infra.Database.SqLite.Factories;

public interface IConnectionFactory
{
    /// <summary>
    /// Cria e abre uma conexão com o banco de dados SQLite.
    /// </summary>
    /// <returns>IDbConnection</returns>
    IDbConnection CreateConnection();
   
}
