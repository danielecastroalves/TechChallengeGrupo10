using FluentValidation;
using MediatR;

namespace FintechGrupo10.Application.Recursos.ClientProfile
{
    public class ClientProfileRequest : IRequest
    {
        public Guid ClientId { get; set; }

        public List<Question> Questions { get; set; } = null!;
    }

    public class Question
    {
        public Guid QuestionId { get; set; }
        public int QuestionPoint { get; set; }
    }

    public class ClientProfileRequestValidator : AbstractValidator<ClientProfileRequest>
    {
        public ClientProfileRequestValidator()
        {
            RuleFor(x => x.ClientId).NotEmpty().NotNull();
            RuleFor(x => x.Questions).NotEmpty().NotNull();
        }
    }
}
