using RaGae.ExceptionLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaGae.ArgumentLib
{
    namespace MarshalerLib
    {
        public enum ErrorCode
        {
            OK,
            EMPTY,
            UNEXPECTED_ARGUMENT,
            MISSING,
            INVALID,
            INVALID_PARAMETER,
            INVALID_ARGUMENT_NAME,
            REFLECTION,
            GLOBAL,
            INVALID_SCHEMA,
            TEST
        }

        public abstract class BaseArgumentException : BaseException<ErrorCode>
        {
            public BaseArgumentException() { }

            public BaseArgumentException(string message) : base(message) { }

            public BaseArgumentException(ErrorCode errorCode)
            {
                this.ErrorCode = errorCode;
            }

            public BaseArgumentException(ErrorCode errorCode, string errorParameter)
            {
                this.ErrorCode = errorCode;
                this.ErrorParameter = errorParameter;
            }

            public string ErrorArgumentId { get; set; }
            public string ErrorParameter { get; set; }
        }
    }
}
