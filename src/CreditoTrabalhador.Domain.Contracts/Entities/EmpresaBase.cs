using CreditoTrabalhador.Domain.Contracts.Enums;

namespace CreditoTrabalhador.Domain.Contracts.Entities
{
    public abstract class EmpresaBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UsuarioId { get; set; }

        public Usuario Usuario { get; set; } = null!;

        public string Razao { get; set; } = string.Empty;

        public Personalidade Personalidade { get; set; } = Personalidade.PessoaJuridica;

        public string CpfCnpj { get; set; } = string.Empty;
    }
}
