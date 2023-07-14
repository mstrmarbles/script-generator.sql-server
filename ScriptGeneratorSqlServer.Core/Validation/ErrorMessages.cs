using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGeneratorSqlServer.Core.Validation
{
    public static class ErrorMessages
    {
        public const string ColumnMultipleIdentities = "Only one identity column is allowed per table";
        public const string ColumnMissingName = "Each column must have a name defined";
        public const string ColumnDuplicateName = "Each column must have a unique name";
        public const string ColumnDecimalPrecisionInvalid = "A column of type DECIMAL must specify a PRECISION between 1 and 38";
        public const string ColumnDecimalScaleInvalid = "A column of type DECIMAL must specify a SCALE between 0 and 38";
        public const string ColumnCharBytesInvalid = "A column of type CHAR must specify a number of BYTES between 1 and 8000";
        public const string ColumnVarCharBytesInvalid = "A column of type VARCHAR must specify a number of BYTES between 1 and 8000, or leave the number of BYTES empty to default to the MAX number";
    }
}
