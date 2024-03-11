using FluentValidation;
using MediatR;

namespace FintechGrupo10.Application.Features.InvestmentProduct.BuyProduct
{
    public class BuyProductRequest : IRequest<bool>
    {
        public Guid ProductId { get; set; }
        public decimal ApplicationValue { get; set; }
        public Guid ClientId { get; set; }
    }

    public class BuyProductRequestValidator : AbstractValidator<BuyProductRequest>
    {
        public BuyProductRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().NotNull();
            RuleFor(x => x.ApplicationValue).NotEmpty().NotNull();
            RuleFor(x => x.ClientId).NotEmpty().NotNull();
        }
    }
}
