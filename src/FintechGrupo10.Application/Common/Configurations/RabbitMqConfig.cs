using System.Diagnostics.CodeAnalysis;

namespace FintechGrupo10.Application.Common.Configurations
{
    [ExcludeFromCodeCoverage]
    public class RabbitMqConfig
    {
        public string ConnectionString { get; set; } = null!;
        public string ClientProfileQueue { get; set; } = null!;
        public string BuyProductQueue { get; set; } = null!;
        public string SellProductQueue { get; set; } = null!;
    }
}
