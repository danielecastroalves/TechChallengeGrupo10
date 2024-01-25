﻿using FintechGrupo10.Domain.Enums;

namespace FintechGrupo10.Domain.Entities
{
    public class ClienteEntity : User
    {
        public string NomeCliente { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
        public InvestorProfile PerfilInvestimento { get; set; }
    }
}