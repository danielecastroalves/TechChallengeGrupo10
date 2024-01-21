using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Domain.Entities;
using MediatR;

namespace FintechGrupo10.Application.Recursos.PerguntasInvestimento.BuscarPerguntas
{
    public class BuscarPerguntasInvestimentoHandler : IRequestHandler<BuscarPerguntasInvestimentoRequest, List<Pergunta>>
    {
        private readonly IRepository<Pergunta> _repositorio;

        public BuscarPerguntasInvestimentoHandler(IRepository<Pergunta> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Pergunta>> Handle(BuscarPerguntasInvestimentoRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var perguntas = await _repositorio.GetListByFilterAsync(x => x.Ativo, cancellationToken);

            return perguntas.ToList();
        }
    }
}
