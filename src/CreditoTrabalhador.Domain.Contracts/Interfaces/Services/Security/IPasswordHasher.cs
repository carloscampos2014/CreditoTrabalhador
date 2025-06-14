namespace CreditoTrabalhador.Domain.Contracts.Interfaces.Services.Security;

public interface IPasswordHasher
{
    /// <summary>
    /// Gera um hash de senha usando o algoritmo BCrypt.
    /// </summary>
    /// <param name="password">Senha em plaintext</param>
    /// <returns>Senha em Hash</returns>
    string Generate(string password);

    /// <summary>
    /// Vrrifica se a senha em plaintext corresponde ao hash fornecido.
    /// </summary>
    /// <param name="password">Senha em plaintext</param>
    /// <param name="hashedPassword">Senha em Hash</param>
    /// <returns>Se correposnde ou não</returns>
    bool Verify(string password, string hashedPassword);
}
