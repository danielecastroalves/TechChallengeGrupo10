using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FintechGrupo10.Domain.Entidades
{
    public class Pergunta
    {
        public string Titulo { get; set; } = null!;
        public bool Resposta  { get; set; }
        public string Pontuacao { get; set; } = null!;
    }
}
