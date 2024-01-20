using MediatR;

namespace FintechGrupo10.Application.Recursos.Cliente.Excluir
{
    public class DeleteClientRequest(Guid clientID) : IRequest;
}
