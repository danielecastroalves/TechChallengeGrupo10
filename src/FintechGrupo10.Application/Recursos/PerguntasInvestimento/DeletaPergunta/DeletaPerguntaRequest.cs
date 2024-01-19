using MediatR;

namespace FintechGrupo10.Application.Recursos.PerguntasInvestimento.DeletaPergunta
{
    public class DeletaPerguntaRequest : IRequest<bool>
    {
        public Guid IdPergunta { get; set; }
    }
}
