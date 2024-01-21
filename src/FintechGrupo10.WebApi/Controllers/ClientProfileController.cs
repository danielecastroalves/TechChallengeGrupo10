using FintechGrupo10.Application.Recursos.ClientProfile;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FintechGrupo10.WebApi.Controllers
{
    [ApiController]
    [Route("v1")]
    public sealed class ClientProfileController : CommonController
    {
        public ClientProfileController(IMediator mediator) : base(mediator) { }

        [HttpPost("clientProfile")]
        [SwaggerOperation(OperationId = "SetClientProfile")]
        public async Task<IActionResult> SetClientProfile
        (
            ClientProfileRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var result = await _mediator.Send(request, cancellationToken);

            return new ObjectResult(result)
            {
                StatusCode = 201
            };
        }
    }
}
