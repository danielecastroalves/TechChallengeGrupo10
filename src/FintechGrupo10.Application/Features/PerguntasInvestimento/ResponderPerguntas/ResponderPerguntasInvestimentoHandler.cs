using MediatR;

namespace FintechGrupo10.Application.Features.PerguntasInvestimento.ResponderPerguntas
{
    internal class ResponderPerguntasInvestimentoHandler : IRequestHandler<ResponderPerguntasInvestimentoRequest, bool>
    {
        public ResponderPerguntasInvestimentoHandler()
        {

        }

        public Task<bool> Handle(ResponderPerguntasInvestimentoRequest request, CancellationToken cancellationToken)
        {
            //TODO: Implementar o envio de evento para o serviço que define o perfil do cliente
            throw new NotImplementedException();
        }
    }
}
