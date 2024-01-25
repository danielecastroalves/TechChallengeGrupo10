using System.Text.Json;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FintechGrupo10.Application.Features.InvestimentQuestion.GetInvestimentQuestion
{
    public class GetInvestimentQuestionRequestHandler : IRequestHandler<GetInvestimentQuestionRequest, GetInvestimentQuestionsResponse>
    {
        private readonly IRepository<Question> _repositorio;
        private readonly ILogger<GetInvestimentQuestionRequestHandler> _logger;

        public GetInvestimentQuestionRequestHandler
        (
            IRepository<Question> repositorio,
            ILogger<GetInvestimentQuestionRequestHandler> logger
        )
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public async Task<GetInvestimentQuestionsResponse> Handle
        (
            GetInvestimentQuestionRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _repositorio.GetListByFilterAsync(x => x.Ativo, cancellationToken);

            var response = new GetInvestimentQuestionsResponse(entity.ToList());

            _logger.LogInformation(
               "[GetInvestimentQuestions] " +
               "[These are the Investiment Questions found] " +
               "[Payload: {response}]",
               JsonSerializer.Serialize(response));

            return response;
        }
    }
}
