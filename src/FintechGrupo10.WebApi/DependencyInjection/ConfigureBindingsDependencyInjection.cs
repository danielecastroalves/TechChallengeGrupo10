using System.Diagnostics.CodeAnalysis;
using FintechGrupo10.Application;
using FintechGrupo10.Application.Common.Auth.Token;
using FintechGrupo10.Application.Common.Behavior;
using FintechGrupo10.Application.Common.Configurations;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Common.Services;
using FintechGrupo10.Domain.Entities;
using FintechGrupo10.Infrastructure.Autenticacao.Token;
using FintechGrupo10.Infrastructure.Mongo.Contexts;
using FintechGrupo10.Infrastructure.Mongo.Contexts.Interfaces;
using FintechGrupo10.Infrastructure.Mongo.Repositories;
using FintechGrupo10.Infrastructure.Mongo.Utils;
using FintechGrupo10.Infrastructure.Mongo.Utils.Interfaces;
using FintechGrupo10.Infrastructure.RabbitMQ;
using FintechGrupo10.WebApi.Consumers;
using FluentValidation;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using RabbitMQ.Client;
using Serilog;
using Serilog.Events;
using ILogger = Serilog.ILogger;

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
            ConfigureBindingsRabbitMQ(services, configuration);
            ConfigureBindingsSerilog(services);
            ConfigureBindingsValidators(services);

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
            services.AddScoped<IRepository<Question>, GenericRepository<Question>>();
            services.AddScoped<IUserRepository, UserRepository>();

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

        private static void ConfigureBindingsRabbitMQ(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfig>(configuration.GetSection("RabbitMq"));

            services.AddSingleton(_ =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = configuration.GetValue<string>("RabbitMq:Host"),
                    UserName = configuration.GetValue<string>("RabbitMq:Username"),
                    Password = configuration.GetValue<string>("RabbitMq:Password")
                };

                return factory.CreateConnection();
            });

            // RabbitMQ Services
            services.AddSingleton<IMessagePublisherService, MessagePublisherService>();

            // Consumers
            services.AddHostedService<ClientProfileConsumer>();
        }

        private static void ConfigureBindingsSerilog(IServiceCollection services)
        {
            const string path = "logs";
            var shortDate = DateTime.Now.ToString("yyyy-MM-dd_HH");
            var filename = $@"{path}\{shortDate}.log";

            var logConfig = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Host.Startup", LogEventLevel.Warning)
                .MinimumLevel.Override("Host.Aggregator", LogEventLevel.Warning)
                .MinimumLevel.Override("Host.Executor", LogEventLevel.Fatal)
                .MinimumLevel.Override("Host.Results", LogEventLevel.Fatal)
                .WriteTo.Console()
                .WriteTo.File(filename, rollingInterval: RollingInterval.Day);

            ILogger logger = logConfig.CreateLogger();

            services.AddLogging(configure: x => x.AddSerilog(logger));
        }

        private static void ConfigureBindingsValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(new AssemblyReference().GetAssembly());
        }
    }
}
