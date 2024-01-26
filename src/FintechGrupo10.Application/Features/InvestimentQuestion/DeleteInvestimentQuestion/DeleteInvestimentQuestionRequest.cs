using FluentValidation;
using MediatR;

namespace FintechGrupo10.Application.Features.InvestimentQuestion.DeleteInvestimentQuestion
{
    public class DeleteInvestimentQuestionRequest : IRequest
    {
        public DeleteInvestimentQuestionRequest(Guid questionId)
        {
            QuestionId = questionId;
        }

        public Guid QuestionId { get; set; }
    }

    public class DeleteInvestimentQuestionRequestValidator : AbstractValidator<DeleteInvestimentQuestionRequest>
    {
        public DeleteInvestimentQuestionRequestValidator()
        {
            RuleFor(x => x.QuestionId).NotEmpty().NotNull();
        }
    }
}
