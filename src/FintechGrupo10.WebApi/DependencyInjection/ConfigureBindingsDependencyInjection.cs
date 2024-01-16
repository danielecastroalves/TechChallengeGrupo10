using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Application.Comum.Servicos;
using FintechGrupo10.Infrastructure.Mongo.Contextos;
using FintechGrupo10.Infrastructure.Mongo.Contextos.Interfaces;
using FintechGrupo10.Infrastructure.Mongo.Repositorios;
using FintechGrupo10.Infrastructure.Mongo.Utils;
using FintechGrupo10.Infrastructure.Mongo.Utils.Interfaces;
using FintechGrupo10.Infrastructure.Services;
using FintechGrupo10.Infrastructure.Servicos;
using System.Diagnostics.CodeAnalysis;

namespace FintechGrupo10.WebApi.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public class ConfigureBindingsDependencyInjection
    {
        public static void RegisterBindings
        (
            IServiceCollection services,
            IConfiguration configuration
        )
        {
            // Mongo
            ConfigureBindingsMongo(services, configuration);
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
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IPerguntasInvestimentoRepositorio, PerguntasInvestimentoRepositorio>();

            //Configure Mongo Serializer

            //Configure Services
            services.AddScoped<IPerfilInvestimentoServico, PerfilInvestimentoServico>();
            services.AddScoped<IPerguntasInvestimentoServico, PerguntasInvestimentoServico>();
        }
    }
}
