using RaGae.ArgumentLib.MarshalerLib;
using RaGae.ArgumentLib.StringMarshalerLib;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;

namespace StringMarshalerLibTest
{
    public class StringMarshalerTest
    {
        private readonly string testSchema = "*";
        private readonly string testArgument = "test";
        private List<string> testData = new List<string>()
            {
                "Testing text",
                "-nextcommand"
            };

        [Fact]
        public void CreateReferenceAndSetIteratorToFirst_PassingTest()
        {
            StringMarshaler m = new StringMarshaler();
            Iterator<string> i = new Iterator<string>(testData);

            m.Set(i);

            Assert.Equal(testData[0], m.Value);
            Assert.Equal(testData[1], i.Current);
        }

        [Fact]
        public void CreateReferenceAndSetNull_FailingTest()
        {
            StringMarshaler m = new StringMarshaler();
            Exception ex = Assert.Throws<NullReferenceException>(() => m.Set(null));

            Assert.Equal("Object reference not set to an instance of an object.", ex.Message);
        }


        [Fact]
        public void CreateReferenceAndSetIteratorToLast_FailingTest()
        {
            StringMarshaler m = new StringMarshaler();
            Iterator<string> i = new Iterator<string>(testData);

            for (int j = 0; j < this.testData.Count; j++)
            {
                i.Next();
            }

            Exception ex = Record.Exception(() => m.Set(i));
            BaseArgumentException exBase = ex as BaseArgumentException ?? throw new XunitException("Wrong exception type!");

            exBase.ErrorArgumentId = this.testArgument;

            Assert.Equal(ErrorCode.MISSING, exBase.ErrorCode);
            Assert.Equal(this.testArgument, exBase.ErrorArgumentId);
            Assert.Equal($"Could not find string parameter for -{this.testArgument}", exBase.ErrorMessage());
        }

        [Fact]
        public void CreateRefernceAndTestSchema_PassingTest()
        {
            StringMarshaler m = new StringMarshaler();
            Assert.Equal(testSchema, m.Schema);
        }
    }
}
