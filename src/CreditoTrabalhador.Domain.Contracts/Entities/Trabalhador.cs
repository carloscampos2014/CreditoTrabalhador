namespace CreditoTrabalhador.Domain.Contracts.Entities;

public class Trabalhador
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid EmpresaId { get; set; }

    public Empresa Empresa { get; set; } = null!;

    public Guid? FilialId { get; set; }

    public Filial? Filial { get; set; } = null!;

    public string Nome { get; set; } = string.Empty;

    public string Cpf { get; set; } = string.Empty;
}
