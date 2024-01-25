using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.PerguntasInvestimento.CriarPergunta;
using FintechGrupo10.Domain.Entities;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Recursos.PerguntasInvestimento
{
    public class CriaPerguntaTeste
    {
        private readonly CriarPerguntasInvestimentoHandler _handler;
        private readonly Mock<IRepository<Pergunta>> _repositorio;
        private readonly CriarPerguntasInvestimentoRequest _request;

        public CriaPerguntaTeste()
        {
            var autoMock = new AutoMocker();
            _handler = autoMock.CreateInstance<CriarPerguntasInvestimentoHandler>();
            _repositorio = autoMock.GetMock<IRepository<Pergunta>>();
            _request = new CriarPerguntasInvestimentoRequest();
        }

        [Fact]
        public async Task CriaPerguntasDeInvestimento()
        {
            // Arrange
            var perguntas = Pergunta();
            _repositorio.Setup(x => x.AddAsync(perguntas, CancellationToken.None)).ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.IsType<Guid>(result);
        }

        private static Pergunta Pergunta()
        {
            return new Pergunta
            {
                Titulo = "Pergunta 1",
                Resposta = new List<Resposta>
                    {
                        new()
                        {
                            Descricao = "Resposta 1",
                            Pontuacao = 10
                        }
                    }
            };
        }
    }
}
