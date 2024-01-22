using FintechGrupo10.Infrastructure.Mongo.Utils;

namespace FintechGrupo10.Tests.MockAssistant.Infrastructure.Mongo
{
    public static class MongoConnectionOptionsMock
    {
        public static MongoConnectionOptions Get
        (
            int defaultTtlDays = 1,
            string schema = "schema",
            string connectionString = "connectionString"
        ) => new()
        {
            DefaultTtlDays = defaultTtlDays,
            Schema = schema,
            ConnectionString = connectionString
        };
    }
}
