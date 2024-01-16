using FintechGrupo10.Domain.Entidades;
using MongoDB.Driver;

namespace FintechGrupo10.Infrastructure.Mongo.Contextos.Interfaces
{
    public interface IMongoContext
    {
        int DefaultTtlDays { get; }

        IMongoDatabase GetDatabase();

        IMongoCollection<T> GetCollection<T>(string? name = null);

        IMongoCollection<ClienteEntity> Cliente { get; }
        IMongoCollection<Pergunta> Pergunta { get; }
    }
}
