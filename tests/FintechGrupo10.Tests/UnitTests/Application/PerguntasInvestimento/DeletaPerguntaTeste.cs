﻿using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.Entidades;
using Moq.AutoMock;
using Moq;
using FintechGrupo10.Application.Recursos.PerguntasInvestimento.DeletaPergunta;

namespace FintechGrupo10.Tests.UnitTests.Application.PerguntasInvestimento
{
    public class DeletaPerguntaTeste
    {
        private readonly DeletaPerguntaHandler _handler;
        private readonly Mock<IRepositorio<Pergunta>> _repositorio;
        private readonly DeletaPerguntaRequest _request;

        public DeletaPerguntaTeste()
        {
            var autoMock = new AutoMocker();
            _handler = autoMock.CreateInstance<DeletaPerguntaHandler>();
            _repositorio = autoMock.GetMock<IRepositorio<Pergunta>>();
            _request = new DeletaPerguntaRequest();
        }

        [Fact]
        public async Task DeletaPerguntasDeInvestimento()
        {
            //Arrange
            _repositorio.Setup(x => x.DeletarPorIdAsync(It.IsAny<Guid>(), CancellationToken.None)).ReturnsAsync(true);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.True(result);
        }
    }
}