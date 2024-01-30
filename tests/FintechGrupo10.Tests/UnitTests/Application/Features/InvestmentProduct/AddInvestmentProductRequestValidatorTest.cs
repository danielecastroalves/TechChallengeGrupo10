using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FintechGrupo10.Application.Features.InvestmentProduct.AddInvestmentProduct;
using FluentAssertions;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.InvestmentProduct
{
    public class AddInvestmentProductRequestValidatorTest
    {
        [Theory]
        [InlineData(null, "Some Description", "1000", "1%", "Low", "Conservative")]
        public void Validate_InvalidInput_ShouldFail(string titulo, string descricao, string valorMinimo, string taxaAdministracao, string riscoProduto, string perfilInvestimento)
        {
            // Arrange
            var request = new AddInvestmentProductRequest
            {
                Titulo = titulo,
                Descricao = descricao,
                ValorMinimo = valorMinimo,
                TaxaAdministracao = taxaAdministracao,
                RiscoProduto = riscoProduto,
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
