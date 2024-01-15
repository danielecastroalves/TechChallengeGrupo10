using FintechGrupo10.Domain.Enums;

namespace FintechGrupo10.Domain.Entidades;

public class Usuario : EntidadeBase
{
    public string Login { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public Permissao Permissao { get; set; }
}