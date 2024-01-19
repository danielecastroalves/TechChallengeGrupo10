using FintechGrupo10.Domain.Entidades;
using MediatR;

namespace FintechGrupo10.Application.Recursos.PerguntasInvestimento.BuscarPerguntas
{
    public class GetPerguntasInvestimentoRequest : IRequest<List<Pergunta>>
    {
    }
}
