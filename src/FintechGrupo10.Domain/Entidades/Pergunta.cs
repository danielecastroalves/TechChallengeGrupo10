namespace FintechGrupo10.Domain.Entidades
{
    public class Pergunta : EntidadeBase
    {
        public string Titulo { get; set; } = null!;
        public List<Resposta> Resposta  { get; set; }
    }
}
