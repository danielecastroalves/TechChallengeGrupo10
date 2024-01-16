using FintechGrupo10.Domain.DTOS;

namespace FintechGrupo10.Application.Comum.Servicos
{
    public interface IPerfilInvestimentoServico
    {
        Task<bool> DefinicaoPerfilInvestimento(PerguntasResponditasDTO resultado);
    }
}
