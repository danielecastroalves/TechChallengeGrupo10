using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.Client.AddClient;
using FintechGrupo10.Domain.Entities;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.Client
{
    public class AddClientRequestHandlerTest
    {
        [Fact]
        public async Task Handle_ValidRequest_ClientAddedSuccessfully()
        {
            // Arrange
            var mocker = new AutoMocker();
            var handler = mocker.CreateInstance<AddClientRequestHandler>();

            var request = new AddClientRequest(/* provide valid request data */);

            mocker.GetMock<IRepository<ClienteEntity>>()
                .Setup(repo => repo.AddAsync(It.IsAny<ClienteEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            mocker.GetMock<IRepository<ClienteEntity>>()
                .Verify(repo => repo.AddAsync(It.IsAny<ClienteEntity>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ThrowsOperationCanceledException()
        {
            // Arrange
            var mocker = new AutoMocker();
            var handler = mocker.CreateInstance<AddClientRequestHandler>();

            var request = new AddClientRequest(/* provide valid request data */);
            var cancellationToken = new CancellationToken(true);

            // Act and Assert
            await Assert.ThrowsAsync<OperationCanceledException>(() => handler.Handle(request, cancellationToken));
        }
    }
}
