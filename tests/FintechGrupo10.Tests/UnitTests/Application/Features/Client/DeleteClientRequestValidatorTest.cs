using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FintechGrupo10.Application.Features.Client.DeleteClient;
using FluentAssertions;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.Client
{
    public class DeleteClientRequestValidatorTest
    {
        [Fact]
        public void Should_Have_Error_When_ClientID_Is_Default()
        {
            // Arrange
            var validator = CreateValidator();
            var request = new DeleteClientRequest(Guid.Empty);

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().Contain(error => error.PropertyName == nameof(DeleteClientRequest.ClientID));
        }

        [Fact]
        public void Should_Not_Have_Error_When_ClientID_Is_Valid()
        {
            // Arrange
            var validator = CreateValidator();
            var request = new DeleteClientRequest(Guid.NewGuid());

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        private DeleteClientRequestValidator CreateValidator()
        {
            var mocker = new AutoMocker();
            var validator = mocker.CreateInstance<DeleteClientRequestValidator>();
            return validator;
        }
    }
}
