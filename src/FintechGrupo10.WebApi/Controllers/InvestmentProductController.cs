using System.Net;
using FintechGrupo10.Application.Features.InvestmentProduct.AddInvestmentProduct;
using FintechGrupo10.Application.Features.InvestmentProduct.GetInvestmentProduct;
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
    [ApiController]
    [Route("v1")]
    public sealed class InvestmentProductController : CommonController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">Mediator DI</param>
        public InvestmentProductController(IMediator mediator) : base(mediator) { }

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
        public async Task<IActionResult> GetAllInvestmentProductAsync()
        {
            var response = await _mediator.Send(new GetInvestmentProductRequest());

            return Ok(response);
        }
    }
}
