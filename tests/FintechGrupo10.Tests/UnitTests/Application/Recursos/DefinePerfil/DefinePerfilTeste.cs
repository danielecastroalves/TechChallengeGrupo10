using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Application.Recursos.DefinePerfil;
using FintechGrupo10.Domain.Entities;
using FintechGrupo10.Domain.Eventos;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Recursos.DefinePerfil
{
    public class DefinePerfilTeste
    {
        private readonly DefinePerfilRequestHandler _handler;
        private readonly Mock<IRepository<ClienteEntity>> _repositorio;
        private readonly DefinePerfilRequest _request;

        public DefinePerfilTeste()
        {
            var autoMock = new AutoMocker();
            _handler = autoMock.CreateInstance<DefinePerfilRequestHandler>();
            _repositorio = autoMock.GetMock<IRepository<ClienteEntity>>();
            _request = Request();
        }

        [Fact]
        public async Task BuscarPerguntasDeInvestimento()
        {
            // Arrange
            var perguntas = ListaDePerguntas();
            var cliente = Cliente();

            _repositorio.Setup(x => x.GetByFilterAsync(x => x.Documento == "123456", CancellationToken.None)).ReturnsAsync(cliente);
            _repositorio.Setup(x => x.UpdateAsync(x => x.Documento == "123456", It.IsAny<ClienteEntity>(), It.IsAny<CancellationToken>()));

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.True(result);
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

        private static ClienteEntity Cliente()
        {
            return new ClienteEntity
            {
                Documento = "123456"
            };
        }

        private static DefinePerfilRequest Request()
        {
            return new DefinePerfilRequest
            {
                EventoPerfil = new EventoPerfil
                {
                    Documento = "123456",
                    PerguntasRespondidas = new List<Pergunta>
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
                    }
                }
            };
        }
    }
}
