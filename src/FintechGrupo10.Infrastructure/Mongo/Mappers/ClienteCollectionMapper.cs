using FintechGrupo10.Domain.Entidades;
using MongoDB.Bson.Serialization;

namespace FintechGrupo10.Infrastructure.Mongo.Mappers
{
    public static class ClienteCollectionMapper
    {
        public static void MapeamentoMongoCollection()
        {
            BsonClassMap.RegisterClassMap<Cliente>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
