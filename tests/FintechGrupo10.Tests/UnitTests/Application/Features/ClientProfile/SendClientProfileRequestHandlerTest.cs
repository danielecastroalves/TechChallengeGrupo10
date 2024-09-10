using FintechGrupo10.Application.Common.Configurations;
using FintechGrupo10.Application.Common.Services;
using FintechGrupo10.Application.Features.ClientProfile.SendClientProfile;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.ClientProfile
{
    public class SendClientProfileRequestHandlerTest
    {
        [Fact]
        public void Handle_ValidRequest_ShouldPublishMessage()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SendClientProfileRequestHandler>>();
            var messagePublisherServiceMock = new Mock<IMessagePublisherService>();
            IOptions<RabbitMqConfig> options = Options.Create(new RabbitMqConfig());

            var handler = new SendClientProfileRequestHandler(
                loggerMock.Object,
                messagePublisherServiceMock.Object,
                options
            );

            var request = new SendClientProfileRequest();

            // Act
            handler.Handle(request, CancellationToken.None);

            // Assert
            messagePublisherServiceMock.Verify(
                x => x.PublishMessage(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>()),
                Times.Once
            );
        }
    }
}
