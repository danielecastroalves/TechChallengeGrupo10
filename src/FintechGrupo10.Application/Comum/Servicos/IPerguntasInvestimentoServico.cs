using FintechGrupo10.Domain.Entidades;

namespace FintechGrupo10.Application.Comum.Servicos
{
    public interface IPerguntasInvestimentoServico
    {
        Task<List<Pergunta>> BuscaPerguntasInvestimento();
        Task<bool> RespondePerguntasInvestimento(string documento, List<Pergunta> perguntasRespondidas);
    }
}
