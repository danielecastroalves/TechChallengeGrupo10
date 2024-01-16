using FintechGrupo10.Domain.Entidades;

namespace FintechGrupo10.Application.Comum.Repositorios
{
    public interface IPerguntasInvestimentoRepositorio
    {
        Task<List<Pergunta>> BuscaPerguntasInvestimento();
    }
}
