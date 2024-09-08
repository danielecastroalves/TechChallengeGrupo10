using FintechGrupo10.Application.Features.InvestmentProduct.BuyProduct;
using FintechGrupo10.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.InvestmentProduct
{
    public class BuyProductRequestValidatorTest
    {
        [Fact]
        public void Validate_BuyProductRequest_ShouldBeSuccess()
        {
            // Arrange
            var request = new BuyProductRequest
            {
                IdCliente = Guid.NewGuid(),
                IdProduto = Guid.NewGuid(),
                Quantidade = 10,
                Preco = 100,
                TipoTransacao = nameof(TransactionType.Compra)
            };

            var validator = new BuyProductRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
