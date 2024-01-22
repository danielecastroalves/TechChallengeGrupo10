using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Application.Recursos.PerguntasInvestimento.BuscarPerguntas;
using FintechGrupo10.Domain.Entities;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Recursos.PerguntasInvestimento
{
    public class BuscaPerguntasTeste
    {
        private readonly BuscarPerguntasInvestimentoHandler _handler;
        private readonly Mock<IRepository<Pergunta>> _repositorio;
        private readonly BuscarPerguntasInvestimentoRequest _request;

        public BuscaPerguntasTeste()
        {
            var autoMock = new AutoMocker();
            _handler = autoMock.CreateInstance<BuscarPerguntasInvestimentoHandler>();
            _repositorio = autoMock.GetMock<IRepository<Pergunta>>();
            _request = new BuscarPerguntasInvestimentoRequest();
        }

        [Fact]
        public async Task BuscarPerguntasDeInvestimento()
        {
            // Arrange
            var perguntas = ListaDePerguntas();
            _repositorio.Setup(x => x.GetListByFilterAsync(x => x.Ativo, CancellationToken.None)).ReturnsAsync(perguntas);

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
