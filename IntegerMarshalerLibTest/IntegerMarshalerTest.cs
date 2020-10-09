using RaGae.ArgumentLib.IntegerMarshalerLib;
using RaGae.ArgumentLib.MarshalerLib;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;

namespace IntegerMarshalerLibTest
{
    public class IntegerMarshalerTest
    {
        private readonly string testSchema = "#";
        private readonly string testArgument = "argument";
        private List<string> testData = new List<string>()
            {
                "1234",
                "-nextcommand",
                "NotANumber",
            };

        [Fact]
        public void CreateReferenceAndSetNull_FailingTest()
        {
            IntegerMarshaler m = new IntegerMarshaler();

            Assert.Throws<NullReferenceException>(() => m.Set(null));
        }

        [Fact]
        public void CreateReferenceAndSetIterator_PassingTest()
        {
            IntegerMarshaler m = new IntegerMarshaler();
            Iterator<string> i = new Iterator<string>(testData);

            m.Set(i);

            Assert.Equal(testData[0], m.Value.ToString());
            Assert.Equal(testData[1], i.Current.ToString());
        }

        [Fact]
        public void CreateReferenceAndSetIteratorToOutOfRange_FailingTest()
        {
            IntegerMarshaler m = new IntegerMarshaler();
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
            Assert.Equal($"Could not find integer parameter for -{this.testArgument}", exBase.ErrorMessage());
        }

        [Fact]
        public void CreateReferenceAndSetIteratorWithWrongFormat_FailingTest()
        {
            IntegerMarshaler m = new IntegerMarshaler();
            Iterator<string> i = new Iterator<string>(testData);

            for (int j = 0; j < (this.testData.Count - 1); j++)
            {
                i.Next();
            }

            Exception ex = Record.Exception(() => m.Set(i));
            BaseArgumentException exBase = ex as BaseArgumentException ?? throw new XunitException("Wrong exception type!");

            exBase.ErrorArgumentId = this.testArgument;

            Assert.Equal(ErrorCode.INVALID, exBase.ErrorCode);
            Assert.Equal(this.testArgument, exBase.ErrorArgumentId);
            Assert.Equal(this.testData[(this.testData.Count - 1)], exBase.ErrorParameter);

            Assert.Equal($"Argument -{this.testArgument} expects an integer but was '{this.testData[(this.testData.Count - 1)]}'", exBase.ErrorMessage());
        }

        [Fact]
        public void CreateReferenceAndTestSchema_PassingTest()
        {
            IntegerMarshaler m = new IntegerMarshaler();
            Assert.Equal(testSchema, m.Schema);
        }
    }
}
