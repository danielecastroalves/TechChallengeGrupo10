using System.Text.Json;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FintechGrupo10.Application.Features.InvestmentQuestion.GetInvestmentQuestion
{
    public class GetInvestmentQuestionRequestHandler : IRequestHandler<GetInvestmentQuestionRequest, GetInvestmentQuestionsResponse>
    {
        private readonly IRepository<QuestionEntity> _repositorio;
        private readonly ILogger<GetInvestmentQuestionRequestHandler> _logger;

        public GetInvestmentQuestionRequestHandler
        (
            IRepository<QuestionEntity> repositorio,
            ILogger<GetInvestmentQuestionRequestHandler> logger
        )
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public async Task<GetInvestmentQuestionsResponse> Handle
        (
            GetInvestmentQuestionRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _repositorio.GetListByFilterAsync(x => x.Ativo, cancellationToken);

            var response = new GetInvestmentQuestionsResponse(entity.ToList());

            _logger.LogInformation(
               "[GetInvestmentQuestion] " +
               "[These are the Investment Questions found] " +
               "[Payload: {response}]",
               JsonSerializer.Serialize(response));

            return response;
        }
    }
}
