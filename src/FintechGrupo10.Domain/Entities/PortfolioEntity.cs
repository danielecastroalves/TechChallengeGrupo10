namespace FintechGrupo10.Domain.Entities
{
    public class PortfolioEntity : Entity
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public List<Wallet>? Ativos { get; set; }
    }

    public class Wallet
    {
        public Guid ProductId { get; set; }
    }
}
