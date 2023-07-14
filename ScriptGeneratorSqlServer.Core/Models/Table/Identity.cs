using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGeneratorSqlServer.Core.Models.Table
{
    public class Identity
    {
        public int Seed { get; set; }
        public int Incrememt { get; set; }
    }
}
