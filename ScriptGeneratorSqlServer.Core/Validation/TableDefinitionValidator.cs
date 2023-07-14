using ScriptGeneratorSqlServer.Core.Models.Common;
using ScriptGeneratorSqlServer.Core.Models.Table;
using ScriptGeneratorSqlServer.Core.Models.Table.Constraints;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGeneratorSqlServer.Core.Validation
{
    public class TableDefinitionValidator
    {
        public void ValidateAndThrowErrors(TableDefinition tableDefinition)
        {
            if (string.IsNullOrWhiteSpace(tableDefinition.TableName))
            {
                throw new ScriptGeneratorException("Must provide a name for the table");
            }

            if (string.IsNullOrWhiteSpace(tableDefinition.SchemaName))
            {
                throw new ScriptGeneratorException("Must provide a schema for the table");
            }

            var columns = tableDefinition.Columns?.Select(s => new Column
            {
                Bytes = s.Bytes,
                DataType = s.DataType,
                Identity = s.Identity,
                IsNullable = s.IsNullable,
                Name = s.Name?.ToLower() ?? string.Empty,
                Scale = s.Scale,
                Precision = s.Precision
            }) ?? Enumerable.Empty<Column>();

            if (tableDefinition.UniqueConstraints != null && tableDefinition.UniqueConstraints.Any()) 
            {
                var uniqueConstraints = tableDefinition.UniqueConstraints.Select(s => new UniqueConstraint
                {
                    ColumnNames = s.ColumnNames?.Select(s => s?.ToLower() ?? string.Empty).ToList() ?? Enumerable.Empty<string>().ToList(),
                    IsPrimaryKey = s.IsPrimaryKey,
                    ConstraintName = s.ConstraintName?.ToLower() ?? string.Empty
                });

                if (uniqueConstraints.Count(c => c.IsPrimaryKey) > 1)
                {
                    throw new ScriptGeneratorException("Each table can only have one primary key");
                }

                if (uniqueConstraints.Any(a => string.IsNullOrWhiteSpace(a.ConstraintName)))
                {
                    throw new ScriptGeneratorException("Each unique constraint must have a name");
                }

                if (uniqueConstraints.Select(s => s.ConstraintName).Distinct().Count() != uniqueConstraints.Count())
                {
                    throw new ScriptGeneratorException("Each unique constraint must have a unique name");
                }

                if (uniqueConstraints.Any(a => a.ColumnNames == null || !a.ColumnNames.Any()))
                {
                    throw new ScriptGeneratorException("Each unique constraint must include at least one column");
                }

                if (uniqueConstraints.Select(s => string.Join(',', s.ColumnNames.OrderBy(ob => ob).ToList())).Distinct().Count() != uniqueConstraints.Count())
                {
                    throw new ScriptGeneratorException("Cannot have mulitple unique constraints with the exact same list of columns");
                }

                if (columns.Any()
                    || uniqueConstraints.SelectMany(sm => sm.ColumnNames).Distinct().Any(a => !columns.Select(s => s.Name).Contains(a)))
                {
                    throw new ScriptGeneratorException("Each column in the unique constraint must be defined in the table");
                }
            }
            
            if (tableDefinition.ForeignKeys != null && tableDefinition.ForeignKeys.Any())
            {
                var foreignKeys = tableDefinition.ForeignKeys.Select(s => new ForeignKey
                {
                    ColumnName = s.ColumnName?.ToLower() ?? string.Empty,
                    ConstraintName = s.ConstraintName?.ToLower() ?? string.Empty,
                    ReferenceTableName = s.ReferenceTableName?.ToLower() ?? string.Empty,
                    ReferenceTableColumnName = s.ReferenceTableColumnName?.ToLower() ?? string.Empty,
                });

                if (foreignKeys.Any(a => string.IsNullOrWhiteSpace(a.ConstraintName)))
                {
                    throw new ScriptGeneratorException("Each foreign key must have a name");
                }

                if (foreignKeys.Select(s => s.ConstraintName).Distinct().Count() != foreignKeys.Count())
                {
                    throw new ScriptGeneratorException("Each foreign key must have a unique name");
                }

                if (foreignKeys.Any(a => string.IsNullOrWhiteSpace(a.ColumnName)))
                {
                    throw new ScriptGeneratorException("Each foreign key must include a column name");
                }

                if (foreignKeys.Select(s => s.ColumnName).Distinct().Count() != foreignKeys.Count())
                {
                    throw new ScriptGeneratorException("Cannot use the same column name in multiple foreign keys");
                }

                if (!columns.Any()
                    || foreignKeys.Select(s => s.ColumnName).Distinct().Any(a => !columns.Select(s => s.Name).Contains(a)))
                {
                    throw new ScriptGeneratorException("Each foreign key column must be defined in the table");
                }

                if (foreignKeys.Any(a => string.IsNullOrWhiteSpace(a.ReferenceTableName)))
                {
                    throw new ScriptGeneratorException("Each foreign key must include a reference table name");
                }

                if (foreignKeys.Any(a => string.IsNullOrWhiteSpace(a.ReferenceTableColumnName)))
                {
                    throw new ScriptGeneratorException("Each foreign key must include a reference table column name");
                }
            }
        }
    }
}
