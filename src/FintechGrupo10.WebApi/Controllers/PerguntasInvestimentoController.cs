using FintechGrupo10.Application.Comum.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace FintechGrupo10.WebApi.Controllers
{
    [ApiController]
    [Route("PerguntasDeInvestimento")]
    public class PerguntasInvestimentoController : ControllerBase
    {
        private readonly IPerguntasInvestimentoServico _perguntasInvestimentoServico;

        public PerguntasInvestimentoController(IPerguntasInvestimentoServico perguntasInvestimentoServico)
        {
            _perguntasInvestimentoServico = perguntasInvestimentoServico;
        }

        [HttpGet]
        public async Task<IActionResult> BuscaPerguntasDeInvestimento()
        {
            return Ok(await _perguntasInvestimentoServico.BuscaPerguntasInvestimento());
        }
    }
}
