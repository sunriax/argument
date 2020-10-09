using RaGae.ArgumentLib.BooleanMarshalerLib;
using RaGae.ArgumentLib.MarshalerLib;
using System;
using System.Collections.Generic;
using Xunit;

namespace BooleanMarshalerLibTest
{
    public class BooleanMarshalerTest
    {
        private readonly string testSchema = string.Empty;
        private List<string> testData = new List<string>()
            {
                "-a",
                "-b"
            };

        [Fact]
        public void CreateReferenceAndNoSet_PassingTest()
        {
            BooleanMarshaler m = new BooleanMarshaler();

            Assert.Null(m.Value);
        }

        [Fact]
        public void CreateReferenceAndSetNull_PassingTest()
        {
            BooleanMarshaler m = new BooleanMarshaler();

            m.Set(null);

            Assert.True((bool)m.Value);
        }

        [Fact]
        public void CreateReferenceAndSetTrue_PassingTest()
        {
            BooleanMarshaler m = new BooleanMarshaler();
            Iterator<string> i = new Iterator<string>(testData);

            m.Set(i);

            Assert.True((bool)m.Value);
            Assert.Equal(testData[0], i.Current);
        }

        [Fact]
        public void CreateReferenceAndTestSchema_PassingTest()
        {
            BooleanMarshaler m = new BooleanMarshaler();
            Assert.Equal(testSchema, m.Schema);
        }
    }
}
