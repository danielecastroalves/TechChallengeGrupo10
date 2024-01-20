using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.Entities;
using MediatR;

namespace FintechGrupo10.Application.Recursos.PerguntasInvestimento.DeletaPergunta
{
    public class DeletaPerguntaHandler : IRequestHandler<DeletaPerguntaRequest, bool>
    {
        private readonly IRepositorio<Pergunta> _repositorio;

        public DeletaPerguntaHandler(IRepositorio<Pergunta> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> Handle(DeletaPerguntaRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return await _repositorio.DeletarPorIdAsync(request.IdPergunta, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
