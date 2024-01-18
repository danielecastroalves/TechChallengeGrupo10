namespace FintechGrupo10.Domain.Entidades
{
    public class Produto : EntidadeBase
    {
        public string Titulo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public string ValorMinimo { get; set; } = null!;
        public string TaxaAdministracao { get; set; } = null!;
        public string RiscoProduto { get; set; } = null!;
        //public ClientProfile PerfilProduto { get; set; }
    }
}
