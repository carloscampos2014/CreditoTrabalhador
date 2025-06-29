using CreditoTrabalhador.Infra.Database.SqLite.Abstractions;
using Dapper;
using FluentAssertions;
using System.Data;
using System.Data.SQLite;

namespace CreditoTrabalhador.Infra.DataBase.SqLite.Test.Abstractions;

public class DapperExecutorTests : IDisposable
{
    private readonly IDbConnection _connection;
    private readonly DapperExecutor _sut; // System Under Test

    public DapperExecutorTests()
    {
        _connection = new SQLiteConnection("Data Source=:memory:; Version=3;");
        _connection.Open();

        _sut = new DapperExecutor();
    }

    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void Execute_DadoUmComandoSqlValido_DeveExecutaLoNoBancoDeDados()
    {
        // Arrange
        var createTableSql = "CREATE TABLE Teste (Id INT, Nome TEXT);";

        // Act
        _sut.Execute(_connection, createTableSql);

        // Assert
        var tableExists = _connection.ExecuteScalar<int>(
            "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Teste';");

        tableExists.Should().Be(1, "a tabela 'Teste' deveria ter sido criada.");
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void ExecuteScalar_DadoUmaQueryQueRetornaUmValor_DeveRetornarOValorCorreto()
    {
        // Arrange
        _connection.Execute("CREATE TABLE Contagem (Id INT);");
        _connection.Execute("INSERT INTO Contagem (Id) VALUES (1);");
        _connection.Execute("INSERT INTO Contagem (Id) VALUES (2);");
        _connection.Execute("INSERT INTO Contagem (Id) VALUES (3);");

        var countSql = "SELECT COUNT(*) FROM Contagem;";

        // Act
        var result = _sut.ExecuteScalar<int>(_connection, countSql);

        // Assert
        result.Should().Be(3);
    }
}
