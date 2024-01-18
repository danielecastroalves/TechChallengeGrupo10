using FintechGrupo10.Domain.Entidades;
using MediatR;

namespace FintechGrupo10.Application.Recursos.PerguntasInvestimento.CriarPergunta
{
    public class CriarPerguntasInvestimentoRequest : IRequest<Guid>
    {
        public Pergunta Pergunta { get; set; }
    }
}
