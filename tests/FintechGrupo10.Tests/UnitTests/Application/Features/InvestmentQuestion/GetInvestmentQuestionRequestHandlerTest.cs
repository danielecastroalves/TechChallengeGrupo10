using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.InvestmentQuestion.GetInvestmentQuestion;
using FintechGrupo10.Domain.Entities;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Recursos.PerguntasInvestmento
{
    public class GetInvestmentQuestionRequestHandlerTest
    {
        private readonly GetInvestmentQuestionRequestHandler _handler;
        private readonly Mock<IRepository<QuestionEntity>> _repository;
        private readonly GetInvestmentQuestionRequest _request;

        public GetInvestmentQuestionRequestHandlerTest()
        {
            var autoMock = new AutoMocker();
            _handler = autoMock.CreateInstance<GetInvestmentQuestionRequestHandler>();
            _repository = autoMock.GetMock<IRepository<QuestionEntity>>();
            _request = new GetInvestmentQuestionRequest();
        }

        [Fact]
        public async Task Handle_ShouldExecuteWithSuccess_WhenRequestIsValid()
        {
            // Arrange
            var response = QuestionList();

            _repository
                .Setup(x => x.GetListByFilterAsync(x => x.Ativo, CancellationToken.None))
                .ReturnsAsync(response);

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        private static List<QuestionEntity> QuestionList()
        {
            return new List<QuestionEntity>
            {
                new()
                {
                    Titulo = "Pergunta 1",
                    Resposta = new List<Answer>
                    {
                        new()
                        {
                            Descricao = "Resposta 1",
                            Pontuacao = 10
                        }
                    }
                }
            };
        }
    }
}
