using FintechGrupo10.Domain.Enums;
using FluentValidation;
using MediatR;

namespace FintechGrupo10.Application.Features.InvestmentProduct.BuyProduct
{
    public class BuyProductRequest : IRequest<bool>
    {
        public Guid IdProduto { get; set; }
        public Guid IdCliente { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }

    public class BuyProductRequestValidator : AbstractValidator<BuyProductRequest>
    {
        public BuyProductRequestValidator()
        {
            RuleFor(x => x.IdProduto).NotEmpty().NotNull();
            RuleFor(x => x.IdCliente).NotEmpty().NotNull();
            RuleFor(x => x.Quantidade).NotEmpty().NotNull();
            RuleFor(x => x.Preco).NotEmpty().NotNull();
        }
    }
}
