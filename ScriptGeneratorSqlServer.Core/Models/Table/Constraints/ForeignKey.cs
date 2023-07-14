using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGeneratorSqlServer.Core.Models.Table.Constraints
{
    public class ForeignKey
    {
        public string ConstraintName { get; set; }
        public string ColumnName { get; set; }
        public string ReferenceTableName { get; set; }
        public string ReferenceTableColumnName { get; set; }
    }
}
