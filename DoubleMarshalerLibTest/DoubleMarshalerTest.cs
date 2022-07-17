using RaGae.ArgumentLib.DoubleMarshalerLib;
using RaGae.ArgumentLib.MarshalerLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;
using Xunit.Sdk;

namespace DoubleMarshalerLibTest
{
    public class DoubleMarshalerTest
    {
        private readonly string testSchema = "##";
        private readonly string testArgument = "argument";
        private List<string> testData = new List<string>()
            {
                Convert.ToDouble("1234,5678", CultureInfo.CreateSpecificCulture("de-AT")).ToString(),
                "-nextcommand",
                "NotANumber"
            };

        [Fact]
        public void CreateReferenceAndSetIterator_PassingTest()
        {
            DoubleMarshaler m = new DoubleMarshaler();
            Iterator<string> i = new Iterator<string>(testData);

            m.Set(i);

            Assert.Equal(Convert.ToDouble(testData[0]).ToString(), m.Value.ToString());
            Assert.Equal(testData[1], i.Current.ToString());
        }

        [Fact]
        public void CreateReferenceAndSetNull_FailingTest()
        {
            DoubleMarshaler m = new DoubleMarshaler();

            Assert.Throws<NullReferenceException>(() => m.Set(null));
        }

        [Fact]
        public void CreateReferenceAndSetIteratorToOutOfRange_FailingTest()
        {
            DoubleMarshaler m = new DoubleMarshaler();
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
            Assert.Equal($"Could not find double parameter for -{this.testArgument}", exBase.ErrorMessage());
        }

        [Fact]
        public void CreateReferenceAndSetIteratorWithWrongFormat_FailingTest()
        {
            DoubleMarshaler m = new DoubleMarshaler();
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

            Assert.Equal($"Argument -{this.testArgument} expects an double but was '{this.testData[(this.testData.Count - 1)]}'", exBase.ErrorMessage());
        }

        [Fact]
        public void CreateReferenceAndTestSchema_PassingTest()
        {
            DoubleMarshaler m = new DoubleMarshaler();
            Assert.Equal(testSchema, m.Schema);
        }
    }
}
