using FintechGrupo10.Domain.Enums;

namespace FintechGrupo10.Domain.Entidades
{
    public class ClienteEntity : Usuario
    {
        public string NomeCliente { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
        public PerfilInvestimento PerfilInvestimento { get; set; }
    }
}