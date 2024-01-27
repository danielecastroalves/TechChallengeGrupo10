using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.InvestmentQuestion.AddInvestmentQuestion;
using FintechGrupo10.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FintechGrupo10.Application.Features.InvestmentQuestion.DeleteInvestmentQuestion
{
    public class DeleteInvestmentQuestionRequestHandler : IRequestHandler<DeleteInvestmentQuestionRequest>
    {
        private readonly IRepository<QuestionEntity> _repositorio;
        private readonly ILogger<AddInvestmentQuestionRequestHandler> _logger;

        public DeleteInvestmentQuestionRequestHandler
        (
            IRepository<QuestionEntity> repositorio,
            ILogger<AddInvestmentQuestionRequestHandler> logger
        )
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteInvestmentQuestionRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _repositorio.DeleteByIdAsync(request.QuestionId, cancellationToken);

            _logger.LogInformation(
                "[DeleteInvestmentQuestion] " +
                "[Question has been deleted successfully] " +
                "[QuestionId: {questionId}]",
                request.QuestionId.ToString());

            return Unit.Value;
        }
    }
}
