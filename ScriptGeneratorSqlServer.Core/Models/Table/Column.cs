using ScriptGeneratorSqlServer.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGeneratorSqlServer.Core.Models.Table
{
    public class Column
    {
        public string Name { get; set; }
        public DataType DataType { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }
        public int? Bytes { get; set; }
        public bool IsNullable { get; set; }
        public Identity? Identity { get; set; }
    }
}
