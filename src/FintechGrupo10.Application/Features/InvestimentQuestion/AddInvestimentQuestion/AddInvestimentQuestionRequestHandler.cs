using System.Text.Json;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FintechGrupo10.Application.Features.InvestimentQuestion.AddInvestimentQuestion
{
    public class AddInvestimentQuestionRequestHandler : IRequestHandler<AddInvestimentQuesitonRequest, Guid>
    {
        private readonly IRepository<Question> _repositorio;
        private readonly ILogger<AddInvestimentQuestionRequestHandler> _logger;

        public AddInvestimentQuestionRequestHandler
        (
            IRepository<Question> repositorio,
            ILogger<AddInvestimentQuestionRequestHandler> logger
        )
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public async Task<Guid> Handle(AddInvestimentQuesitonRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = request.Adapt<Question>();

            var response = await _repositorio.AddAsync(entity, cancellationToken);

            _logger.LogInformation(
                "[AddInvestimentQuestion] " +
                "[Question has been added successfully] " +
                "[QuestionId: {questionId}] " +
                "[Payload: {entity}]",
                response.ToString(),
                JsonSerializer.Serialize(entity));

            return response;
        }
    }
}
