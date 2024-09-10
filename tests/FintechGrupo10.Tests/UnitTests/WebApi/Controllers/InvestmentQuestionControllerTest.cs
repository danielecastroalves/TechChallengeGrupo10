using FintechGrupo10.Application.Features.InvestmentQuestion.AddInvestmentQuestion;
using FintechGrupo10.Application.Features.InvestmentQuestion.DeleteInvestmentQuestion;
using FintechGrupo10.Application.Features.InvestmentQuestion.GetInvestmentQuestion;
using FintechGrupo10.WebApi.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.WebApi.Controllers
{
    public class InvestmentQuestionControllerTest
    {
        private readonly AutoMocker _mocker;
        private readonly Mock<IMediator> _mediator;
        private readonly InvestmentQuestionController _sut;

        public InvestmentQuestionControllerTest()
        {
            _mocker = new AutoMocker();
            _mediator = _mocker.GetMock<IMediator>();
            _sut = _mocker.CreateInstance<InvestmentQuestionController>();
        }

        [Fact]
        public async Task InvestmentQuestion_AddQuestionAsync_Ok()
        {
            // Arrange
            var request = new AddInvestmentQuestionRequest();

            _mediator
                .Setup(x => x.Send(It.IsAny<AddInvestmentQuestionRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _sut.AddQuestionAsync(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
        }

        [Fact]
        public async Task InvestmentQuestion_GetQuestionsAsync_Ok()
        {
            // Arrange
            _mediator
                .Setup(x => x.Send(It.IsAny<GetInvestmentQuestionRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetInvestmentQuestionsResponse([]));

            // Act
            var result = await _sut.GetQuestionsAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task InvestmentQuestion_DeleteQuestionAsync_Ok()
        {
            // Arrange
            var request = Guid.NewGuid();

            _mediator
                .Setup(x => x.Send(It.IsAny<DeleteInvestmentQuestionRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            // Act
            var result = await _sut.DeleteQuestionAsync(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
