using MongoDB.Driver;

namespace FintechGrupo10.Infrastructure.Mongo.Contextos.Interfaces
{
    public interface IMongoContext
    {
        int DefaultTtlDays { get; }

        IMongoDatabase GetDatabase();

        IMongoCollection<T> GetCollection<T>(string? name = null);
    }
}
