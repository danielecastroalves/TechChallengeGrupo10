using System.Net;
using FintechGrupo10.Application.Features.InvestimentProduct;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FintechGrupo10.WebApi.Controllers
{
    /// <summary>
    /// Investiment Product Controller
    /// </summary>
    [ApiController]
    [Route("v1")]
    public sealed class InvestimentProductController : CommonController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">Mediator DI</param>
        public InvestimentProductController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// GetInvestimentProductByProfile - Get product according to investment profile
        /// </summary>
        /// <param name="investorProfile">Investment Product Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>GetInvestmentProductResponse</returns>
        [HttpGet("investimentProduct/{investorProfile}")]
        [Authorize]
        [SwaggerOperation(OperationId = "GetInvestimentProductByProfileAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "Here is the Investment Product found for this profile"
        )]
        public async Task<IActionResult> GetInvestimentProductByProfileAsync
        (
            [FromRoute] string investorProfile,
            CancellationToken cancellationToken = default
        )
        {
            var response = await _mediator.Send(new GetInvestmentProductRequest(investorProfile), cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// GetAllInvestimentProduct - Get all investiment products
        /// </summary>
        /// <returns>GetInvestmentProductResponse</returns>
        [HttpGet("investimentProduct")]
        [Authorize]
        [SwaggerOperation(OperationId = "GetAllInvestimentProductAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "Here is the Investment Product found for this profile"
        )]
        public async Task<IActionResult> GetAllInvestimentProductAsync()
        {
            var response = await _mediator.Send(new GetInvestmentProductRequest());

            return Ok(response);
        }
    }
}
