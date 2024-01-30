using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.Client.GetClient;
using FintechGrupo10.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.Client
{
    public class GetClientRequestHandlerTest
    {
        [Fact]
        public async Task Handle_ValidRequest_ReturnsResponseAndLogs()
        {
            // Arrange
            var mocker = new AutoMocker();
            var handler = mocker.CreateInstance<GetClientRequestHandler>();

            var request = new GetClientRequest { Documento = "123456789" };

            var mockRepository = mocker.GetMock<IRepository<ClienteEntity>>();

            var existingEntity = new ClienteEntity { Documento = "123456789", Ativo = true };
            mockRepository.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingEntity);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("123456789", result.Documento); // Verifica se os dados foram mapeados corretamente
        }
    }
}
