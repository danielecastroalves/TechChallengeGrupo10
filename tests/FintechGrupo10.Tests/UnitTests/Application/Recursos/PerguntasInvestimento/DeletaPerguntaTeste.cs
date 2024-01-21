using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Application.Recursos.PerguntasInvestimento.DeletaPergunta;
using FintechGrupo10.Domain.Entities;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.Application.Recursos.PerguntasInvestimento
{
    public class DeletaPerguntaTeste
    {
        private readonly DeletaPerguntaHandler _handler;
        private readonly Mock<IRepository<Pergunta>> _repositorio;
        private readonly DeletaPerguntaRequest _request;

        public DeletaPerguntaTeste()
        {
            var autoMock = new AutoMocker();
            _handler = autoMock.CreateInstance<DeletaPerguntaHandler>();
            _repositorio = autoMock.GetMock<IRepository<Pergunta>>();
            _request = new DeletaPerguntaRequest();
        }

        [Fact]
        public async Task DeletaPerguntasDeInvestimento()
        {
            // Arrange
            _repositorio.Setup(x => x.DeleteByIdAsync(It.IsAny<Guid>(), CancellationToken.None)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.True(result);
        }
    }
}
