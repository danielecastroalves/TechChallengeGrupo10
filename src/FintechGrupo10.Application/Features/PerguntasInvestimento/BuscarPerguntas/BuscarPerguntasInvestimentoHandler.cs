using System.Text.Json;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FintechGrupo10.Application.Features.PerguntasInvestimento.BuscarPerguntas
{
    public class BuscarPerguntasInvestimentoHandler : IRequestHandler<BuscarPerguntasInvestimentoRequest, GetInvestingQuestionsResponse>
    {
        private readonly IRepository<Pergunta> _repositorio;
        private readonly ILogger<BuscarPerguntasInvestimentoHandler> _logger;

        public BuscarPerguntasInvestimentoHandler
        (
            IRepository<Pergunta> repositorio,
            ILogger<BuscarPerguntasInvestimentoHandler> logger
        )
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public async Task<GetInvestingQuestionsResponse> Handle
        (
            BuscarPerguntasInvestimentoRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _repositorio.GetListByFilterAsync(x => x.Ativo, cancellationToken);

            var response = new GetInvestingQuestionsResponse(entity.ToList());

            _logger.LogInformation(
               "[GetInvestingQuestions] " +
               "[These are the Investing Questions found] " +
               "[Payload: {response}]",
               JsonSerializer.Serialize(response));

            return response;
        }
    }
}
