using FintechGrupo10.Application.Common.Behavior;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Comum.Behavior
{
    public class ValidationBehaviorQuery : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class ValidationBehaviorQueryValidator : AbstractValidator<ValidationBehaviorQuery>
    {
        public ValidationBehaviorQueryValidator()
        {
            RuleFor(r => r.Id)
                .NotEqual(0)
                .WithMessage("Id não pode ser igual a zero.");
        }
    }

    public class ValidationBehaviorTest
    {
        [Fact]
        public async Task Handle_ShouldRunCommand_WhenGivenCommandQueryWithResponseAndValidationDoesNotExists()
        {
            // Arrange && Act
            var result = await Handle();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldCheckCommandWithoutErrors_WhenGivenCommandQueryWithResponseAndValidationExists()
        {
            // Arrange && Act
            var result = await Handle(true, false);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldCheckCommandWithErrors_WhenGivenCommandQueryWithResponseAndValidationExists()
        {
            // Arrange, Act && Assert
            await Assert.ThrowsAsync<ValidationException>(() => Handle(true, true));
        }

        private static Task<bool> Handle(bool withValidator = false, bool withError = false)
        {
            var validators = new List<IValidator<ValidationBehaviorQuery>>();

            var query = new ValidationBehaviorQuery { Id = 1 };

            if (withValidator)
            {
                if (withError)
                {
                    query.Id = 0;
                }

                validators.Add(new ValidationBehaviorQueryValidator());
            }

            var handle = new ValidationBehavior<ValidationBehaviorQuery, bool>
            (
                validators
            );

            return handle.Handle
            (
                query,
                async () => await Task.FromResult(true),
                CancellationToken.None
            );
        }
    }
}
