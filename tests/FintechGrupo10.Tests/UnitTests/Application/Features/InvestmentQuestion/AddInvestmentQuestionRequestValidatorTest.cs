using FintechGrupo10.Application.Features.InvestmentQuestion.AddInvestmentQuestion;
using FintechGrupo10.Domain.Entities;
using FluentValidation.TestHelper;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.InvestmentQuestion
{
    public class AddInvestmentQuestionRequestValidatorTest
    {
        [Fact]
        public void Should_Have_Error_When_Titulo_Is_Null()
        {
            // Arrange
            var validator = new AddInvestmentQuestionRequestValidator();
            var request = new AddInvestmentQuestionRequest { Titulo = null, Resposta = new List<Answer>() };

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Titulo)
                  .WithErrorMessage("'Titulo' must not be empty.");
        }

        [Fact]
        public void Should_Have_Error_When_Titulo_Is_Empty()
        {
            // Arrange
            var validator = new AddInvestmentQuestionRequestValidator();
            var request = new AddInvestmentQuestionRequest { Titulo = string.Empty, Resposta = new List<Answer>() };

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Titulo)
                  .WithErrorMessage("'Titulo' must not be empty.");
        }

        [Fact]
        public void Should_Have_Error_When_Resposta_Is_Null()
        {
            // Arrange
            var validator = new AddInvestmentQuestionRequestValidator();
            var request = new AddInvestmentQuestionRequest { Titulo = "Pergunta", Resposta = null };

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Resposta)
                  .WithErrorMessage("'Resposta' must not be empty.");
        }

        [Fact]
        public void Should_Have_Error_When_Resposta_Is_Empty()
        {
            // Arrange
            var validator = new AddInvestmentQuestionRequestValidator();
            var request = new AddInvestmentQuestionRequest { Titulo = "Pergunta", Resposta = new List<Answer>() };

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Resposta)
                  .WithErrorMessage("'Resposta' must not be empty.");
        }
    }
}
