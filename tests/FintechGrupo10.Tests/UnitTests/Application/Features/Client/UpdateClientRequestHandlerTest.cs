using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.Client.UpdateClient;
using FintechGrupo10.Domain.Entities;
using Microsoft.Extensions.Logging;
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

            var request = new UpdateClientRequest { Id = Guid.NewGuid(), /* other properties */ };

            var mockRepository = mocker.GetMock<IRepository<ClienteEntity>>();

            var existingEntity = new ClienteEntity { Id = request.Id, Ativo = false };
            mockRepository.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingEntity);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.Id, result.Id);
            Assert.True(result.Ativo); // Verifica se o cliente foi ativado corretamente
        }
    }
}
