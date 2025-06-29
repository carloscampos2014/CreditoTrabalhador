using System.Data;

namespace CreditoTrabalhador.Infra.Database.SqLite.Abstractions;

public interface ISqlExecutor
{
    /// <summary>
    /// Execução de comandos SQL.
    /// </summary>
    /// <param name="connection">Objeto de Conexão com banco</param>
    /// <param name="sql">Comando SQL a ser executado</param>
    /// <param name="param">Parâmetros opcionais para o comando SQL</param>
    /// <returns>Quantidade de linhas afetadas</returns>
    int Execute(IDbConnection connection, string sql, object param = null);

    /// <summary>
    /// Executa uma consulta SQL e retorna um único valor.
    /// </summary>
    /// <typeparam name="T">Tipo do Objeto</typeparam>
    /// <param name="connection">Objeto de Conexão com banco</param>
    /// <param name="sql">Comando SQL a ser executado</param>
    /// <param name="param">Parâmetros opcionais para o comando SQL</param>
    /// <returns>O valor retornado da consulta SQL.</returns>
    T ExecuteScalar<T>(IDbConnection connection, string sql, object param = null);
}