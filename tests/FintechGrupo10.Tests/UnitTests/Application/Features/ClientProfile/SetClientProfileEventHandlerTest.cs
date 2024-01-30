using System.Linq.Expressions;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.ClientProfile;
using FintechGrupo10.Application.Features.ClientProfile.SetClientProfile;
using FintechGrupo10.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Recursos.DefinePerfil
{
    public class SetClientProfileEventHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_Success()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var questions = new List<Question>
            {
                new() { QuestionValue = 10 },
                new() { QuestionValue = 15 },
            };

            var setClientProfileEvent = new SetClientProfileEvent
            {
                ClientId = clientId,
                Questions = questions,
            };

            var repositoryMock = new Mock<IRepository<ClienteEntity>>();
            var loggerMock = new Mock<ILogger<SetClientProfileEventHandler>>();

            repositoryMock.Setup(x => x.GetByFilterAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ClienteEntity());

            var handler = new SetClientProfileEventHandler(repositoryMock.Object, loggerMock.Object);

            // Act
            await handler.Handle(setClientProfileEvent, CancellationToken.None);

            // Assert
            repositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
            repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>(), It.IsAny<ClienteEntity>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ThrowsOperationCanceledException()
        {
            // Arrange
            var setClientProfileEvent = new SetClientProfileEvent
            {
                ClientId = Guid.NewGuid(),
                Questions = new List<Question>(),
            };

            var repositoryMock = new Mock<IRepository<ClienteEntity>>();
            var loggerMock = new Mock<ILogger<SetClientProfileEventHandler>>();

            var handler = new SetClientProfileEventHandler(repositoryMock.Object, loggerMock.Object);

            var cancellationToken = new CancellationToken(true); // Cancellation requested

            // Act & Assert
            await Assert.ThrowsAsync<OperationCanceledException>(() => handler.Handle(setClientProfileEvent, cancellationToken));
        }
    }
}
