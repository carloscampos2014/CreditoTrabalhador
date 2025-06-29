using Dapper;
using System.Data;

namespace CreditoTrabalhador.Infra.Database.SqLite.Abstractions;

public class DapperExecutor : ISqlExecutor
{

    /// <summary>
    /// Execução de comandos SQL utilizando Dapper.
    /// </summary>
    /// <param name="connection">Objeto de Conexão com banco</param>
    /// <param name="sql">Comando SQL a ser executado</param>
    /// <param name="param">Parâmetros opcionais para o comando SQL</param>
    /// <returns>Quantidade de linhas afetadas</returns>
    public int Execute(IDbConnection connection, string sql, object param = null)
    {
        return connection.Execute(sql, param);
    }

    /// <summary>
    /// Executa uma consulta SQL e retorna um único valor.
    /// </summary>
    /// <typeparam name="T">Tipo do Objeto</typeparam>
    /// <param name="connection">Objeto de Conexão com banco</param>
    /// <param name="sql">Comando SQL a ser executado</param>
    /// <param name="param">Parâmetros opcionais para o comando SQL</param>
    /// <returns>O valor retornado da consulta SQL.</returns>
    public T ExecuteScalar<T>(IDbConnection connection, string sql, object param = null)
    {
        return connection.ExecuteScalar<T>(sql, param);
    }
}
