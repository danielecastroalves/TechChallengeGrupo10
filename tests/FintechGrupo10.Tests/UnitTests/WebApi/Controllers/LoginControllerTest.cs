using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FintechGrupo10.Application.Features.ClientProfile.SendClientProfile;
using FintechGrupo10.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using Moq;
using Xunit;
using FintechGrupo10.Application.Features.Login;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FintechGrupo10.Tests.UnitTests.WebApi.Controllers
{
    public class LoginControllerTest
    {
        private readonly AutoMocker _mocker;
        private readonly Mock<IMediator> _mediator;
        private readonly LoginController _sut;

        public LoginControllerTest()
        {
            _mocker = new AutoMocker();
            _mediator = _mocker.GetMock<IMediator>();
            _sut = _mocker.CreateInstance<LoginController>();
        }

        [Fact]
        public async Task LoginControlle_Login()
        {
            // Arrange
            var request = new LoginRequest() { Login = "user", Password = "123"};

            _mediator
                .Setup(x => x.Send(It.IsAny<LoginRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("token");

            // Act
            var result = await _sut.Authenticate(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<ActionResult<dynamic>>();
        }

        [Fact]
        public async Task LoginControlle_InvalidUser()
        {
            // Arrange
            var request = new LoginRequest() { Login = "user", Password = "123" };

            _mediator
                .Setup(x => x.Send(It.IsAny<LoginRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("");

            // Act
            var result = await _sut.Authenticate(request, CancellationToken.None);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
