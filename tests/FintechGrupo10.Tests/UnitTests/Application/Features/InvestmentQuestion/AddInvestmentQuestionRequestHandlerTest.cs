using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.InvestmentQuestion.AddInvestmentQuestion;
using FintechGrupo10.Domain.Entities;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Recursos.PerguntasInvestimento
{
    public class AddInvestmentQuestionRequestHandlerTest
    {
        private readonly AddInvestmentQuestionRequestHandler _sut;
        private readonly Mock<IRepository<QuestionEntity>> _repository;
        private readonly AddInvestmentQuestionRequest _request;

        public AddInvestmentQuestionRequestHandlerTest()
        {
            var autoMock = new AutoMocker();

            _repository = autoMock.GetMock<IRepository<QuestionEntity>>();
            _request = new AddInvestmentQuestionRequest();

            _sut = autoMock.CreateInstance<AddInvestmentQuestionRequestHandler>();
        }

        [Fact]
        public async Task Handle_ShouldExecuteWithSuccess_WhenRequestIsValid()
        {
            // Arrange         
            _repository.Setup(x => x.AddAsync(It.IsAny<QuestionEntity>(), CancellationToken.None)).ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _sut.Handle(_request, CancellationToken.None);

            // Assert
            Assert.IsType<Guid>(result);
        }
    }
}
