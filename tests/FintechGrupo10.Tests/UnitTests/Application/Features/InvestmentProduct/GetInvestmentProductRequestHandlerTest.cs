using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.InvestmentProduct.GetInvestmentProduct;
using FintechGrupo10.Application.Features.InvestmentQuestion.GetInvestmentQuestion;
using FintechGrupo10.Domain.Entities;
using FintechGrupo10.Domain.Enums;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.InvestmentProduct
{
    public class GetInvestmentProductRequestHandlerTest
    {
        [Fact]
        public async Task Handle_ReturnsExpectedInvestmentProductResponse()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository<ProductEntity>>();
            var loggerMock = new Mock<ILogger<GetInvestmentQuestionRequestHandler>>();

            var handler = new GetInvestmentProductRequestHandler(repositoryMock.Object, loggerMock.Object);

            var request = new GetInvestmentProductRequest
            {
                InvestorProfile = InvestorProfile.Conservador
            };

            var cancellationToken = new CancellationToken();

            var productList = new List<ProductEntity>
            {
                new ProductEntity ()
            };
            repositoryMock.Setup(x => x.GetListByFilterAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>(), cancellationToken))
                          .ReturnsAsync(productList);

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productList, result.Products);
        }
    }
}
