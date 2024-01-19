using FintechGrupo10.Domain.DTOS;
using MediatR;

namespace FintechGrupo10.Application.Recursos.PerguntasInvestimento.ResponderPerguntas
{
    public class ResponderPerguntasInvestimentoRequest : IRequest<bool>
    {
        public string Documento { get; set; }
        public List<PerguntaRespondidaDTO> PerguntasRespondidas { get; set; }
    }
}
