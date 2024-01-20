namespace FintechGrupo10.Domain.Entities
{
    public class Pergunta : Entity
    {
        public string Titulo { get; set; } = null!;
        public List<Resposta> Resposta { get; set; } = null!;
    }
}
