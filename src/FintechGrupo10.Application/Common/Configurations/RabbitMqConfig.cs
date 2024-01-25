using System.Diagnostics.CodeAnalysis;

namespace FintechGrupo10.Application.Common.Configurations
{
    [ExcludeFromCodeCoverage]
    public class RabbitMqConfig
    {
        public string Host { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ClientProfileQueue { get; set; } = null!;
    }
}
