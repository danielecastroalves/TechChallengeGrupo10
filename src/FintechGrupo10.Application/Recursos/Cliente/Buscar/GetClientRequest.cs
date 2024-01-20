﻿using FintechGrupo10.Domain.Entidades;
using MediatR;

namespace FintechGrupo10.Application.Recursos.Cliente.Buscar
{
    public class GetClientRequest : IRequest<GetClientResponse>
    {
        public string Documento { get; set; } = null!;
    }

    public class GetClientResponse : ClienteEntity { }
}
