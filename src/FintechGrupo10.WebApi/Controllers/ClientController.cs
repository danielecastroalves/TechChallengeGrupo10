using System.Net;
using FintechGrupo10.Application.Recursos.Cliente.Adicionar;
using FintechGrupo10.Application.Recursos.Cliente.Atualizar;
using FintechGrupo10.Application.Recursos.Cliente.Buscar;
using FintechGrupo10.Application.Recursos.Cliente.Excluir;
using FintechGrupo10.WebApi.Controllers.Comum;
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
        [HttpPost("cliente")]
        [AllowAnonymous]
        [SwaggerOperation(OperationId = "AddClientAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.Created,
            "Client has been created successfully"
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
        [HttpGet("cliente")]
        [Authorize]
        [SwaggerOperation(OperationId = "GetClientAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "Here is the Client found"
        )]
        public async Task<IActionResult> GetClientAsync
        (
            [FromQuery] GetClientRequest request,
            CancellationToken cancellationToken
        )
        {
            var cliente = await _mediator.Send(request, cancellationToken);

            return Ok(cliente);
        }

        /// <summary>
        /// UpdateClientAsync - Updates a Client register
        /// </summary>
        /// <param name="clientId">Client Id - GUID</param>
        /// <param name="request">UpdateClient Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpPut("cliente/{clientId}")]
        [Authorize]
        [SwaggerOperation(OperationId = "UpdateClientAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.NoContent,
            "Client has been updated successfully"
        )]
        public async Task<IActionResult> UpdateClientAsync
        (
            [FromRoute] Guid clientId,
            [FromBody] UpdateClientRequest request,
            CancellationToken cancellationToken = default
        )
        {
            request.Id = clientId;

            var result = await _mediator.Send(request, cancellationToken);

            return result is null ? NotFound() : NoContent();
        }

        /// <summary>
        /// DeleteClientAsync - Delete a Client register from given id
        /// </summary>
        /// <param name="clientId">Client Id - GUID</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpDelete("cliente/{clientId}")]
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
