using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.Entidades;
using MediatR;

namespace FintechGrupo10.Application.Recursos.PerguntasInvestimento.CriarPergunta
{
    public class CriarPerguntasInvestimentoHandler : IRequestHandler<CriarPerguntasInvestimentoRequest, Guid>
    {
        private readonly IRepositorio<Pergunta> _repositorio;

        public CriarPerguntasInvestimentoHandler(IRepositorio<Pergunta> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Guid> Handle(CriarPerguntasInvestimentoRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return await _repositorio.AdicionarAsync(request.Pergunta, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
