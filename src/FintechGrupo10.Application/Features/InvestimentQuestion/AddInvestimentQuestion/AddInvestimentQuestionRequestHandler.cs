using System.Text.Json;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FintechGrupo10.Application.Features.InvestmentQuestion.AddInvestmentQuestion
{
    public class AddInvestmentQuestionRequestHandler : IRequestHandler<AddInvestmentQuestionRequest, Guid>
    {
        private readonly IRepository<QuestionEntity> _repositorio;
        private readonly ILogger<AddInvestmentQuestionRequestHandler> _logger;

        public AddInvestmentQuestionRequestHandler
        (
            IRepository<QuestionEntity> repositorio,
            ILogger<AddInvestmentQuestionRequestHandler> logger
        )
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public async Task<Guid> Handle(AddInvestmentQuestionRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = request.Adapt<QuestionEntity>();

            var response = await _repositorio.AddAsync(entity, cancellationToken);

            _logger.LogInformation(
                "[AddInvestmentQuestion] " +
                "[Question has been added successfully] " +
                "[QuestionId: {questionId}] " +
                "[Payload: {entity}]",
                response.ToString(),
                JsonSerializer.Serialize(entity));

            return response;
        }
    }
}
