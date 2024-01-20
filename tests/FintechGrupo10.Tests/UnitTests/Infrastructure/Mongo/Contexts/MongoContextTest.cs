using FintechGrupo10.Domain.Entities;
using FintechGrupo10.Infrastructure.Mongo.Contextos;
using FintechGrupo10.Infrastructure.Mongo.Utils;
using FintechGrupo10.Infrastructure.Mongo.Utils.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Infrastructure.Mongo.Contexts
{
    public class MongoContextTest
    {
        private readonly AutoMocker _mocker;
        private readonly MongoContext _mockedMongoContext;

        [Fact]
        public void GetDatabase_ShouldReturnMongoDatabase_WhenRequested()
        {
            // Arrange
            var mongoDataBase = _mocker.GetMock<IMongoDatabase>();

            _mocker
                .GetMock<IMongoConnection>()
                .Setup(x => x.GetDatabase())
                .Returns(mongoDataBase.Object);

            // Act
            var result = _mockedMongoContext.GetDatabase();

            // Assert
            Assert.Equal(mongoDataBase.Object, result);
        }

        [Fact]
        public void GetDatabase_ShouldReturnCollection_WhenAskedForCollectionByName()
        {
            // Arrange
            var collection = new Mock<IMongoCollection<Entity>>();

            var mongoDataBase = _mocker.GetMock<IMongoDatabase>();

            mongoDataBase
            .Setup(x => x.GetCollection<Entity>(typeof(Entity).Name, null))
            .Returns(collection.Object);
            _mocker
                .GetMock<IMongoConnection>()
                .Setup(x => x.GetDatabase())
                .Returns(mongoDataBase.Object);

            // Act
            _mockedMongoContext.GetCollection<Entity>(typeof(Entity).Name);

            // Assert
            mongoDataBase.Verify(x =>
                x.GetCollection<Entity>(typeof(Entity).Name, null),
                Times.Exactly(1));
        }

        [Fact]
        public void GetDatabase_ShouldReturnCollection_WhenAskedForCollectionByType()
        {
            // Arrange
            var collection = new Mock<IMongoCollection<Entity>>();

            var mongoDataBase = _mocker.GetMock<IMongoDatabase>();

            mongoDataBase
                .Setup(x => x.GetCollection<Entity>(typeof(Entity).Name, null))
                .Returns(collection.Object);

            _mocker
                .GetMock<IMongoConnection>()
                .Setup(x => x.GetDatabase())
                .Returns(mongoDataBase.Object);

            // Act
            MongoContext.GetCollection<Entity>(mongoDataBase.Object);

            // Assert
            mongoDataBase.Verify(x =>
                x.GetCollection<Entity>(typeof(Entity).Name, null),
                Times.Exactly(1));
        }

        public MongoContextTest()
        {
            _mocker = new AutoMocker();

            var mongoConnection = _mocker.GetMock<IMongoConnection>();

            var mongoDatabase = _mocker.GetMock<IMongoDatabase>();

            var options = _mocker.GetMock<IOptions<MongoConnectionOptions>>();

            mongoConnection
                .Setup(mock => mock.GetDatabase())
                .Returns(mongoDatabase.Object);

            options
                .Setup(mock => mock.Value)
                .Returns(MongoConnectionOptionsMock.Get());

            _mockedMongoContext = _mocker.CreateInstance<MongoContext>();
        }
    }
}
