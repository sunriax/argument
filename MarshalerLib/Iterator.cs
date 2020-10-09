using System;
using System.Collections.Generic;
using System.Text;

namespace RaGae.ArgumentLib
{
    namespace MarshalerLib
    {
        public class Iterator<T>
        {
            private readonly IList<T> list;
            private int index;

            public Iterator(IList<T> list)
            {
                if (list == null)
                    throw new NullReferenceException();

                this.list = list;
            }

            public T Current => this.list[index];
            public bool HasNext => this.index < this.list.Count;

            public T Next()
            {
                return this.list[this.index++];
            }

            public T Previous()
            {
                return this.list[--this.index];
            }
        }
    }
}
