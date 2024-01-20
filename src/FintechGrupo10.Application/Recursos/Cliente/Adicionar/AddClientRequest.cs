using MediatR;

namespace FintechGrupo10.Application.Recursos.Cliente.Adicionar
{
    public class AddClientRequest : IRequest
    {
        public string NomeCliente { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
    }
}
