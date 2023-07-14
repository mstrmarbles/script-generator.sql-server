using FluentAssertions;
using ScriptGeneratorSqlServer.Core.Models.Common;
using ScriptGeneratorSqlServer.Core.Models.Table;
using ScriptGeneratorSqlServer.Core.Validation;

namespace ScriptGeneratorSqlServer.Core.Tests.Validation
{
    [TestClass]
    public class ColumnsValidatorTests
    {
        [TestMethod]
        public void ShouldThrowErrorWhenColumnListIsNull()
        {
            var validator = new ColumnsValidator();

            try
            {
                validator.ValidateAndThrowErrors(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                ex.Should().BeOfType<ArgumentNullException>();
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenColumnListIsEmpty()
        {
            var validator = new ColumnsValidator();

            try
            {
                validator.ValidateAndThrowErrors(new List<Column>());
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                ex.Should().BeOfType<ArgumentNullException>();
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenMultipleColumnsHaveIdentities()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Int,
                    IsNullable = true,
                    Name = "Column1",
                    Identity = new Identity
                    {
                        Seed = 1,
                        Incrememt = 1
                    }
                },
                new Column
                {
                    DataType = DataType.Int,
                    IsNullable = true,
                    Name = "Column2",
                    Identity = new Identity
                    {
                        Seed = 1,
                        Incrememt = 1
                    }
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnMultipleIdentities);
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenAtLeastOneColumnHasEmptyName()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Int,
                    IsNullable = true,
                    Name = string.Empty,
                },
                new Column
                {
                    DataType = DataType.Int,
                    IsNullable = true,
                    Name = "Column2",
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnMissingName);
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenTwoColumnsHaveTheSameName()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Int,
                    IsNullable = true,
                    Name = "Column2",
                },
                new Column
                {
                    DataType = DataType.Int,
                    IsNullable = true,
                    Name = "Column2",
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnDuplicateName);
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenDecimalColumnDoesNotHavePrecisionDefined()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Decimal,
                    IsNullable = true,
                    Name = "Column2",
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnDecimalPrecisionInvalid);
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenDecimalColumnHasPrecisionLessThan1()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Decimal,
                    IsNullable = true,
                    Name = "Column2",
                    Precision = 0
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnDecimalPrecisionInvalid);
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenDecimalColumnHasPrecisionGreaterThan38()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Decimal,
                    IsNullable = true,
                    Name = "Column2",
                    Precision = 40
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnDecimalPrecisionInvalid);
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenDecimalColumnDoesNotHaveScaleDefined()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Decimal,
                    IsNullable = true,
                    Name = "Column2",
                    Precision = 10,
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnDecimalScaleInvalid);
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenDecimalColumnHasScaleLessThan0()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Decimal,
                    IsNullable = true,
                    Name = "Column2",
                    Precision = 10,
                    Scale = -1
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnDecimalScaleInvalid);
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenDecimalColumnHasScaleGreaterThan38()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Decimal,
                    IsNullable = true,
                    Name = "Column2",
                    Precision = 10,
                    Scale = 40
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnDecimalScaleInvalid);
            }
        }

        [TestMethod]
        public void ShouldNotThrowErrorWhenDecimalColumnHasValidPrecisionAndScale()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Decimal,
                    IsNullable = true,
                    Name = "Column2",
                    Precision = 10,
                    Scale = 5
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
            }
            catch (ScriptGeneratorException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenCharColumnDoesNotHaveBytesDefined()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Char,
                    IsNullable = true,
                    Name = "Column2",
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnCharBytesInvalid);
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenCharColumnHasBytesLessThan1()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Char,
                    IsNullable = true,
                    Name = "Column2",
                    Bytes = 0
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnCharBytesInvalid);
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenCharColumnHasBytesGreaterThan8000()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Char,
                    IsNullable = true,
                    Name = "Column2",
                    Bytes = 9000
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnCharBytesInvalid);
            }
        }

        [TestMethod]
        public void ShouldNotThrowErrorWhenCharColumnHasValidBytes()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.Char,
                    IsNullable = true,
                    Name = "Column2",
                    Bytes = 100
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
            }
            catch (ScriptGeneratorException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenVarCharColumnHasBytesLessThan1()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.VarChar,
                    IsNullable = true,
                    Name = "Column2",
                    Bytes = 0
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnVarCharBytesInvalid);
            }
        }

        [TestMethod]
        public void ShouldThrowErrorWhenVarCharColumnHasBytesGreaterThan8000()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.VarChar,
                    IsNullable = true,
                    Name = "Column2",
                    Bytes = 9000
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
                Assert.Fail();
            }
            catch (ScriptGeneratorException ex)
            {
                ex.Message.Should().Be(ErrorMessages.ColumnVarCharBytesInvalid);
            }
        }

        [TestMethod]
        public void ShouldNotThrowErrorWhenVarCharColumnHasBytesWith1To8000()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.VarChar,
                    IsNullable = true,
                    Name = "Column2",
                    Bytes = 2000
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
            }
            catch (ScriptGeneratorException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ShouldNotThrowErrorWhenVarCharColumnDoesNotHaveBytesDefined()
        {
            var validator = new ColumnsValidator();
            var columns = new List<Column>
            {
                new Column
                {
                    DataType = DataType.VarChar,
                    IsNullable = true,
                    Name = "Column2"
                }
            };

            try
            {
                validator.ValidateAndThrowErrors(columns);
            }
            catch (ScriptGeneratorException)
            {
                Assert.Fail();
            }
        }
    }
}
