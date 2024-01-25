using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Domain.Entities;
using Mapster;
using MediatR;

namespace FintechGrupo10.Application.Features.PerguntasInvestimento.CriarPergunta
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
                var entity = request.Adapt<Pergunta>();

                return await _repositorio.AddAsync(entity, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}