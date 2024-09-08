using FintechGrupo10.Application.Features.InvestmentProduct.AddInvestmentProduct;
using FluentAssertions;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.InvestmentProduct
{
    public class AddInvestmentProductRequestValidatorTest
    {
        [Theory]
        [InlineData(null, "Some Description", 1000, "1%", "Low", "Criptomoedas", "BTC", "Conservative")]
        public void Validate_InvalidInput_ShouldFail
        (
            string titulo,
            string descricao,
            decimal valorMinimo,
            string taxaAdministracao,
            string riscoProduto,
            string tipoAtivo,
            string codigoAtivo,
            string perfilInvestimento
        )
        {
            // Arrange
            var request = new AddInvestmentProductRequest
            {
                Titulo = titulo,
                Descricao = descricao,
                ValorMinimo = valorMinimo,
                TaxaAdministracao = taxaAdministracao,
                RiscoProduto = riscoProduto,
                TipoAtivo = tipoAtivo,
                CodigoAtivo = codigoAtivo,
                PerfilInvestimento = perfilInvestimento
            };

            var validator = new AddInvestmentProductRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
        }
    }
}
