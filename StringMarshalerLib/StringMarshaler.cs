using RaGae.ArgumentLib.MarshalerLib;
using System;

namespace RaGae.ArgumentLib.StringMarshalerLib
{
    public class StringMarshaler : Marshaler
    {
        public override string Schema => "*";

        public override void Set(Iterator<string> currentArgument)
        {
            try
            {
                base.Value = currentArgument.Next();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new StringMarshalerException(ErrorCode.MISSING);
            }
        }

        private class StringMarshalerException : BaseArgumentException
        {
            public StringMarshalerException(ErrorCode errorCode) : base(errorCode) { }

            public override string ErrorMessage()
            {
                switch (base.ErrorCode)
                {
                    case ErrorCode.MISSING:
                        return $"Could not find string parameter for -{base.ErrorArgumentId}";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
