using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.InvestimentQuestion.AddInvestimentQuestion;
using FintechGrupo10.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FintechGrupo10.Application.Features.InvestimentQuestion.DeleteInvestimentQuestion
{
    public class DeleteInvestimentQuestionRequestHandler : IRequestHandler<DeleteInvestimentQuestionRequest>
    {
        private readonly IRepository<QuestionEntity> _repositorio;
        private readonly ILogger<AddInvestimentQuestionRequestHandler> _logger;

        public DeleteInvestimentQuestionRequestHandler
        (
            IRepository<QuestionEntity> repositorio,
            ILogger<AddInvestimentQuestionRequestHandler> logger
        )
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteInvestimentQuestionRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _repositorio.DeleteByIdAsync(request.QuestionId, cancellationToken);

            _logger.LogInformation(
                "[DeleteInvestimentQuestion] " +
                "[Question has been deleted successfully] " +
                "[QuestionId: {questionId}]",
                request.QuestionId.ToString());

            return Unit.Value;
        }
    }
}
