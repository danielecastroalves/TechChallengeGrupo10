using System.Linq.Expressions;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.Client.UpdateClient;
using FintechGrupo10.Domain.Entities;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.Client
{
    public class UpdateClientRequestHandlerTest
    {
        [Fact]
        public async Task Handle_ValidRequest_ReturnsUpdatedEntityAndLogs()
        {
            // Arrange
            var mocker = new AutoMocker();
            var handler = mocker.CreateInstance<UpdateClientRequestHandler>();

            var existingEntity = new ClienteEntity();
            var request = new UpdateClientRequest { Id = existingEntity.Id };

            var mockRepository = mocker.GetMock<IRepository<ClienteEntity>>();

            existingEntity.SetUsuarioInativo();
            mockRepository.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingEntity);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Ativo);
        }
    }
}
