using System.Net;
using FintechGrupo10.Application.Features.Client;
using FintechGrupo10.Application.Features.Client.AddClient;
using FintechGrupo10.Application.Features.Client.DeleteClient;
using FintechGrupo10.Application.Features.Client.GetClient;
using FintechGrupo10.Application.Features.Client.UpdateClient;
using FintechGrupo10.WebApi.Controllers.Comum;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FintechGrupo10.WebApi.Controllers
{
    /// <summary>
    /// Client Controller
    /// </summary>
    [ApiController]
    [Route("v1")]
    public sealed class ClientController : CommonController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">Mediator DI</param>
        public ClientController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// AddClientAsync - Create a new Client
        /// </summary>
        /// <param name="request">AddClient Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpPost("client")]
        [AllowAnonymous]
        [SwaggerOperation(OperationId = "AddClientAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.Created,
            "Client has been created successfully"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.BadRequest,
            "Bad Request - Invalid input or missing required parameters"
        )]
        public async Task<IActionResult> AddClientAsync
        (
            AddClientRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var result = await _mediator.Send(request, cancellationToken);

            return new ObjectResult(result)
            {
                StatusCode = 201
            };
        }

        /// <summary>
        /// GetClientAsync - Get Client from the Document
        /// </summary>
        /// <param name="request">GetClient Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>GetClientResponse</returns>
        [HttpGet("client")]
        [Authorize]
        [SwaggerOperation(OperationId = "GetClientAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "Here is the Client found"
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
        public async Task<IActionResult> GetClientAsync
        (
            [FromQuery] GetClientRequest request,
            CancellationToken cancellationToken
        )
        {
            var client = await _mediator.Send(request, cancellationToken);

            return Ok(client);
        }

        /// <summary>
        /// UpdateClientAsync - Updates a Client register
        /// </summary>
        /// <param name="clientId">Client Id - GUID</param>
        /// <param name="request">UpdateClient Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpPut("client/{clientId}")]
        [Authorize]
        [SwaggerOperation(OperationId = "UpdateClientAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.NoContent,
            "Client has been updated successfully"
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
        public async Task<IActionResult> UpdateClientAsync
        (
            [FromRoute] Guid clientId,
            [FromBody] ClientRequestBase request,
            CancellationToken cancellationToken = default
        )
        {
            var client = request.Adapt<UpdateClientRequest>();
            client.Id = clientId;

            var result = await _mediator.Send(client, cancellationToken);

            return result is null ? NotFound() : NoContent();
        }

        /// <summary>
        /// DeleteClientAsync - Delete a Client register from given id
        /// </summary>
        /// <param name="clientId">Client Id - GUID</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpDelete("client/{clientId}")]
        [Authorize]
        [SwaggerOperation(OperationId = "DeleteClientAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "Client has been deleted successfully"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.BadRequest,
            "Failed to delete Client register"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.Unauthorized,
            "Unauthorized - Invalid credentials or authentication token"
        )]
        public async Task<IActionResult> DeleteClientAsync
        (
            [FromRoute] Guid clientId,
            CancellationToken cancellationToken = default
        )
        {
            await _mediator.Send(new DeleteClientRequest(clientId), cancellationToken);

            return Ok();
        }
    }
}
