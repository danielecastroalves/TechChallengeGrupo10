using System.ComponentModel;
using System.Reflection;
using FintechGrupo10.Domain.Enums;
using FluentValidation;
using MediatR;

namespace FintechGrupo10.Application.Features.InvestmentProduct.AddInvestmentProduct
{
    public class AddInvestmentProductRequest : IRequest<Guid>
    {
        public string Titulo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public decimal ValorMinimo { get; set; }
        public string TaxaAdministracao { get; set; } = null!;
        public string RiscoProduto { get; set; } = null!;
        public string TipoAtivo { get; set; } = null!;
        public string CodigoAtivo { get; set; } = null!;
        public string PerfilInvestimento { get; set; } = null!;
    }

    public class AddInvestmentProductRequestValidator : AbstractValidator<AddInvestmentProductRequest>
    {
        public AddInvestmentProductRequestValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().NotNull();
            RuleFor(x => x.Descricao).NotEmpty().NotNull();
            RuleFor(x => x.ValorMinimo).NotEmpty().NotNull();
            RuleFor(x => x.TaxaAdministracao).NotEmpty().NotNull();
            RuleFor(x => x.RiscoProduto).NotEmpty().NotNull();
            RuleFor(x => x.TipoAtivo).Must(IsValidActiveTypeValue);
            RuleFor(x => x.CodigoAtivo).NotEmpty().NotNull();
            RuleFor(x => x.PerfilInvestimento).Must(BeValidEnumValue);
        }

        private bool BeValidEnumValue(string value) => Enum.TryParse(typeof(InvestorProfile), value, out _);

        private bool IsValidActiveTypeValue(string value)
        {
            return Enum.GetValues(typeof(ActiveType))
                .Cast<ActiveType>()
                .Any(e => GetEnumDescription(e).Equals(value, StringComparison.OrdinalIgnoreCase));
        }

        private static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute?.Description ?? value.ToString();
        }
    }
}
