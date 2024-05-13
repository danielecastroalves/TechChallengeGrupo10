using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FintechGrupo10.Application.Common.Configurations;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Common.Services;
using FintechGrupo10.Application.Features.ClientProfile.SendClientProfile;
using FintechGrupo10.Application.Features.ClientProfile.SendClientProfileCommand;
using FintechGrupo10.Application.Features.InvestmentProduct.AddInvestmentProduct;
using FintechGrupo10.Application.Features.InvestmentProduct.BuyProduct;
using FintechGrupo10.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.InvestmentProduct
{
    public class BuyProductRequestHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldBuyProductSuccessfully()
        {
            // Arrange
            var request = new BuyProductRequest();
            var cancellationToken = new CancellationToken();
            var loggerMock = new Mock<ILogger<BuyProductRequestHandler>>();
            var messagePublisherServiceMock = new Mock<IMessagePublisherService>();
            var repositoryMock = new Mock<IRepository<ClienteEntity>>();

            IOptions<RabbitMqConfig> options = Options.Create(new RabbitMqConfig());
                        
            repositoryMock.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>(), cancellationToken))
                          .ReturnsAsync(new ClienteEntity { Id = Guid.NewGuid()});

            var handler = new BuyProductRequestHandler(loggerMock.Object,
                messagePublisherServiceMock.Object,
                options,
                repositoryMock.Object);

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            messagePublisherServiceMock.Verify(
                x => x.PublishMessage(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>()),
                Times.Once
            );

            repositoryMock.Verify(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>(), cancellationToken), Times.Once);          
        }

        [Fact]
        public async Task Handle_ShouldNotBuyProductSuccessfully()
        {
            // Arrange
            var request = new BuyProductRequest();
            var cancellationToken = new CancellationToken();
            var loggerMock = new Mock<ILogger<BuyProductRequestHandler>>();
            var messagePublisherServiceMock = new Mock<IMessagePublisherService>();
            var repositoryMock = new Mock<IRepository<ClienteEntity>>();

            IOptions<RabbitMqConfig> options = Options.Create(new RabbitMqConfig());

            var handler = new BuyProductRequestHandler(loggerMock.Object,
                messagePublisherServiceMock.Object,
                options,
                repositoryMock.Object);

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            messagePublisherServiceMock.Verify(
                x => x.PublishMessage(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>()),
                Times.Never
            );

            repositoryMock.Verify(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>(), cancellationToken), Times.Once);
        }
    }
}
