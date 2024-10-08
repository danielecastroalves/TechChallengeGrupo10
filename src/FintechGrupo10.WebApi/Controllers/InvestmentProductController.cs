using System.Net;
using FintechGrupo10.Application.Features.InvestmentProduct.AddInvestmentProduct;
using FintechGrupo10.Application.Features.InvestmentProduct.BuyProduct;
using FintechGrupo10.Application.Features.InvestmentProduct.GetInvestmentProduct;
using FintechGrupo10.Application.Features.InvestmentProduct.SellProduct;
using FintechGrupo10.Domain.Enums;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FintechGrupo10.WebApi.Controllers
{
    /// <summary>
    /// Investment Product Controller
    /// </summary>
    /// <param name="mediator">Mediator DI</param>
    [ApiController]
    [Route("v1")]
    public sealed class InvestmentProductController(IMediator mediator) : CommonController(mediator)
    {
        /// <summary>
        /// AddInvestmentProductAsync - Create a new Product for Investors
        /// </summary>
        /// <param name="request">AddInvestmentProduct Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpPost("investmentProduct")]
        [Authorize]
        [SwaggerOperation(OperationId = "AddInvestmentProductAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.Created,
            "Product has been created successfully"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.BadRequest,
            "Bad Request - Invalid input or missing required parameters"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.Unauthorized,
            "Unauthorized - Invalid credentials or authentication token"
        )]
        public async Task<IActionResult> AddInvestmentProductAsync
        (
            AddInvestmentProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var result = await _mediator.Send(request, cancellationToken);

            return new ObjectResult(result)
            {
                StatusCode = 201
            };
        }

        /// <summary>
        /// GetInvestmentProductByProfileAsync - Get product according to investment profile
        /// </summary>
        /// <param name="investorProfile">Investment Product Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>GetInvestmentProductResponse</returns>
        [HttpGet("investmentProduct/investorProfile/")]
        [Authorize]
        [SwaggerOperation(OperationId = "GetInvestmentProductByProfileAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "Here is the Investment Products found for this profile"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.BadRequest,
            "Bad Request - Invalid input or missing required parameters"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.Unauthorized,
            "Unauthorized - Invalid credentials or authentication token"
        )]
        public async Task<IActionResult> GetInvestmentProductByProfileAsync
        (
            [FromQuery] InvestorProfile investorProfile,
            CancellationToken cancellationToken = default
        )
        {
            var response = await _mediator.Send(new GetInvestmentProductRequest(investorProfile), cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// GetAllInvestmentProductAsync - Get all investment products
        /// </summary>
        /// <returns>GetInvestmentProductResponse</returns>
        [HttpGet("investmentProduct")]
        [Authorize]
        [SwaggerOperation(OperationId = "GetAllInvestmentProductAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "Here is all the Investment Products found"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.BadRequest,
            "Bad Request - Invalid input or missing required parameters"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.Unauthorized,
            "Unauthorized - Invalid credentials or authentication token"
        )]
        public async Task<IActionResult> GetAllInvestmentProductAsync()
        {
            var response = await _mediator.Send(new GetInvestmentProductRequest());

            return Ok(response);
        }

        /// <summary>
        /// BuyProductAsync - Buy a Investment Product
        /// </summary>
        /// <param name="request">BuyProductRequest Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpPost("investmentProduct/buyProduct")]
        [Authorize]
        [SwaggerOperation(OperationId = "BuyProductAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "Product has been purchased successfully"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.BadRequest,
            "Bad Request - Invalid input or missing required parameters"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.NotFound,
            "Not Found - Client not found"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.Unauthorized,
            "Unauthorized - Invalid credentials or authentication token"
        )]
        public async Task<IActionResult> BuyProductAsync
        (
            BuyProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result)
                return NotFound("Cliente não encontrado");

            return Ok("Ordem de compra enviada");
        }

        /// <summary>
        /// SellProductAsync - Sell a Investment Product
        /// </summary>
        /// <param name="request">SellProductRequest Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpPost("investmentProduct/sellProduct")]
        [Authorize]
        [SwaggerOperation(OperationId = "SellProductAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "Product has been purchased successfully"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.BadRequest,
            "Bad Request - Invalid input or missing required parameters"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.NotFound,
            "Not Found - Client not found"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.Unauthorized,
            "Unauthorized - Invalid credentials or authentication token"
        )]
        public async Task<IActionResult> SellProductAsync
        (
            SellProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result)
                return NotFound("Cliente não encontrado");

            return Ok("Ordem de venda enviada");
        }
    }
}
