using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Domain.Entities;
using FintechGrupo10.Domain.Enums;
using MediatR;

namespace FintechGrupo10.Application.Features.DefinePerfil
{
    public class DefinePerfilRequestHandler : IRequestHandler<DefinePerfilRequest, bool>
    {
        private readonly IRepository<ClienteEntity> _repositorio;

        public DefinePerfilRequestHandler(IRepository<ClienteEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> Handle(DefinePerfilRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                return false;

            var pontuacaoTotal = 0;

            foreach (var pergunta in request.EventoPerfil.PerguntasRespondidas)
            {
                pontuacaoTotal = +pergunta.Resposta.First().Pontuacao;
            }

            var perfil = DefinePerfil(pontuacaoTotal);


            return await AtualizaPerfil(perfil, request.EventoPerfil.Documento);
        }

        private static PerfilInvestimento DefinePerfil(int pontuacaoTotal)
        {
            PerfilInvestimento perfil = PerfilInvestimento.Indefinido;

            if (pontuacaoTotal <= 20)
                perfil = PerfilInvestimento.Conservador;

            if (pontuacaoTotal > 20 && pontuacaoTotal <= 40)
                perfil = PerfilInvestimento.Moderado;

            if (pontuacaoTotal > 40)
                perfil = PerfilInvestimento.Agressivo;

            return perfil;
        }

        private async Task<bool> AtualizaPerfil(PerfilInvestimento perfilInvestimento, string documento)
        {
            var entidade = await _repositorio.GetByFilterAsync(x => x.Documento == documento);
            if (entidade is null)
                return false;

            entidade.PerfilInvestimento = perfilInvestimento;

            try
            {
                await _repositorio.UpdateAsync(x => x.Documento == documento, entidade, CancellationToken.None);
                return true;
            }
            catch (Exception)
            {
                throw;
            }          
        }
    }
}
