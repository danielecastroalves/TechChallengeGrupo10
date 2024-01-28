using FintechGrupo10.Domain.Enums;
using FluentValidation;

namespace FintechGrupo10.Application.Features.Client
{
    public class ClientRequestBase
    {
        public string NomeCliente { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DataNascimento { get; set; }

        public string Login { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public Roles Permissao { get; set; }
    }

    public class ClientRequestBaseValidator<T> : AbstractValidator<T> where T : ClientRequestBase
    {
        public ClientRequestBaseValidator()
        {
            RuleFor(x => x.NomeCliente).NotEmpty().NotNull();
            RuleFor(x => x.Documento).NotEmpty().NotNull();
            RuleFor(x => x.Telefone).NotEmpty().NotNull();
            RuleFor(x => x.Email).NotEmpty().NotNull();
            RuleFor(x => x.Login).NotEmpty().NotNull();
            RuleFor(x => x.Senha).NotEmpty().NotNull();
            RuleFor(x => x.DataNascimento).Must(BeValidDateTime);
        }

        private bool BeValidDateTime(DateTime date) => !date.Equals(default);
    }
}
