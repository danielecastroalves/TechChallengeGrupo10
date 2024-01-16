using FintechGrupo10.Application.Recursos.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FintechGrupo10.WebApi.Controllers;

[ApiController]
[Route("login")]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Authenticar(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }
}