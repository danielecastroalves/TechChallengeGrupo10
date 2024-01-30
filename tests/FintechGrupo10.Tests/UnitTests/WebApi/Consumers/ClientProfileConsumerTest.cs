using System.Text;
using FintechGrupo10.Application.Common.Configurations;
using FintechGrupo10.Application.Features.ClientProfile.SetClientProfile;
using FintechGrupo10.WebApi.Consumers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using NSubstitute;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.WebApi.Consumers
{
    public class ClientProfileConsumerTest
    {
        [Fact]
        public async Task ExecuteAsync_Should_ConsumeMessageAndInvokeMediator()
        {
            // Arrange
            var rabbitConnectionMock = new Mock<IConnection>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            IOptions<RabbitMqConfig> options = Options.Create(new RabbitMqConfig());
            var channelMock = new Mock<IModel>();

            var consumer = new ClientProfileConsumer(
                rabbitConnectionMock.Object,
                serviceProviderMock.Object,
                options
            );

            // Setup mocks
            rabbitConnectionMock.Setup(x => x.CreateModel()).Returns(channelMock.Object);

            // Act
            await consumer.StartAsync(CancellationToken.None);

            // Assert
            channelMock
                .Verify(x => x.BasicConsume(null, It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), null, It.IsAny<IBasicConsumer>()),
                Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_CreateConsumerAndConsumeMessage()
        {
            // Arrange
            var rabbitConnectionMock = new Mock<IConnection>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            IOptions<RabbitMqConfig> options = Options.Create(new RabbitMqConfig());
            var channelMock = new Mock<IModel>();

            var consumer = new ClientProfileConsumer(rabbitConnectionMock.Object, serviceProviderMock.Object, options);

            // Setup mocks
            rabbitConnectionMock.Setup(x => x.CreateModel()).Returns(channelMock.Object);

            // Act
            await consumer.StartAsync(CancellationToken.None);

            // Assert
            channelMock.Verify(x => x.BasicConsume(null, It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), null, It.IsAny<IBasicConsumer>()), Times.Once);

            // Clean up
            await consumer.StopAsync(CancellationToken.None);
        }
    }
}
