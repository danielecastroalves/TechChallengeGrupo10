using FintechGrupo10.Application.Recursos.Login;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FintechGrupo10.WebApi.Controllers
{
    [ApiController]
    [Route("login")]
    public sealed class LoginController : CommonController
    {
        public LoginController(IMediator mediator) : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<dynamic>> Autenticar(
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            var token = await _mediator.Send(request, cancellationToken);

            if (string.IsNullOrWhiteSpace(token))
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }
            else
            {
                request.Senha = "";

                return new
                {
                    user = request,
                    token
                };
            }
        }
    }
}
