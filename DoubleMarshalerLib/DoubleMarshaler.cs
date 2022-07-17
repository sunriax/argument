using RaGae.ArgumentLib.MarshalerLib;
using System;

namespace RaGae.ArgumentLib.DoubleMarshalerLib
{
    public class DoubleMarshaler : Marshaler
    {
        public override string Schema => "##";

        public override void Set(Iterator<string> currentArgument)
        {
            string parameter = null;

            try
            {
                parameter = currentArgument.Next();
                base.Value = double.Parse(parameter);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new DoubleMarshalerException(ErrorCode.MISSING);
            }
            catch (FormatException)
            {
                throw new DoubleMarshalerException(ErrorCode.INVALID, parameter);
            }
        }

        private class DoubleMarshalerException : BaseArgumentException
        {
            public DoubleMarshalerException(ErrorCode errorCode) : base(errorCode) { }

            public DoubleMarshalerException(ErrorCode errorCode, string message) : base(errorCode, message) { }

            public override string ErrorMessage()
            {
                switch (base.ErrorCode)
                {
                    case ErrorCode.MISSING:
                        return $"Could not find double parameter for -{base.ErrorArgumentId}";
                    case ErrorCode.INVALID:
                        return $"Argument -{base.ErrorArgumentId} expects an double but was '{base.ErrorParameter}'";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
