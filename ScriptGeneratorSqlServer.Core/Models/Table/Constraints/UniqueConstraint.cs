using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGeneratorSqlServer.Core.Models.Table.Constraints
{
    public class UniqueConstraint
    {
        public string ConstraintName { get; set; }
        public IReadOnlyCollection<string> ColumnNames { get; set; }
        public bool IsPrimaryKey { get; set; }
    }
}
