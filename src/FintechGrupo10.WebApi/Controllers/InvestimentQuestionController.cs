using System.Net;
using FintechGrupo10.Application.Features.InvestmentQuestion.AddInvestmentQuestion;
using FintechGrupo10.Application.Features.InvestmentQuestion.DeleteInvestmentQuestion;
using FintechGrupo10.Application.Features.InvestmentQuestion.GetInvestmentQuestion;
using FintechGrupo10.WebApi.Controllers.Comum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FintechGrupo10.WebApi.Controllers
{
    /// <summary>
    /// Investment Question Controller
    /// </summary>
    [ApiController]
    [Route("v1")]
    public class InvestmentQuestionController : CommonController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">Mediator DI</param>
        public InvestmentQuestionController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// AddQuestionAsync - Create a new Investment Question for Profile Definition
        /// </summary>
        /// <param name="request">AddInvestmentQuestion Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpPost("investmentQuestion")]
        [Authorize]
        [SwaggerOperation(OperationId = "AddQuestionAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.Created,
            "Question has been created successfully"
        )]
        public async Task<IActionResult> AddQuestionAsync
        (
            AddInvestmentQuestionRequest request,
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
        /// GetQuestionsAsync - Returns all Investment Questions
        /// </summary>
        /// <returns>GetInvestmentQuestionsResponse</returns>
        [HttpGet("investmentQuestion")]
        [Authorize]
        [SwaggerOperation(OperationId = "GetQuestionsAsync")]
        [SwaggerResponse
        (
            (int)HttpStatusCode.OK,
            "These are the Investment Questions found"
        )]
        public async Task<IActionResult> GetQuestionsAsync()
        {
            return Ok(await _mediator.Send(new GetInvestmentQuestionRequest()));
        }

        /// <summary>
        /// DeleteQuestionAsync - Delete a Investment Question from given id
        /// </summary>
        /// <param name="questionId">Question Id - GUID</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpDelete("investmentQuestion/{questionId}")]
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
            await _mediator.Send(new DeleteInvestmentQuestionRequest(questionId), cancellationToken);

            return Ok();
        }
    }
}
