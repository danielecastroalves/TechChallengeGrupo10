using FintechGrupo10.Domain.Entities;
using MediatR;

namespace FintechGrupo10.Application.Features.Client.GetClient
{
    public class GetClientRequest : IRequest<GetClientResponse>
    {
        public string Documento { get; set; } = null!;
    }

    public class GetClientResponse : ClienteEntity { }
}
