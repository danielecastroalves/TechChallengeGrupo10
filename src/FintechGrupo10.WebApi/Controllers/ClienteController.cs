using FintechGrupo10.Application.Recursos.Cliente.Adicionar;
using FintechGrupo10.Application.Recursos.Cliente.Atualizar;
using FintechGrupo10.Application.Recursos.Cliente.Buscar;
using FintechGrupo10.Application.Recursos.Cliente.Excluir;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FintechGrupo10.WebApi.Controllers
{
    [ApiController]
    [Route("v1")]
    public sealed class ClienteController : CommonController
    {
        public ClienteController(IMediator mediator) : base(mediator) { }

        // FEITO
        [HttpPost("cliente")]
        [SwaggerOperation(OperationId = "AddClientAsync")]
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

        // FEITO
        [HttpGet("cliente")]
        [SwaggerOperation(OperationId = "GetClientAsync")]
        public async Task<IActionResult> GetClientAsync
        (
            [FromQuery] GetClientRequest request,
            CancellationToken cancellationToken
        )
        {
            var cliente = await _mediator.Send(request, cancellationToken);

            return Ok(cliente);
        }

        // FEITO
        [HttpPut("cliente/{clientId}")]
        [SwaggerOperation(OperationId = "UpdateClientAsync")]
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

        // FEITO
        [HttpDelete("cliente/{clientId}")]
        [SwaggerOperation(OperationId = "DeleteClientAsync")]
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
