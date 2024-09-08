using System.ComponentModel;

namespace FintechGrupo10.Domain.Enums
{
    public enum TransactionType
    {
        [Description("Compra")]
        Compra,

        [Description("Venda")]
        Venda
    }
}
