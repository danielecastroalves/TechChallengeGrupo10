namespace FintechGrupo10.Domain.Entidades
{
    public class EntidadeBase
    {
        public Guid Id { get; set; }
        public DateTime DataInsercao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public bool Ativo { get; set; }
    }    
}
