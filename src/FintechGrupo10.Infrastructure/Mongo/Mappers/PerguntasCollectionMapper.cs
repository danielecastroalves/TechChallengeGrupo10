using FintechGrupo10.Domain.Entidades;
using MongoDB.Bson.Serialization;

namespace FintechGrupo10.Infrastructure.Mongo.Mappers
{
    public static class PerguntasCollectionMapper
    {
        public static void MapeamentoMongoCollection()
        {
            BsonClassMap.RegisterClassMap<Pergunta>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
