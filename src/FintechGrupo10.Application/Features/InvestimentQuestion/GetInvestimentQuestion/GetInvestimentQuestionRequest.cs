using FintechGrupo10.Domain.Entities;
using MediatR;

namespace FintechGrupo10.Application.Features.InvestmentQuestion.GetInvestmentQuestion
{
    public class GetInvestmentQuestionRequest : IRequest<GetInvestmentQuestionsResponse>
    { }

    public class GetInvestmentQuestionsResponse
    {
        public GetInvestmentQuestionsResponse(List<QuestionEntity> investmentQuestions)
        {
            InvestmentQuestions = investmentQuestions;
        }

        public List<QuestionEntity> InvestmentQuestions { get; set; }
    }
}
