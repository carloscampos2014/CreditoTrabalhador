using CreditoTrabalhador.Domain.Contracts.Entities;

namespace CreditoTrabalhador.Domain.Contracts.Interfaces.Repositories.Usuarios;

public interface IUsuarioQueryRepository
{
    Task<Usuario?> GetByEmail(string email);
}
