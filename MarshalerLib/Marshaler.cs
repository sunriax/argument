using System;

namespace RaGae.ArgumentLib
{
    namespace MarshalerLib
    {
        public abstract class Marshaler
        {
            public object Value { get; protected set; }

            public abstract string Schema { get; }

            public abstract void Set(Iterator<string> currentArgument);
        }
    }
}
