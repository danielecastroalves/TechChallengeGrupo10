using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.InvestmentProduct.AddInvestmentProduct;
using FintechGrupo10.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.InvestmentProduct
{
    public class AddInvestmentProductRequestHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldAddProductSuccessfully()
        {
            // Arrange
            var request = new AddInvestmentProductRequest();
            var cancellationToken = new CancellationToken();

            var repositoryMock = new Mock<IRepository<ProductEntity>>();
            repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<ProductEntity>(), cancellationToken))
                          .ReturnsAsync(Guid.NewGuid());

            var loggerMock = new Mock<ILogger<AddInvestmentProductRequestHandler>>();

            var handler = new AddInvestmentProductRequestHandler(repositoryMock.Object, loggerMock.Object);

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<ProductEntity>(), cancellationToken), Times.Once);
        }
    }
}
