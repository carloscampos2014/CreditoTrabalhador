using CreditoTrabalhador.Infra.Database.SqLite.Factories;
using FluentAssertions;
using System.Data;
using System.Data.SQLite;

namespace CreditoTrabalhador.Infra.DataBase.SqLite.Test.Factories;

public class ConnectionFactoryTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public void Constructor_QuandoConnectionStringEhNula_DeveLancarArgumentNullException()
    {
        // Arrange 
        string connectionStringNula = null;

        // Act 
        Action act = () => new ConnectionFactory(connectionStringNula);

        // Assert 
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName("connectionString");
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Constructor_QuandoConnectionStringEhValida_DeveCriarInstanciaComSucesso()
    {
        // Arrange
        var connectionStringValida = "Data Source=:memory:;";

        // Act
        Action act = () => new ConnectionFactory(connectionStringValida);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void CreateConnection_DeveRetornarUmaConexaoAbertaComOBancoDeDados()
    {
        // Arrange
        var inMemoryConnectionString = "Data Source=:memory:; Version=3;";
        var factory = new ConnectionFactory(inMemoryConnectionString);

        // Act
        using var connection = factory.CreateConnection();

        // Assert
        connection.Should().NotBeNull("a conexão não deve ser nula.");
        connection.Should().BeOfType<SQLiteConnection>("o tipo de conexão deve ser do SQLite.");
        connection.State.Should().Be(ConnectionState.Open, "a conexão deve estar no estado 'Aberta'.");
    }
}