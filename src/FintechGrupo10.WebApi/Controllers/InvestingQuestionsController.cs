using System.Net;
using FintechGrupo10.Application.Features.PerguntasInvestimento.BuscarPerguntas;
using FintechGrupo10.Application.Features.PerguntasInvestimento.CriarPergunta;
using FintechGrupo10.Application.Features.PerguntasInvestimento.DeletaPergunta;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FintechGrupo10.WebApi.Controllers
{
    /// <summary>
    /// InvestingQuestions Controller
    /// </summary>
    [ApiController]
    [Route("v1")]
    public class InvestingQuestionsController : CommonController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">Mediator DI</param>
        public InvestingQuestionsController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// CreateQuestionAsync - Create a new Investing Question for Profile Definition
        /// </summary>
        /// <param name="request">CreateInvestingQuestion Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpPost("investingQuestions")]
        [Authorize]
        [SwaggerOperation(OperationId = "CreateQuestionAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.Created,
            "Question has been created successfully"
        )]
        public async Task<IActionResult> CreateQuestionAsync
        (
            CriarPerguntasInvestimentoRequest request,
            CancellationToken cancellationToken
        )
        {
            var result = await _mediator.Send(request, cancellationToken);

            return new ObjectResult(result)
            {
                StatusCode = 201
            };
        }

        /// <summary>
        /// GetQuestionsAsync - Returns all Investing Questions
        /// </summary>
        /// <returns></returns>
        [HttpGet("investingQuestions")]
        [Authorize]
        [SwaggerOperation(OperationId = "GetQuestionsAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "These are the Investing Questions found"
        )]
        public async Task<IActionResult> GetQuestionsAsync()
        {
            return Ok(await _mediator.Send(new BuscarPerguntasInvestimentoRequest(), CancellationToken.None));
        }

        /// <summary>
        /// DeleteQuestionAsync - Delete a Investing Question from given id
        /// </summary>
        /// <param name="questionId">Question Id - GUID</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        [HttpDelete("investingQuestions/{questionId}")]
        [Authorize]
        [SwaggerOperation(OperationId = "DeleteQuestionAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "Question has been deleted successfully"
        )]
        [SwaggerResponse
        (
            (int)HttpStatusCode.BadRequest,
            "Failed to delete Question register"
        )]
        public async Task<IActionResult> DeleteQuestionAsync
        (
            [FromRoute] Guid questionId,
            CancellationToken cancellationToken
        )
        {
            await _mediator.Send(new DeletaPerguntaRequest(questionId), cancellationToken);

            return Ok();
        }
    }
}
