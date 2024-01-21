using System.Diagnostics.CodeAnalysis;
using FintechGrupo10.Application;
using FintechGrupo10.Application.Comum.Behavior;
using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Application.Comum.Services;
using FintechGrupo10.Domain.Entities;
using FintechGrupo10.Infrastructure.Autenticacao.Token;
using FintechGrupo10.Infrastructure.Autenticacao.Token.Interface;
using FintechGrupo10.Infrastructure.Mongo.Contexts;
using FintechGrupo10.Infrastructure.Mongo.Contexts.Interfaces;
using FintechGrupo10.Infrastructure.Mongo.Repositories;
using FintechGrupo10.Infrastructure.Mongo.Utils;
using FintechGrupo10.Infrastructure.Mongo.Utils.Interfaces;
using FintechGrupo10.Infrastructure.RabbitMQ;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using RabbitMQ.Client;

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
            ConfigureBindingsMediatR(services);
            ConfigureBindingsMongo(services, configuration);
            ConfigureBindingsRabbitMQ(services);

            // Services
            services.AddScoped<ITokenService, TokenService>();       
        }
       

        private static void ConfigureBindingsMediatR(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddMediatR(new AssemblyReference().GetAssembly());
        }

        private static void ConfigureBindingsMongo(IServiceCollection services, IConfiguration configuration)
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
            services.AddScoped<IRepository<ClienteEntity>, GenericRepository<ClienteEntity>>();
            services.AddScoped<IRepository<Pergunta>, GenericRepository<Pergunta>>();
            services.AddScoped<IUsuarioRepository, UsuarioRepositorio>();

            //Configure Mongo Serializer
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            #pragma warning disable 618
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
            BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
            #pragma warning restore

            #pragma warning disable CS8602
            var objectSerializer = new ObjectSerializer
            (
               type =>
                       ObjectSerializer.DefaultAllowedTypes(type) ||
                       type.FullName.StartsWith("FintechGrupo10.Domain")
            );
            #pragma warning restore CS8602

            BsonSerializer.RegisterSerializer(objectSerializer);
        }

        private static void ConfigureBindingsRabbitMQ(IServiceCollection services)
        {
            services.AddSingleton(x =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "root",
                    Password = "root"
                };

                return factory.CreateConnection();
            });

            // RabbitMQ Services
            services.AddSingleton<IMessagePublisherService, MessagePublisherService>();
            services.AddSingleton<IMessageConsumerService, MessageConsumerService>();
        }
    }
}
