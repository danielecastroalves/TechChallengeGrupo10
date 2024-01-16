using FintechGrupo10.Application.Recursos.Login;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FintechGrupo10.WebApi.Controllers
{
    [ApiController]
    [Route("login")]
    public sealed class LoginController : CommonController
    {
        public LoginController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Authenticar(
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(request, cancellationToken));
        }
    }
}