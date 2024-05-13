using Bogus;
using FintechGrupo10.Domain.Entities;
using FintechGrupo10.Infrastructure.Mongo.Contexts.Interfaces;
using FintechGrupo10.Infrastructure.Mongo.Repositories;
using MongoDB.Driver;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Infrastructure.Mongo.Repositories
{
    public class UserRepositoryTest
    {
        private readonly AutoMocker _mocker;
        private readonly TestDataRepository _sut;
        public readonly Faker _faker;

        public UserRepositoryTest()
        {
            _faker = new Faker();
            _mocker = new AutoMocker();
            _sut = _mocker.CreateInstance<TestDataRepository>();
        }

        [Fact]
        public async Task GetListByFilterAsync_ShouldExecuteWithSuccess_WhenFilterIsValid()
        {
            // Arrange          
            _mocker
                .GetMock<IMongoCollection<ClienteEntity>>()
                .Setup(x => x.FindAsync(It.IsAny<FilterDefinition<ClienteEntity>>(),
                                        It.IsAny<FindOptions<ClienteEntity>>(),
                                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(_mocker.GetMock<IAsyncCursor<ClienteEntity>>().Object);

            _mocker.GetMock<IMongoContext>()
                .Setup(x => x.GetCollection<ClienteEntity>(null))
                .Returns(_mocker.GetMock<IMongoCollection<ClienteEntity>>().Object);

            // Act
            _ = await _sut.GetAuthByLoginAndPassword("login", "senha", CancellationToken.None);

            // Assert
            _mocker
                .GetMock<IMongoCollection<ClienteEntity>>()
                .Verify(x =>
                    x.FindAsync(It.IsAny<FilterDefinition<ClienteEntity>>(),
                                It.IsAny<FindOptions<ClienteEntity>>(),
                                It.IsAny<CancellationToken>()),
                    Times.Once);

            _mocker.GetMock<IMongoContext>()
                .Verify(x =>
                    x.GetCollection<ClienteEntity>(null),
                    Times.Once);
        }

        public class TestDataRepository : UserRepository
        {
            public TestDataRepository(IMongoContext context) : base(context) { }
        }
    }
}
