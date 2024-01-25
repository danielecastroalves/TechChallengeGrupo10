using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.PerguntasInvestimento.DeletaPergunta;
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

        public DeletaPerguntaTeste()
        {
            var autoMock = new AutoMocker();
            _handler = autoMock.CreateInstance<DeletaPerguntaHandler>();
            _repositorio = autoMock.GetMock<IRepository<Pergunta>>();
        }

        [Fact]
        public async Task DeletaPerguntasDeInvestimento()
        {
            // Arrange
            var request = new DeletaPerguntaRequest(Guid.NewGuid());

            _repositorio.Setup(x => x.DeleteByIdAsync(It.IsAny<Guid>(), CancellationToken.None)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result);
        }
    }
}
