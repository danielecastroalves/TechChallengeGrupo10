using System.Net;
using FintechGrupo10.Application.Features.ClientProfile.SendClientProfile;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FintechGrupo10.WebApi.Controllers
{
    /// <summary>
    /// Client Profile Controller
    /// </summary>
    [ApiController]
    [Route("v1")]
    public sealed class ClientProfileController : CommonController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">Mediator DI</param>
        public ClientProfileController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// SendClientProfile - Send the questions and answers to set the client profile
        /// </summary>
        /// <param name="request">SendClientProfile Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpPost("clientProfile")]
        [Authorize]
        [SwaggerOperation(OperationId = "SendClientProfile")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "The list was sent successfully"
        )]
        public async Task<IActionResult> SendClientProfile
        (
            SendClientProfileRequest request,
            CancellationToken cancellationToken = default
        )
        {
            await _mediator.Send(request, cancellationToken);

            return Ok();
        }
    }
}
