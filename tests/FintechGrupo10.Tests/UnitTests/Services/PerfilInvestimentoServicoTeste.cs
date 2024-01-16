using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.DTOS;
using FintechGrupo10.Domain.Entidades;
using FintechGrupo10.Domain.Enums;
using FintechGrupo10.Infrastructure.Services;
using Moq;
using Moq.AutoMock;

namespace FintechGrupo10.Tests.UnitTests.Services
{
    public class PerfilInvestimentoServicoTeste
    {
        private readonly PerfilInvestimentoServico _service;
        private readonly Mock<IClienteRepositorio> _clienteRepositorio;

        public PerfilInvestimentoServicoTeste()
        {
            var autoMock = new AutoMocker();
            _service = autoMock.CreateInstance<PerfilInvestimentoServico>();
            _clienteRepositorio = autoMock.GetMock<IClienteRepositorio>();
        }

        [Fact(DisplayName = "Define um perfil de investimento para o cliente")]
        public async Task DefinePerfil()
        {
            //Arrange
            var entrada = Entrada();
            _clienteRepositorio.Setup(x => x.AtualizaPerfilInvestimento(It.IsAny<string>(), It.IsAny<PerfilInvestimento>())).ReturnsAsync(true);

            //Act
            var resultado = await _service.DefinicaoPerfilInvestimento(entrada);

            //Assert
            Assert.True(resultado);
        }

        private PerguntasResponditasDTO Entrada()
        {
            return new PerguntasResponditasDTO 
            {
                Documento = "123456",
                Perguntas =
                    new List<Pergunta>
                    {
                        new Pergunta
                        {
                            Titulo = "Pergunta 1",
                            Resposta = new List<Resposta> {
                                new Resposta {
                                    Id = 1,
                                    Descricao = "Resposta 1",
                                    Pontuacao = 3,
                                },
                                new Resposta {
                                    Id = 1,
                                    Descricao = "Resposta 2",
                                    Pontuacao = 5,
                                },
                                new Resposta {
                                    Id = 1,
                                    Descricao = "Resposta 3",
                                    Pontuacao = 10,
                                },
                            }
                        },
                        new Pergunta
                        {
                            Titulo = "Pergunta 1",
                            Resposta = new List<Resposta> {
                                new Resposta {
                                    Id = 1,
                                    Descricao = "Resposta 1",
                                    Pontuacao = 3,
                                },
                                new Resposta {
                                    Id = 1,
                                    Descricao = "Resposta 2",
                                    Pontuacao = 5,
                                },
                                new Resposta {
                                    Id = 1,
                                    Descricao = "Resposta 3",
                                    Pontuacao = 10,
                                },
                            }
                        },
                    }
            };
        }
    }
}
