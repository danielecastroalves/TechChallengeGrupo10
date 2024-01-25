using FintechGrupo10.Domain.Entities;
using MediatR;

namespace FintechGrupo10.Application.Features.InvestimentQuestion.GetInvestimentQuestion
{
    public class GetInvestimentQuestionRequest : IRequest<GetInvestimentQuestionsResponse>
    { }

    public class GetInvestimentQuestionsResponse
    {
        public GetInvestimentQuestionsResponse(List<Question> investimentQuestions)
        {
            InvestimentQuestions = investimentQuestions;
        }

        public List<Question> InvestimentQuestions { get; set; }
    }
}
