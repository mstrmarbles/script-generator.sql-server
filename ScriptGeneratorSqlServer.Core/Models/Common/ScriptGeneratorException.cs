using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGeneratorSqlServer.Core.Models.Common
{
    public class ScriptGeneratorException : Exception
    {
        public ScriptGeneratorException(string message): base(message) { }
    }
}
