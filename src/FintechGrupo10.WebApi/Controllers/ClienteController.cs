using FintechGrupo10.Application.Recursos.Cliente.Adicionar;
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

        [HttpPost("cliente")]
        [SwaggerOperation(OperationId = "AddClienteAsync")]
        public async Task<IActionResult> AddClienteAsync
        (
            AddClienteRequest request, 
            CancellationToken cancellationToken = default
        )
        {
            var result = await _mediator.Send(request, cancellationToken);

            return new ObjectResult(result)
            {
                StatusCode = 201
            };
        }
        
        
        //[HttpGet]
        //[SwaggerOperation(OperationId = "GetCliente")]
        //public async Task<IActionResult> GetCliente()
        //{

        //}


    }
}
