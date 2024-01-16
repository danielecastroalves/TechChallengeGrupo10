using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.Entidades;
using FintechGrupo10.Infrastructure.Mongo.Contextos.Interfaces;
using MongoDB.Driver;

namespace FintechGrupo10.Infrastructure.Mongo.Repositorios
{
    public class PerguntasInvestimentoRepositorio : IPerguntasInvestimentoRepositorio
    {
        private readonly IMongoContext _mongoContext;

        public PerguntasInvestimentoRepositorio(IMongoContext mongoContext) => _mongoContext = mongoContext;

        public async Task<List<Pergunta>> BuscaPerguntasInvestimento()
        {
            return await _mongoContext.Pergunta.Find(Builders<Pergunta>.Filter.Empty).ToListAsync();
        }
    }
}
