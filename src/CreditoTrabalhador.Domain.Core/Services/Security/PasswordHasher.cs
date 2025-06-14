using CreditoTrabalhador.Domain.Contracts.Interfaces.Services.Security;

namespace CreditoTrabalhador.Domain.Core.Services.Security;

public class PasswordHasher : IPasswordHasher
{
    /// <summary>
    /// Gera um hash de senha usando o algoritmo BCrypt.
    /// </summary>
    /// <param name="password">Senha em plaintext</param>
    /// <returns>Senha em Hash</returns>
    public string Generate(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("A senha não pode ser vazia ou consistir apenas em espaços em branco.", nameof(password));
        }

        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }

    /// <summary>
    /// Verifica se a senha em plaintext corresponde ao hash fornecido.
    /// </summary>
    /// <param name="password">Senha em plaintext</param>
    /// <param name="hashedPassword">Senha em Hash</param>
    /// <returns>Se correposnde ou não</returns>
    public bool Verify(string password, string hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("A senha não pode ser vazia ou consistir apenas em espaços em branco.", nameof(password));
        }

        if (string.IsNullOrWhiteSpace(hashedPassword))
        {
            throw new ArgumentException("O hash da senha não pode ser vazio ou consistir apenas em espaços em branco.", nameof(hashedPassword));
        }

        try
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        catch (BCrypt.Net.SaltParseException ex)
        {
            throw new ArgumentException("O hash da senha fornecido é inválido ou malformado.", nameof(hashedPassword), ex);
        }
    }
}
