using RaGae.ArgumentLib.MarshalerLib;
using System;

namespace RaGae.ArgumentLib.BooleanMarshalerLib
{
    public class BooleanMarshaler : Marshaler
    {
        public override string Schema => string.Empty;

        public override void Set(Iterator<string> currentArgument)
        {
            base.Value = true;
        }
    }
}
