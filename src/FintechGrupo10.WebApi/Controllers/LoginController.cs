using FintechGrupo10.Application.Recursos.Login;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FintechGrupo10.WebApi.Controllers;

[ApiController]
[Route("login")]
public class LoginController : CommonController
{
    public LoginController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<ActionResult<string>> Authenticar(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }
}