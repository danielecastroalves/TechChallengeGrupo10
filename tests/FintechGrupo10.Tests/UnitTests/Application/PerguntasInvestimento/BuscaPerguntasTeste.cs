using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Application.Recursos.PerguntasInvestimento.BuscarPerguntas;
using FintechGrupo10.Domain.Entidades;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.PerguntasInvestimento
{
    public class BuscaPerguntasTeste
    {
        private readonly BuscarPerguntasInvestimentoHandler _handler;
        private readonly Mock<IRepositorio<Pergunta>> _repositorio;
        private readonly BuscarPerguntasInvestimentoRequest _request;

        public BuscaPerguntasTeste()
        {
            var autoMock = new AutoMocker();
            _handler = autoMock.CreateInstance<BuscarPerguntasInvestimentoHandler>();
            _repositorio = autoMock.GetMock<IRepositorio<Pergunta>>();
            _request = new BuscarPerguntasInvestimentoRequest();
        }

        [Fact]
        public async Task BuscarPerguntasDeInvestimento()
        {
            // Arrange
            var perguntas = ListaDePerguntas();
            _repositorio.Setup(x => x.ObterListaPorFiltroAsync(x => x.Ativo, CancellationToken.None)).ReturnsAsync(perguntas);

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        private static List<Pergunta> ListaDePerguntas()
        {
            return new List<Pergunta>
            {
                new()
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
                }
            };
        }
    }
}
