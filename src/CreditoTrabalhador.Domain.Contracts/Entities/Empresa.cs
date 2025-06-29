namespace CreditoTrabalhador.Domain.Contracts.Entities;

public class Empresa : EmpresaBase
{

    public ICollection<Filial> Filiais { get; set; } = new List<Filial>();
}
