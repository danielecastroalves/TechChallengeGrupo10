using Bogus;
using FintechGrupo10.Application.Features.Client;
using FintechGrupo10.Application.Features.Client.AddClient;
using FintechGrupo10.Application.Features.Client.UpdateClient;
using FintechGrupo10.Domain.Enums;

namespace FintechGrupo10.Tests.MotherObjects.Requests
{
    public static class ClientRequestBaseMotherObject
    {
        public static Faker<T> ValidRequestBase<T>(this Faker<T> faker) where T : ClientRequestBase => faker
            .RuleFor(x => x.NomeCliente, f => f.Random.String(10))
            .RuleFor(x => x.Documento, f => f.Random.Long(00000000001, 99999999999).ToString())
            .RuleFor(x => x.Telefone, f => f.Random.Long(000000001, 999999999).ToString())
            .RuleFor(x => x.Email, f => f.Random.String(10))
            .RuleFor(x => x.DataNascimento, DateTime.UtcNow)
            .RuleFor(x => x.Login, f => f.Random.String(10))
            .RuleFor(x => x.Senha, f => f.Random.String(10))
            .RuleFor(x => x.Permissao, f => f.PickRandom<Roles>());

        public static AddClientRequest ValidAddClientRequest()
            => new Faker<AddClientRequest>()
            .ValidRequestBase()
            .Generate();

        public static UpdateClientRequest ValidUpdateClientRequest()
            => new Faker<UpdateClientRequest>()
            .ValidRequestBase()
            .Generate();
    }
}
