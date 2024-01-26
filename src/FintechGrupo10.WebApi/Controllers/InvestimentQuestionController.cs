using System.Net;
using FintechGrupo10.Application.Features.InvestimentQuestion.AddInvestimentQuestion;
using FintechGrupo10.Application.Features.InvestimentQuestion.DeleteInvestimentQuestion;
using FintechGrupo10.Application.Features.InvestimentQuestion.GetInvestimentQuestion;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FintechGrupo10.WebApi.Controllers
{
    /// <summary>
    /// Investiment Question Controller
    /// </summary>
    [ApiController]
    [Route("v1")]
    public class InvestimentQuestionController : CommonController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">Mediator DI</param>
        public InvestimentQuestionController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// AddQuestionAsync - Create a new Investiment Question for Profile Definition
        /// </summary>
        /// <param name="request">AddInvestimentQuestion Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpPost("investimentQuestion")]
        [Authorize]
        [SwaggerOperation(OperationId = "AddQuestionAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.Created,
            "Question has been created successfully"
        )]
        public async Task<IActionResult> AddQuestionAsync
        (
            AddInvestimentQuesitonRequest request,
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
        /// GetQuestionsAsync - Returns all Investiment Questions
        /// </summary>
        /// <returns>GetInvestimentQuestionsResponse</returns>
        [HttpGet("investimentQuestion")]
        [Authorize]
        [SwaggerOperation(OperationId = "GetQuestionsAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "These are the Investiment Questions found"
        )]
        public async Task<IActionResult> GetQuestionsAsync()
        {
            return Ok(await _mediator.Send(new GetInvestimentQuestionRequest()));
        }

        /// <summary>
        /// DeleteQuestionAsync - Delete a Investiment Question from given id
        /// </summary>
        /// <param name="questionId">Question Id - GUID</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpDelete("investimentQuestion/{questionId}")]
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
            await _mediator.Send(new DeleteInvestimentQuestionRequest(questionId), cancellationToken);

            return Ok();
        }
    }
}
