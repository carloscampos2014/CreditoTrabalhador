namespace CreditoTrabalhador.Domain.Contracts.Entities;

public class Filial : EmpresaBase
{
    public Guid EmpresaId { get; set; }

    public Empresa Empresa { get; set; } = null!;
}
