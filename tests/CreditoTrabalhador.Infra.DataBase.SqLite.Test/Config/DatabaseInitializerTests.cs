using CreditoTrabalhador.Domain.Contracts.Resources;
using CreditoTrabalhador.Infra.Database.SqLite.Abstractions;
using CreditoTrabalhador.Infra.Database.SqLite.Config;
using CreditoTrabalhador.Infra.Database.SqLite.Factories;
using Moq;
using System.Data;

namespace CreditoTrabalhador.Infra.DataBase.SqLite.Test.Config;

public class DatabaseInitializerTests
{
    private readonly Mock<IConnectionFactory> _mockConnectionFactory;
    private readonly Mock<ISqlExecutor> _mockSqlExecutor; 
    private readonly DatabaseInitializer _sut;

    public DatabaseInitializerTests()
    {
        _mockConnectionFactory = new Mock<IConnectionFactory>();
        _mockSqlExecutor = new Mock<ISqlExecutor>();

        _mockConnectionFactory.Setup(f => f.CreateConnection())
                              .Returns(new Mock<IDbConnection>().Object);

        _sut = new DatabaseInitializer(_mockConnectionFactory.Object, _mockSqlExecutor.Object);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void EnsureDatabaseSchema_QuandoTabelaNaoExiste_DeveExecutarComandoDeCriacao()
    {
        // Arrange
        _mockSqlExecutor.Setup(executor => executor.ExecuteScalar<int>(It.IsAny<IDbConnection>(), It.IsAny<string>(), null))
                        .Returns(0); 

        // Act
        _sut.EnsureDatabaseSchema();

        // Assert
        _mockSqlExecutor.Verify(executor => executor.Execute(It.IsAny<IDbConnection>(), TableScripts.UsuarioCreateSql, null),
            Times.Once());
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void EnsureDatabaseSchema_QuandoTabelaJaExiste_NaoDeveExecutarComandoDeCriacao()
    {
        // Arrange
        _mockSqlExecutor.Setup(executor => executor.ExecuteScalar<int>(It.IsAny<IDbConnection>(), It.IsAny<string>(), null))
                        .Returns(1); // Simula: tabela já existe

        // Act
        _sut.EnsureDatabaseSchema();

        // Assert
        _mockSqlExecutor.Verify(executor => executor.Execute(It.IsAny<IDbConnection>(), It.IsAny<string>(), null),
            Times.Never());
    }
}