using FintechGrupo10.Application.Recursos.ClientProfile.SendClientProfile;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FintechGrupo10.WebApi.Controllers
{
    [ApiController]
    [Route("v1")]
    public sealed class ClientProfileController : CommonController
    {
        public ClientProfileController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize]
        [HttpPost("clientProfile")]
        [SwaggerOperation(OperationId = "SendClientProfile")]
        public async Task<IActionResult> SendClientProfile
        (
            SendClientProfileRequest request,
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
