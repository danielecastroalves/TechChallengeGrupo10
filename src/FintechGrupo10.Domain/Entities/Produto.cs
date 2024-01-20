namespace FintechGrupo10.Domain.Entities
{
    public class Produto : Entity
    {
        public string Titulo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public string ValorMinimo { get; set; } = null!;
        public string TaxaAdministracao { get; set; } = null!;
        public string RiscoProduto { get; set; } = null!;
        //public ClientProfile PerfilProduto { get; set; }
    }
}
