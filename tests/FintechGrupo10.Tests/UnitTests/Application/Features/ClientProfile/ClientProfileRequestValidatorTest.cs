using FintechGrupo10.Application.Features.ClientProfile;
using FluentAssertions;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.ClientProfile
{
    public class ClientProfileRequestValidatorTest
    {
        [Fact]
        public void Should_Have_Error_When_ClientId_Is_Default()
        {
            // Arrange
            var validator = CreateValidator();
            var request = new ClientProfileRequest
            {
                ClientId = Guid.Empty,
                Questions = new List<Question> { new Question { QuestionId = Guid.NewGuid(), QuestionValue = 1 } }
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().Contain(error => error.PropertyName == nameof(ClientProfileRequest.ClientId));
        }

        [Fact]
        public void Should_Have_Error_When_Questions_Is_Null()
        {
            // Arrange
            var validator = CreateValidator();
            var request = new ClientProfileRequest
            {
                ClientId = Guid.NewGuid(),
                Questions = null!
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().Contain(error => error.PropertyName == nameof(ClientProfileRequest.Questions));
        }

        [Fact]
        public void Should_Have_Error_When_Questions_Is_Empty()
        {
            // Arrange
            var validator = CreateValidator();
            var request = new ClientProfileRequest
            {
                ClientId = Guid.NewGuid(),
                Questions = []
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().Contain(error => error.PropertyName == nameof(ClientProfileRequest.Questions));
        }

        [Fact]
        public void Should_Not_Have_Error_When_ClientId_And_Questions_Are_Valid()
        {
            // Arrange
            var validator = CreateValidator();
            var request = new ClientProfileRequest
            {
                ClientId = Guid.NewGuid(),
                Questions = [new Question { QuestionId = Guid.NewGuid(), QuestionValue = 1 }]
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        private ClientProfileRequestValidator CreateValidator()
        {
            var mocker = new AutoMocker();
            return mocker.CreateInstance<ClientProfileRequestValidator>();
        }
    }
}
