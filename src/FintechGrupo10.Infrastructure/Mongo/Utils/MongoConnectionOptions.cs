using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FintechGrupo10.Infrastructure.Mongo.Utils
{
    public class MongoConnectionOptions
    {
        public string? ConnectionString { get; set; }
        public string? Schema { get; set; }
        public int DefaultTtlDays { get; set; }
    }
}
