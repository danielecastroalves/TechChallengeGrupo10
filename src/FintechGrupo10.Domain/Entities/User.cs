using FintechGrupo10.Domain.Enums;

namespace FintechGrupo10.Domain.Entities;

public class User : Entity
{
    public string Login { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public Permission Permissao { get; set; }
}
