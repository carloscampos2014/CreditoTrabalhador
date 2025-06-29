using CreditoTrabalhador.Domain.Contracts.Resources;
using CreditoTrabalhador.Infra.Database.SqLite.Abstractions;
using CreditoTrabalhador.Infra.Database.SqLite.Factories;

namespace CreditoTrabalhador.Infra.Database.SqLite.Config;

public class DatabaseInitializer
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly ISqlExecutor _sqlExecutor; // <- Nova dependência

    /// <summary>
    /// Constructor para inicializar o DatabaseInitializer.
    /// </summary>
    /// <param name="connectionFactory">Objeto de conexão.</param>
    /// <param name="sqlExecutor">Executor SQL.</param>
    /// <exception cref="ArgumentNullException">Erro de Argumento nulo quando parâmetros inválidos</exception>
    public DatabaseInitializer(IConnectionFactory connectionFactory, ISqlExecutor sqlExecutor)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        _sqlExecutor = sqlExecutor ?? throw new ArgumentNullException(nameof(sqlExecutor)); // <- Injetada
    }

    /// <summary>
    /// Verifica se banco se não cria as tabelas necessárias
    /// </summary>
    public void EnsureDatabaseSchema()
    {
        using var connection = _connectionFactory.CreateConnection();

        var tableExists = _sqlExecutor.ExecuteScalar<int>(connection,
            "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Usuarios';") > 0;

        if (!tableExists)
        {
            _sqlExecutor.Execute(connection, TableScripts.UsuarioCreateSql);
        }
    }
}
