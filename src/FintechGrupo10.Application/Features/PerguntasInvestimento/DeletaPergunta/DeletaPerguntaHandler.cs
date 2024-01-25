using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Domain.Entities;
using MediatR;

namespace FintechGrupo10.Application.Features.PerguntasInvestimento.DeletaPergunta
{
    public class DeletaPerguntaHandler : IRequestHandler<DeletaPerguntaRequest, bool>
    {
        private readonly IRepository<Pergunta> _repositorio;

        public DeletaPerguntaHandler(IRepository<Pergunta> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> Handle(DeletaPerguntaRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return await _repositorio.DeleteByIdAsync(request.QuestionId, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
