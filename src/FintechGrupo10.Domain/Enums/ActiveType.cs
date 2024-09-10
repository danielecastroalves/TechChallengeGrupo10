using System.ComponentModel;

namespace FintechGrupo10.Domain.Enums
{
    public enum ActiveType
    {
        [Description("Ações")]
        Acoes,

        [Description("CDB")]
        CDB,

        [Description("Commodities")]
        Commodities,

        [Description("Criptomoedas")]
        Criptomoedas,

        [Description("Fundos de Investimento")]
        FundosDeInvestimento,

        [Description("Fundos Imobiliários")]
        FundosImobiliarios,

        [Description("LCA")]
        LCA,

        [Description("LCI")]
        LCI,

        [Description("Títulos Privados")]
        TitulosPrivados,

        [Description("Títulos Públicos")]
        TitulosPublicos
    }
}
