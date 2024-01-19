using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.Entidades;
using MediatR;

namespace FintechGrupo10.Application.Recursos.PerguntasInvestimento.BuscarPerguntas
{
    public class BuscarPerguntasInvestimentoHandler : IRequestHandler<GetPerguntasInvestimentoRequest, List<Pergunta>>
    {
        private readonly IRepositorio<Pergunta> _repositorio;

        public BuscarPerguntasInvestimentoHandler(IRepositorio<Pergunta> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Pergunta>> Handle(GetPerguntasInvestimentoRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var perguntas = await _repositorio.ObterListaPorFiltroAsync(x => x.Ativo, cancellationToken);

            return perguntas.ToList();
        }
    }
}
