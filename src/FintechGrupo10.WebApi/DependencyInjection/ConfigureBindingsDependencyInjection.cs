﻿using FintechGrupo10.Application;
using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.Entidades;
using FintechGrupo10.Infrastructure.Autenticacao.Token;
using FintechGrupo10.Infrastructure.Autenticacao.Token.Interface;
using FintechGrupo10.Infrastructure.Mongo.Contextos;
using FintechGrupo10.Infrastructure.Mongo.Contextos.Interfaces;
using FintechGrupo10.Infrastructure.Mongo.Repositorios;
using FintechGrupo10.Infrastructure.Mongo.Utils;
using FintechGrupo10.Infrastructure.Mongo.Utils.Interfaces;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Diagnostics.CodeAnalysis;

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

            // MediatR
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMediatR(new AssemblyReference().GetAssembly());

            // Services
            services.AddScoped<ITokenService, TokenService>();
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
            services.AddScoped<IRepositorio<ClienteEntity>, RepositorioBase<ClienteEntity>>();
            services.AddScoped<IRepositorio<Pergunta>, RepositorioBase<Pergunta>>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

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
    }
}