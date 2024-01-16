using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Application.Comum.Servicos;
using FintechGrupo10.Domain.Entidades;

namespace FintechGrupo10.Infrastructure.Servicos
{
    public class PerguntasInvestimentoServico : IPerguntasInvestimentoServico
    {
        private readonly IPerguntasInvestimentoRepositorio _perguntasInvestimentoRepositorio;

        public PerguntasInvestimentoServico(IPerguntasInvestimentoRepositorio perguntasInvestimentoRepositorio)
        {
            _perguntasInvestimentoRepositorio = perguntasInvestimentoRepositorio;
        }

        public async Task<List<Pergunta>> BuscaPerguntasInvestimento()
        {
            return await _perguntasInvestimentoRepositorio.BuscaPerguntasInvestimento();
        }

        public Task<bool> RespondePerguntasInvestimento(string documento, List<Pergunta> perguntasRespondidas)
        {
            throw new NotImplementedException();
        }
    }
}
