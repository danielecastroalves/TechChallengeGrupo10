using FintechGrupo10.Application.Recursos.Login;
using FintechGrupo10.Infrastructure.Mongo.Contextos;
using FintechGrupo10.Infrastructure.Mongo.Contextos.Interfaces;
using FintechGrupo10.Infrastructure.Mongo.Utils;
using FintechGrupo10.Infrastructure.Mongo.Utils.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace FintechGrupo10.WebApi.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ConfigureBindingsDependencyInjection
    {
        public static void RegisterBindings
        (
            IServiceCollection services,
            IConfiguration configuration
        )
        {
            // Mongo
            ConfigureBindingsMongo(services, configuration);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        public static void ConfigureBindingsMongo
        (
            IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.Configure<MongoConnectionOptions>(c =>
            {
                int defaultTtlDays = configuration.GetValue<int>("Mongo:DefaultTtlDays");
                c.DefaultTtlDays = defaultTtlDays == default ? 30 : defaultTtlDays;

                c.ConnectionString = configuration.GetValue<string>("Mongo:ConnectionString");

                c.Schema = configuration.GetValue<string>("Mongo:Schema");
            });

            services.AddSingleton<IMongoConnection, MongoConnection>();
            services.AddSingleton<IMongoContext, MongoContext>();

            //Configure Mongo Repositories

            //Configure Mongo Serializer
        }
    }
}