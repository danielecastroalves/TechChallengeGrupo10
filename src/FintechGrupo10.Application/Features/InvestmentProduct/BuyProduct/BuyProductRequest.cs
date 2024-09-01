using FluentValidation;
using MediatR;

namespace FintechGrupo10.Application.Features.InvestmentProduct.BuyProduct
{
    public class BuyProductRequest : IRequest<bool>
    {
        public Guid ProductId { get; set; }
        public Guid ClientId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public TransactionType TransactionType { get; set; }
    }

    public class BuyProductRequestValidator : AbstractValidator<BuyProductRequest>
    {
        public BuyProductRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().NotNull();
            RuleFor(x => x.ClientId).NotEmpty().NotNull();
            RuleFor(x => x.Price).NotEmpty().NotNull();
            RuleFor(x => x.Amount).NotEmpty().NotNull();
            RuleFor(x => x.TransactionType).NotEmpty().NotNull();
        }
    }

    public enum TransactionType
    {
        Buy,
        Sale
    }
}
