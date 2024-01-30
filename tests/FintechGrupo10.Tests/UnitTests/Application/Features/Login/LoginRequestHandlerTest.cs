using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FintechGrupo10.Application.Common.Auth.Token;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.Login;
using FintechGrupo10.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.Login
{
    public class LoginRequestHandlerTest
    {
        [Fact]
        public async Task Handle_ValidUser_ReturnsToken()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger<LoginRequestHandler>>();
            var tokenServiceMock = new Mock<ITokenService>();

            var handler = new LoginRequestHandler(userRepositoryMock.Object, loggerMock.Object, tokenServiceMock.Object);

            var request = new LoginRequest
            {
                Login = "validUser",
                Password = "validPassword"
            };

            var cancellationToken = new CancellationToken();

            var user = new User(); // Replace with your actual User class
            userRepositoryMock.Setup(repo => repo.GetAuthByLoginAndPassword(request.Login, request.Password, cancellationToken))
                .ReturnsAsync(user);

            tokenServiceMock.Setup(service => service.GetUserToken(user))
                .Returns("fakeToken"); // Replace with the expected token

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.Equal("fakeToken", result);
        }

        [Fact]
        public async Task Handle_InvalidUser_ReturnsEmptyString()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger<LoginRequestHandler>>();
            var tokenServiceMock = new Mock<ITokenService>();

            var handler = new LoginRequestHandler(userRepositoryMock.Object, loggerMock.Object, tokenServiceMock.Object);

            var request = new LoginRequest
            {
                Login = "invalidUser",
                Password = "invalidPassword"
            };

            var cancellationToken = new CancellationToken();

            userRepositoryMock.Setup(repo => repo.GetAuthByLoginAndPassword(request.Login, request.Password, cancellationToken))
                .ReturnsAsync((User)null);

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.Equal(string.Empty, result);
        }
    }
}
