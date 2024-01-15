using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FintechGrupo10.WebApi.Controllers.Comum
{
    public abstract class CommonController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public CommonController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
