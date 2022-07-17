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
            REFLECTION,
            ERROR,
            GLOBAL,
            TEST
        }

        public abstract class BaseArgumentException : BaseException<ErrorCode>
        {
            public BaseArgumentException(ErrorCode errorCode)
            {
                this.ErrorCode = errorCode;
            }

            public BaseArgumentException(ErrorCode errorCode, string errorParameter)
            {
                this.ErrorCode = errorCode;
                this.ErrorParameter = errorParameter;
            }
            public BaseArgumentException(ErrorCode errorCode, string errorArgumentId, string errorParameter)
            {
                this.ErrorCode = errorCode;
                this.ErrorArgumentId = errorArgumentId;
                this.ErrorParameter = errorParameter;
            }

            public string ErrorArgumentId { get; set; }
            public string ErrorParameter { get; set; }

            public override string Message => this.ErrorMessage();
        }
    }
}
