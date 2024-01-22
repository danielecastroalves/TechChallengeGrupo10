using FintechGrupo10.Application.Recursos.PerguntasInvestimento.BuscarPerguntas;
using FintechGrupo10.Application.Recursos.PerguntasInvestimento.CriarPergunta;
using FintechGrupo10.Application.Recursos.PerguntasInvestimento.DeletaPergunta;
using FintechGrupo10.Application.Recursos.PerguntasInvestimento.ResponderPerguntas;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FintechGrupo10.WebApi.Controllers
{
    [ApiController]
    [Route("PerguntasDeInvestimento")]
    public class PerguntasInvestimentoController : CommonController
    {
        public PerguntasInvestimentoController(IMediator mediator) : base(mediator) { }

        [HttpGet("busca-perguntas")]
        public async Task<IActionResult> GetQuestions()
        {
            return Ok(await _mediator.Send(new BuscarPerguntasInvestimentoRequest(), CancellationToken.None));
        }

        [HttpPost("cria-pergunta")]
        public async Task<IActionResult> CreateQuestion([FromBody] CriarPerguntasInvestimentoRequest request,
            CancellationToken cancellationToken)
        {
            if(request == null) throw new ArgumentNullException(nameof(request));

            return Ok(await _mediator.Send(request, cancellationToken));
        }

        [HttpPost("responde-perguntas")]
        public async Task<IActionResult> AnswerQuestions([FromBody] ResponderPerguntasInvestimentoRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return Ok(await _mediator.Send(request, cancellationToken));
        }

        [HttpDelete("deleta-pergunta")]
        public async Task<IActionResult> DeleteQuestions([FromBody][Required] DeletaPerguntaRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return Ok(await _mediator.Send(request, cancellationToken));
        }
    }
}
