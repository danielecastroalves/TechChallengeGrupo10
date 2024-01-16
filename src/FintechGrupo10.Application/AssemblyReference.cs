using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace FintechGrupo10.Application
{
    [ExcludeFromCodeCoverage]
    public class AssemblyReference
    {
        public Assembly GetAssembly()
        {
            return GetType().Assembly;
        }
    }
}
