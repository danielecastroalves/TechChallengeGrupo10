using System.Linq.Expressions;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.Client.DeleteClient;
using FintechGrupo10.Domain.Entities;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.Client
{
    public class DeleteClientRequestHandlerTest
    {
        [Fact]
        public async Task Handle_ValidClientId_DeactivatesClientAndLogs()
        {
            // Arrange
            var mocker = new AutoMocker();
            var handler = mocker.CreateInstance<DeleteClientRequestHandler>();

            var clientId = Guid.NewGuid();
            var request = new DeleteClientRequest(clientId);

            var mockRepository = mocker.GetMock<IRepository<ClienteEntity>>();

            var existingEntity = new ClienteEntity { Id = clientId, Ativo = true };
            mockRepository.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingEntity);

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>(), It.IsAny<ClienteEntity>(), It.IsAny<CancellationToken>()));

            Assert.False(existingEntity.Ativo); // Verifica se o cliente foi desativado corretamente
        }
    }
}
