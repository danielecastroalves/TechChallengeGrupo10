using FintechGrupo10.Domain.Entities;

namespace FintechGrupo10.Domain.DTOS
{
    public class PerguntasResponditasDTO
    {
        public string Documento { get; set; } = null!;
        public List<Pergunta> Perguntas { get; set; } = null!;
    }
}
