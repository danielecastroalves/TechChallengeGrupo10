using FintechGrupo10.Domain.Enums;

namespace FintechGrupo10.Domain.Entities
{
    public class ProductEntity : Entity
    {
        public string Titulo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public string ValorMinimo { get; set; } = null!;
        public string TaxaAdministracao { get; set; } = null!;
        public string RiscoProduto { get; set; } = null!;

        public InvestorProfile PerfilInvestimento { get; set; }
    }
}
