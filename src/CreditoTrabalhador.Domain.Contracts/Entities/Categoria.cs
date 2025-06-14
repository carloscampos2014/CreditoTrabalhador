namespace CreditoTrabalhador.Domain.Contracts.Entities;

public class Categoria
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UsuarioId { get; set; }

    public Usuario Usuario { get; set; } = null!;

    public string Codigo { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;
}
