using RaGae.ArgumentLib.MarshalerLib;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ArgumentException = RaGae.ArgumentLib.ArgumentException;

namespace ArgumentLibTest
{
    public class ExceptionTest
    {
        private const string testArgument = "argument";
        private const string testParameter = "parameter";

        public static IEnumerable<object[]> GetExceptionType()
        {
            yield return new object[] {
                ErrorCode.OK,
                null,
                null,
                "TILT: Should not be reached!"
            };

            yield return new object[] {
                ErrorCode.EMPTY,
                null,
                testParameter,
                $"Config File: {testParameter} contains no data!"
            };

            yield return new object[] {
                ErrorCode.INVALID,
                null,
                testParameter,
                $"Config File: {testParameter} contains invalid data!"
            };

            yield return new object[] {
                ErrorCode.UNEXPECTED_ARGUMENT,
                testArgument,
                null,
                $"Argument -{testArgument} unexpected"
            };

            yield return new object[] {
                ErrorCode.MISSING,
                testArgument,
                null,
                $"Missing parameter: ('{testArgument}')"
            };

            yield return new object[] {
                ErrorCode.INVALID_PARAMETER,
                null,
                testParameter,
                $"'{testParameter}' is not a valid parameter"
            };

            yield return new object[] {
                ErrorCode.REFLECTION,
                null,
                testParameter,
                testParameter
            };

            yield return new object[] {
                ErrorCode.ERROR,
                testArgument,
                testParameter,
                string.Empty
            };

            yield return new object[] {
                ErrorCode.GLOBAL,
                null,
                testParameter,
                $"There was an ERROR with '{testParameter}'"
            };

            yield return new object[] {
                ErrorCode.TEST,
                testArgument,
                testParameter,
                string.Empty
            };
        }

        [Theory]
        [MemberData(nameof(GetExceptionType))]
        public void CreateExceptionWithErrorCodeAndParameter_PassingTest(ErrorCode code, string argument, string parameter, string message)
        {
            BaseArgumentException ex = new ArgumentException(code, parameter);
            ex.ErrorArgumentId = argument;

            Assert.Equal(code, ex.ErrorCode);

            if (argument == null)
                Assert.Null(ex.ErrorArgumentId);
            else
                Assert.Equal(argument, ex.ErrorArgumentId);

            if (parameter == null)
                Assert.Null(ex.ErrorParameter);
            else
                Assert.Equal(parameter, ex.ErrorParameter);

            Assert.Equal(message, ex.Message);
            Assert.Equal(message, ex.ErrorMessage());
        }

        [Theory]
        [MemberData(nameof(GetExceptionType))]
        public void CreateExceptionWithErrorCodeAndParameterAndArgument_PassingTest(ErrorCode code, string argument, string parameter, string message)
        {
            BaseArgumentException ex = new ArgumentException(code, argument, parameter);

            Assert.Equal(code, ex.ErrorCode);

            if (argument == null)
                Assert.Null(ex.ErrorArgumentId);
            else
                Assert.Equal(argument, ex.ErrorArgumentId);

            if (parameter == null)
                Assert.Null(ex.ErrorParameter);
            else
                Assert.Equal(parameter, ex.ErrorParameter);

            Assert.Equal(message, ex.Message);
            Assert.Equal(message, ex.ErrorMessage());
        }
    }
}
