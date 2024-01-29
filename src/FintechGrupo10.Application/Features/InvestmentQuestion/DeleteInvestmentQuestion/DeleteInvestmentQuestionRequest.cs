using FluentValidation;
using MediatR;

namespace FintechGrupo10.Application.Features.InvestmentQuestion.DeleteInvestmentQuestion
{
    public class DeleteInvestmentQuestionRequest : IRequest
    {
        public DeleteInvestmentQuestionRequest(Guid questionId)
        {
            QuestionId = questionId;
        }

        public Guid QuestionId { get; set; }
    }

    public class DeleteInvestmentQuestionRequestValidator : AbstractValidator<DeleteInvestmentQuestionRequest>
    {
        public DeleteInvestmentQuestionRequestValidator()
        {
            RuleFor(x => x.QuestionId).NotEmpty().NotNull();
        }
    }
}
