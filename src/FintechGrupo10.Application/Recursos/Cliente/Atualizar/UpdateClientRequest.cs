using FintechGrupo10.Domain.Entidades;
using MediatR;

namespace FintechGrupo10.Application.Recursos.Cliente.Atualizar
{
    public class UpdateClientRequest : ClientRequestBase, IRequest<ClienteEntity?> 
    {
        public Guid Id { get; set; }
    }
}
