using FintechGrupo10.Domain.Eventos;
using MediatR;

namespace FintechGrupo10.Application.Recursos.DefinePerfil
{
    public class DefinePerfilRequest : IRequest<bool>
    {
        public EventoPerfil EventoPerfil { get; set; }
    }
}
