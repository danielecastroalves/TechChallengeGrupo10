using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.InvestmentQuestion.DeleteInvestmentQuestion;
using FintechGrupo10.Domain.Entities;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Features.InvestmentQuestion
{
    public class DeleteInvestmentQuestionRequestHandlerTest
    {
        private readonly DeleteInvestmentQuestionRequestHandler _sut;
        private readonly Mock<IRepository<QuestionEntity>> _repository;

        public DeleteInvestmentQuestionRequestHandlerTest()
        {
            var autoMock = new AutoMocker();
            _sut = autoMock.CreateInstance<DeleteInvestmentQuestionRequestHandler>();
            _repository = autoMock.GetMock<IRepository<QuestionEntity>>();
        }

        [Fact]
        public async Task DeletaPerguntasDeInvestmento()
        {
            // Arrange
            var request = new DeleteInvestmentQuestionRequest(Guid.NewGuid());

            _repository
                .Setup(x => x.DeleteByIdAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync(true);

            // Act
            await _sut.Handle(request, CancellationToken.None);

            // Assert
            _repository
                .Verify(x => x.DeleteByIdAsync(It.IsAny<Guid>(), CancellationToken.None),
                        Times.Once());
        }
    }
}
