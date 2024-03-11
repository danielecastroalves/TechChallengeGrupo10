using FintechGrupo10.Application.Features.InvestmentQuestion.DeleteInvestmentQuestion;
using FluentValidation.TestHelper;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.InvestmentQuestion
{
    public class DeleteInvestmentQuestionRequestValidatorTest
    {
        [Fact]
        public void Should_Have_Error_When_QuestionId_Is_Default()
        {
            // Arrange
            var validator = new DeleteInvestmentQuestionRequestValidator();
            var request = new DeleteInvestmentQuestionRequest(Guid.Empty);

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.QuestionId)
                  .WithErrorMessage("'Question Id' must not be empty.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_QuestionId_Is_Not_Default()
        {
            // Arrange
            var validator = new DeleteInvestmentQuestionRequestValidator();
            var request = new DeleteInvestmentQuestionRequest(Guid.NewGuid());

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.QuestionId);
        }
    }
}
