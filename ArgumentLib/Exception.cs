using RaGae.ArgumentLib.MarshalerLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaGae.ArgumentLib
{
    public class ArgumentException : BaseArgumentException
    {
        public ArgumentException(ErrorCode errorCode, string errorParameter) : base(errorCode, errorParameter) { }

        public ArgumentException(ErrorCode errorCode, string errorArgumentId, string errorParameter) : base(errorCode, errorArgumentId, errorParameter) { }

        public override string ErrorMessage()
        {
            switch (base.ErrorCode)
            {
                case ErrorCode.OK:
                    return "TILT: Should not be reached!";
                case ErrorCode.EMPTY:
                    return $"Config File: {base.ErrorParameter} contains no data!";
                case ErrorCode.INVALID:
                    return $"Config File: {base.ErrorParameter} contains invalid data!";
                case ErrorCode.UNEXPECTED_ARGUMENT:
                    return $"Argument -{base.ErrorArgumentId} unexpected";
                case ErrorCode.INVALID_PARAMETER:
                    return $"'{base.ErrorParameter}' is not a valid parameter";
                case ErrorCode.MISSING:
                    return $"Missing parameter: ('{base.ErrorArgumentId}')";
                case ErrorCode.REFLECTION:
                    return base.ErrorParameter;
                case ErrorCode.GLOBAL:
                    return $"There was an ERROR with '{base.ErrorParameter}'";
                default:
                    return string.Empty;
            }
        }
    }
}
