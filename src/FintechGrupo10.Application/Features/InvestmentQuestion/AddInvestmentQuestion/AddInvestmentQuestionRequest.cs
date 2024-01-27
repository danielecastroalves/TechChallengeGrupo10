using FintechGrupo10.Domain.Entities;
using FluentValidation;
using MediatR;

namespace FintechGrupo10.Application.Features.InvestmentQuestion.AddInvestmentQuestion
{
    public class AddInvestmentQuestionRequest : IRequest<Guid>
    {
        public string Titulo { get; set; } = null!;
        public List<Answer> Resposta { get; set; } = null!;
    }
    public class AddInvestmentQuestionRequestValidator : AbstractValidator<AddInvestmentQuestionRequest>
    {
        public AddInvestmentQuestionRequestValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().NotNull();
            RuleFor(x => x.Resposta).NotEmpty().NotNull();
        }
    }
}
