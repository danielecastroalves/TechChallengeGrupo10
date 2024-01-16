using FintechGrupo10.Domain.Entidades;

namespace FintechGrupo10.Domain.DTOS
{
    public class PerguntasResponditasDTO
    {
        public string Documento { get; set; }
        public List<Pergunta> Perguntas { get; set; }
    }
}
