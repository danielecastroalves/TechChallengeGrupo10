using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.Client
{
    using System;
    using FintechGrupo10.Application.Features.Client;
    using FluentAssertions;
    using FluentValidation.TestHelper;
    using Moq;
    using Moq.AutoMock;
    using Xunit;

    namespace YourNamespace.Tests
    {
        public class ClientRequestBaseValidatorTests
        {
            [Fact]
            public void Should_Have_Error_When_NomeCliente_Is_Null()
            {
                // Arrange
                var validator = CreateValidator();
                var request = new ClientRequestBase { NomeCliente = null };

                // Act
                var result = validator.Validate(request);

                // Assert
                result.Errors.Should().Contain(error => error.PropertyName == nameof(ClientRequestBase.NomeCliente));
            }

            [Fact]
            public void Should_Have_Error_When_Documento_Is_Null()
            {
                // Arrange
                var validator = CreateValidator();
                var request = new ClientRequestBase { Documento = null };

                // Act
                var result = validator.Validate(request);

                // Assert
                result.Errors.Should().Contain(error => error.PropertyName == nameof(ClientRequestBase.Documento));
            }

            // ... (repeat similar tests for other properties)

            [Fact]
            public void Should_Not_Have_Error_When_All_Properties_Are_Valid()
            {
                // Arrange
                var validator = CreateValidator();
                var request = new ClientRequestBase
                {
                    NomeCliente = "John Doe",
                    Documento = "123456789",
                    Telefone = "123-456-7890",
                    Email = "john.doe@example.com",
                    Login = "johndoe",
                    Senha = "Password123",
                    DataNascimento = DateTime.Now.AddYears(-25)
                };

                // Act
                var result = validator.Validate(request);

                // Assert
                result.Errors.Should().BeEmpty();
            }

            private ClientRequestBaseValidator<ClientRequestBase> CreateValidator()
            {
                var mocker = new AutoMocker();
                var validator = mocker.CreateInstance<ClientRequestBaseValidator<ClientRequestBase>>();
                return validator;
            }
        }
    }
}
