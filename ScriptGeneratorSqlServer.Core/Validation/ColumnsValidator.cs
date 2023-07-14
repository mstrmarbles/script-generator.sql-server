using ScriptGeneratorSqlServer.Core.Models.Common;
using ScriptGeneratorSqlServer.Core.Models.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGeneratorSqlServer.Core.Validation
{
    public class ColumnsValidator
    {
        public void ValidateAndThrowErrors(IReadOnlyCollection<Column> columns)
        {
            if (columns == null || !columns.Any())
            {
                throw new ArgumentNullException(nameof(columns));
            }

            if (columns.Where(w => w.Identity != null).Count() > 1)
            {
                throw new ScriptGeneratorException(ErrorMessages.ColumnMultipleIdentities);
            }

            if(columns.Any(a => string.IsNullOrWhiteSpace(a.Name)))
            {
                throw new ScriptGeneratorException(ErrorMessages.ColumnMissingName);
            }

            if(columns.Select(s => s.Name.ToLower()).Distinct().Count() != columns.Count())
            {
                throw new ScriptGeneratorException(ErrorMessages.ColumnDuplicateName);
            }

            columns.ToList().ForEach(ValidateDataTypeAndThrowError);
        }

        private void ValidateDataTypeAndThrowError(Column column)
        {
            if (column.DataType == DataType.Decimal)
            {
                if (!column.Precision.HasValue || column.Precision.Value < 1 || column.Precision.Value > 38)
                {
                    throw new ScriptGeneratorException(ErrorMessages.ColumnDecimalPrecisionInvalid);
                }

                if (!column.Scale.HasValue || column.Scale.Value < 0 || column.Scale.Value > 38)
                {
                    throw new ScriptGeneratorException(ErrorMessages.ColumnDecimalScaleInvalid);
                }
            }

            if (column.DataType == DataType.Char)
            {
                if (!column.Bytes.HasValue || column.Bytes.Value < 1 || column.Bytes.Value > 8000)
                {
                    throw new ScriptGeneratorException(ErrorMessages.ColumnCharBytesInvalid);
                }
            }

            if (column.DataType == DataType.VarChar)
            {
                if (column.Bytes.HasValue && (column.Bytes.Value < 1 || column.Bytes.Value > 8000))
                {
                    throw new ScriptGeneratorException(ErrorMessages.ColumnVarCharBytesInvalid);
                }
            }
        }
    }
}
