using MediatR;

namespace FintechGrupo10.Application.Features.PerguntasInvestimento.DeletaPergunta
{
    public class DeletaPerguntaRequest : IRequest<bool>
    {
        public DeletaPerguntaRequest(Guid questionId)
        {
            QuestionId = questionId;
        }

        public Guid QuestionId { get; set; }
    }
}
