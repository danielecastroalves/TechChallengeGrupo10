using Bogus;
using FintechGrupo10.Domain.Entities;

namespace FintechGrupo10.Tests.MotherObjects.Entities
{
    public class MockedEntity : Entity
    {
        public MockedEntity() { }
    }

    public static class EntityMotherObject
    {
        public static MockedEntity ValidObject()
        {
            var faker = new Faker<MockedEntity>()
                .CustomInstantiator(f => Activator.CreateInstance<MockedEntity>())
                .RuleFor(x => x.Id, f => f.Lorem.Random.Guid())
                .RuleFor(x => x.DataInsercao, f => f.Date.Recent())
                .RuleFor(x => x.DataAtualizacao, f => f.Date.Recent())
                .Generate();

            return faker;
        }
    }
}
