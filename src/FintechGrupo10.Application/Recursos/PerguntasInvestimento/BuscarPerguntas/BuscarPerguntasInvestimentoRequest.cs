﻿using FintechGrupo10.Domain.Entities;
using MediatR;

namespace FintechGrupo10.Application.Recursos.PerguntasInvestimento.BuscarPerguntas
{
    public class BuscarPerguntasInvestimentoRequest : IRequest<List<Pergunta>>
    {
    }
}
