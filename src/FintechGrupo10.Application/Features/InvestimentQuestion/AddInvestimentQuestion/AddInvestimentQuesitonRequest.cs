using FintechGrupo10.Domain.Entities;
using FluentValidation;
using MediatR;

namespace FintechGrupo10.Application.Features.InvestimentQuestion.AddInvestimentQuestion
{
    public class AddInvestimentQuesitonRequest : IRequest<Guid>
    {
        public string Titulo { get; set; } = null!;
        public List<Answer> Resposta { get; set; } = null!;
    }
    public class AddInvestimentQuesitonRequestValidator : AbstractValidator<AddInvestimentQuesitonRequest>
    {
        public AddInvestimentQuesitonRequestValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().NotNull();
            RuleFor(x => x.Resposta).NotEmpty().NotNull();
        }
    }
}
