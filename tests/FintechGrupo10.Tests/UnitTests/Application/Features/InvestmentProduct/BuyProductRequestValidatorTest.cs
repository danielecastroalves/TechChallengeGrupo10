using FintechGrupo10.Application.Features.InvestmentProduct.BuyProduct;
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
                ClientId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                ApplicationValue = 10
            };

            var validator = new BuyProductRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
