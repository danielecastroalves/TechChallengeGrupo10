using FintechGrupo10.Application.Features.Login;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FintechGrupo10.WebApi.Controllers
{
    /// <summary>
    /// Login Controller
    /// </summary>
    [ApiController]
    [Route("v1/login")]
    public sealed class LoginController : CommonController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">Mediator DI</param>
        public LoginController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="request">Login Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            var token = await _mediator.Send(request, cancellationToken);

            if (string.IsNullOrWhiteSpace(token))
            {
                return NotFound(new { message = "Invalid User or Password" });
            }
            else
            {
                request.Password = "";

                return new
                {
                    user = request,
                    token
                };
            }
        }
    }
}
