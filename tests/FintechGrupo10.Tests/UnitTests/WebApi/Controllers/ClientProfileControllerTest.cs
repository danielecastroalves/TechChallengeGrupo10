using FintechGrupo10.WebApi.Controllers;
using MediatR;
using Moq.AutoMock;
using Moq;
using Xunit;
using FintechGrupo10.Application.Features.ClientProfile.SendClientProfile;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace FintechGrupo10.Tests.UnitTests.WebApi.Controllers
{
    public class ClientProfileControllerTest
    {
        private readonly AutoMocker _mocker;
        private readonly Mock<IMediator> _mediator;
        private readonly ClientProfileController _sut;

        public ClientProfileControllerTest()
        {
            _mocker = new AutoMocker();
            _mediator = _mocker.GetMock<IMediator>();
            _sut = _mocker.CreateInstance<ClientProfileController>();
        }

        [Fact]
        public async Task AddClientAsync_ShouldCreateNewClient_WhenRequestIsValid()
        {
            // Arrange
            var request = new SendClientProfileRequest();

            _mediator
                .Setup(x => x.Send(It.IsAny<SendClientProfileRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            // Act
            var result = await _sut.SendClientProfile(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
