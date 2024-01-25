using FintechGrupo10.Domain.Entities;
using MediatR;

namespace FintechGrupo10.Application.Recursos.PerguntasInvestimento.BuscarPerguntas
{
    public class BuscarPerguntasInvestimentoRequest : IRequest<GetInvestingQuestionsResponse>
    { }

    public class GetInvestingQuestionsResponse
    {
        public GetInvestingQuestionsResponse(List<Pergunta> investingQuestions)
        {
            InvestingQuestions = investingQuestions;
        }

        public List<Pergunta> InvestingQuestions { get; set; } = null!;
    }
}
