using FintechGrupo10.Domain.Entidades;

namespace FintechGrupo10.Domain.Eventos
{
    public class EventoPerfil
    {
        public string Documento { get; set; } = null!;
        public List<Pergunta> PerguntasRespondidas { get; set; } = null!;
    }
}
