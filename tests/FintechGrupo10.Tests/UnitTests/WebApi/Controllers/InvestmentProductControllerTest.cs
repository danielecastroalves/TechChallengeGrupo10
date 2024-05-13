using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FintechGrupo10.Application.Features.ClientProfile.SendClientProfile;
using FintechGrupo10.Application.Features.InvestmentProduct.AddInvestmentProduct;
using FintechGrupo10.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using Moq;
using Xunit;
using FluentAssertions;
using FintechGrupo10.Application.Features.InvestmentProduct.BuyProduct;
using FintechGrupo10.Application.Features.InvestmentProduct.GetInvestmentProduct;
using FintechGrupo10.Domain.Entities;
using FintechGrupo10.Domain.Enums;

namespace FintechGrupo10.Tests.UnitTests.WebApi.Controllers
{
    public class InvestmentProductControllerTest
    {
        private readonly AutoMocker _mocker;
        private readonly Mock<IMediator> _mediator;
        private readonly InvestmentProductController _sut;

        public InvestmentProductControllerTest()
        {
            _mocker = new AutoMocker();
            _mediator = _mocker.GetMock<IMediator>();
            _sut = _mocker.CreateInstance<InvestmentProductController>();
        }

        [Fact]
        public async Task InvestmentProduct_AddInvestmentProductAsync_Success()
        {
            // Arrange
            var request = new AddInvestmentProductRequest();

            _mediator
                .Setup(x => x.Send(It.IsAny<AddInvestmentProductRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _sut.AddInvestmentProductAsync(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
        }

        [Fact]
        public async Task InvestmentProduct_GetInvestmentProductByProfileAsync_Success()
        {
            // Arrange
            var request = InvestorProfile.Agressivo;

            _mediator
                .Setup(x => x.Send(It.IsAny<GetInvestmentProductRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetInvestmentProductResponse(new List<ProductEntity>()));

            // Act
            var result = await _sut.GetInvestmentProductByProfileAsync(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task InvestmentProduct_GetAllInvestmentProductAsync_Success()
        {
            // Arrange
            var request = new GetInvestmentProductRequest();

            _mediator
                .Setup(x => x.Send(It.IsAny<GetInvestmentProductRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetInvestmentProductResponse(new List<ProductEntity>()));

            // Act
            var result = await _sut.GetAllInvestmentProductAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task InvestmentProduct_BuyProductAsync_Success()
        {
            // Arrange
            var request = new BuyProductRequest();

            _mediator
                .Setup(x => x.Send(It.IsAny<BuyProductRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.BuyProductAsync(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task InvestmentProduct_BuyProductAsync_NotFoud_Success()
        {
            // Arrange
            var request = new BuyProductRequest();

            _mediator
                .Setup(x => x.Send(It.IsAny<BuyProductRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _sut.BuyProductAsync(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
