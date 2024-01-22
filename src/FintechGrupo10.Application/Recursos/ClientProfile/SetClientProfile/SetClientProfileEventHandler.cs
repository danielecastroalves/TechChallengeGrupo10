using System.Text.Json;
using MediatR;

namespace FintechGrupo10.Application.Recursos.ClientProfile.SetClientProfile
{
    public class SetClientProfileEventHandler : IRequestHandler<SetClientProfileEvent>
    {
        public SetClientProfileEventHandler() { }

        public Task<Unit> Handle(SetClientProfileEvent request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            //TODO - Implementar Lógica do Handler
            Console.WriteLine(JsonSerializer.Serialize(request));

            return Task.FromResult(Unit.Value);
        }
    }
}
