using RaGae.ArgumentLib.MarshalerLib;
using System;

namespace RaGae.ArgumentLib.IntegerMarshalerLib
{
    public class IntegerMarshaler : Marshaler
    {
        public override string Schema => "#";

        public override void Set(Iterator<string> currentArgument)
        {
            string parameter = null;

            try
            {
                parameter = currentArgument.Next();
                base.Value = int.Parse(parameter);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new IntegerMarshalerException(ErrorCode.MISSING);
            }
            catch (FormatException)
            {
                throw new IntegerMarshalerException(ErrorCode.INVALID, parameter);
            }
        }

        private class IntegerMarshalerException : BaseArgumentException
        {
            public IntegerMarshalerException(ErrorCode errorCode) : base(errorCode) { }

            public IntegerMarshalerException(ErrorCode errorCode, string message) : base(errorCode, message) { }

            public override string ErrorMessage()
            {
                switch (base.ErrorCode)
                {
                    case ErrorCode.MISSING:
                        return $"Could not find integer parameter for -{base.ErrorArgumentId}";
                    case ErrorCode.INVALID:
                        return $"Argument -{base.ErrorArgumentId} expects an integer but was '{base.ErrorParameter}'";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
