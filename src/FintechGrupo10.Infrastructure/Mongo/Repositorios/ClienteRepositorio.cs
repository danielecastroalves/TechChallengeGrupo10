using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.Entidades;
using FintechGrupo10.Domain.Enums;
using FintechGrupo10.Infrastructure.Mongo.Contextos.Interfaces;
using MongoDB.Driver;

namespace FintechGrupo10.Infrastructure.Mongo.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IMongoContext _mongoContext;

        public ClienteRepositorio(IMongoContext mongoContext) => _mongoContext = mongoContext;

        public async Task<bool> AtualizaPerfilInvestimento(string documento, PerfilInvestimento perfil)
        {
            var atualizaPerfil = Builders<Cliente>.Update.Set(x => x.PerfilInvestimento, perfil);

            var filtro = Builders<Cliente>.Filter.Eq(z => z.Documento, documento);

            var resultado = await _mongoContext.Cliente.UpdateOneAsync(filtro, atualizaPerfil);

            return resultado.IsAcknowledged;
        }


    }
}
