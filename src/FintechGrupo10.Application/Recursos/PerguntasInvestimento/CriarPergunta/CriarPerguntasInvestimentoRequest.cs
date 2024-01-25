using FintechGrupo10.Domain.Entities;
using MediatR;

namespace FintechGrupo10.Application.Recursos.PerguntasInvestimento.CriarPergunta
{
    public class CriarPerguntasInvestimentoRequest : IRequest<Guid>
    {
        public string Titulo { get; set; } = null!;
        public List<Resposta> Resposta { get; set; } = null!;
    }
}
