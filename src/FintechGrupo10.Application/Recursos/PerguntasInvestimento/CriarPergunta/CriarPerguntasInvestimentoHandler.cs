using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Domain.Entities;
using MediatR;

namespace FintechGrupo10.Application.Recursos.PerguntasInvestimento.CriarPergunta
{
    public class CriarPerguntasInvestimentoHandler : IRequestHandler<CriarPerguntasInvestimentoRequest, Guid>
    {
        private readonly IRepository<Pergunta> _repositorio;

        public CriarPerguntasInvestimentoHandler(IRepository<Pergunta> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Guid> Handle(CriarPerguntasInvestimentoRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return await _repositorio.AddAsync(request.Pergunta, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
