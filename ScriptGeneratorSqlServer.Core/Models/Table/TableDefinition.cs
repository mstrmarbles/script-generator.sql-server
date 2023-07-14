using ScriptGeneratorSqlServer.Core.Models.Table.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGeneratorSqlServer.Core.Models.Table
{
    public class TableDefinition
    {
        public string TableName { get; set; }
        public string SchemaName { get; set; }
        public IReadOnlyCollection<Column> Columns { get; set; }
        public IReadOnlyCollection<UniqueConstraint> UniqueConstraints { get; set; }
        public IReadOnlyCollection<ForeignKey> ForeignKeys { get; set; }
    }
}
