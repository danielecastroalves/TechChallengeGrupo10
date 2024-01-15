using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FintechGrupo10.WebApi.Controllers
{
    [ApiController]
    [Route("cliente")]
    public sealed class ClienteController : CommonController
    {
        public ClienteController(IMediator mediator) : base(mediator) { }

        //[HttpGet]
        //[SwaggerOperation(OperationId = "GetCliente")]
        //public async Task<IActionResult> GetCliente()
        //{

        //}

    }
}
