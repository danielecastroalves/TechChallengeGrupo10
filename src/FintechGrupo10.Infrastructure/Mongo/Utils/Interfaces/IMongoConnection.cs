using MongoDB.Driver;

namespace FintechGrupo10.Infrastructure.Mongo.Utils.Interfaces
{
    public interface IMongoConnection
    {
        IMongoDatabase GetDatabase();
    }
}
