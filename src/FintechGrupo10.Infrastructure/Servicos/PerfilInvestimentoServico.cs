using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Application.Comum.Servicos;
using FintechGrupo10.Domain.DTOS;
using FintechGrupo10.Domain.Enums;

namespace FintechGrupo10.Infrastructure.Services
{
    public class PerfilInvestimentoServico : IPerfilInvestimentoServico
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public PerfilInvestimentoServico(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public async Task<bool> DefinicaoPerfilInvestimento(PerguntasResponditasDTO evento)
        {
            var pontuacaoTotal = 0;

            foreach (var pergunta in evento.Perguntas)
            {
                pontuacaoTotal = +pergunta.Resposta.First().Pontuacao;
            }

            var perfil = DefinePerfil(pontuacaoTotal);


            return await AtualizaPerfil(perfil, evento.Documento);
        }

        private PerfilInvestimento DefinePerfil(int pontuacaoTotal)
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
            var resultado = await _clienteRepositorio.AtualizaPerfilInvestimento(documento, perfilInvestimento);

            return resultado;
        }
    }
}
