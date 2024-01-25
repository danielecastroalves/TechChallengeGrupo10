using FintechGrupo10.Domain.Entities;
using MediatR;

namespace FintechGrupo10.Application.Features.Client.UpdateClient
{
    public class UpdateClientRequest : ClientRequestBase, IRequest<ClienteEntity?>
    {
        public Guid Id { get; set; }
    }
}
