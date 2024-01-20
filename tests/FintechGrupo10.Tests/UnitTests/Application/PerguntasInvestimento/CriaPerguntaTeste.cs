using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.Entidades;
using Moq.AutoMock;
using Moq;
using FintechGrupo10.Application.Recursos.PerguntasInvestimento.CriarPergunta;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.PerguntasInvestimento
{
    public class CriaPerguntaTeste
    {
        private readonly CriarPerguntasInvestimentoHandler _handler;
        private readonly Mock<IRepositorio<Pergunta>> _repositorio;
        private readonly CriarPerguntasInvestimentoRequest _request;

        public CriaPerguntaTeste()
        {
            var autoMock = new AutoMocker();
            _handler = autoMock.CreateInstance<CriarPerguntasInvestimentoHandler>();
            _repositorio = autoMock.GetMock<IRepositorio<Pergunta>>();
            _request = new CriarPerguntasInvestimentoRequest();
        }

        [Fact]
        public async Task CriaPerguntasDeInvestimento()
        {
            // Arrange
            var perguntas = Pergunta();
            _repositorio.Setup(x => x.AdicionarAsync(perguntas, CancellationToken.None)).ReturnsAsync(Guid.NewGuid());

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
                        new Resposta
                        { 
                            Descricao = "Resposta 1",
                            Pontuacao = 10
                        }
                    }
            };     
        }
    }
}
