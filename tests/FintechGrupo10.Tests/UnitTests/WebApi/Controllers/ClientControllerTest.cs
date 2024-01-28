using System.Net;
using FintechGrupo10.Application.Features.Client.AddClient;
using FintechGrupo10.Application.Features.Client.DeleteClient;
using FintechGrupo10.Application.Features.Client.GetClient;
using FintechGrupo10.Application.Features.Client.UpdateClient;
using FintechGrupo10.Domain.Entities;
using FintechGrupo10.Tests.MotherObjects.Requests;
using FintechGrupo10.WebApi.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.WebApi.Controllers
{
    public class ClientControllerTest
    {
        private readonly AutoMocker _mocker;
        private readonly Mock<IMediator> _mediator;
        private readonly ClientController _sut;

        public ClientControllerTest()
        {
            _mocker = new AutoMocker();
            _mediator = _mocker.GetMock<IMediator>();
            _sut = _mocker.CreateInstance<ClientController>();
        }

        [Fact]
        public async Task AddClientAsync_ShouldCreateNewClient_WhenRequestIsValid()
        {
            // Arrange
            var request = ClientRequestBaseMotherObject.ValidAddClientRequest();

            _mediator
                .Setup(x => x.Send(It.IsAny<AddClientRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            // Act
            var result = await _sut.AddClientAsync(request, CancellationToken.None) as ObjectResult;

            // Assert
            result?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetClientAsync_ShouldReturnClientData_WhenRequestIsValid()
        {
            // Arrange
            var request = new GetClientRequest
            {
                Documento = "123"
            };

            var response = new GetClientResponse();

            _mediator
               .Setup(x => x.Send(It.IsAny<GetClientRequest>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(response);

            // Act
            var result = await _sut.GetClientAsync(request, CancellationToken.None) as ObjectResult;

            // Assert
            result?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result?.Value.Should().Be(response);
        }

        [Fact]
        public async Task UpdateClientAsync_ShouldUpdateClientData_WhenRequestIsValid()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var request = ClientRequestBaseMotherObject.ValidUpdateClientRequest();
            var response = new ClienteEntity();

            _mediator
               .Setup(x => x.Send(It.IsAny<UpdateClientRequest>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(response);

            // Act
            var result = await _sut.UpdateClientAsync(clientId, request, CancellationToken.None) as ObjectResult;

            // Assert
            result?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteClientAsync_ValidDeleteClientData_WhenRequestIsValid()
        {
            // Arrange
            _mediator
               .Setup(x => x.Send(It.IsAny<DeleteClientRequest>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(Unit.Value);

            // Act
            var result = await _sut.DeleteClientAsync(It.IsAny<Guid>(), CancellationToken.None);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
