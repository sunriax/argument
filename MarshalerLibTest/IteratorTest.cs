using RaGae.ArgumentLib.MarshalerLib;
using System;
using System.Collections.Generic;
using Xunit;

namespace MarshalerLibTest
{
    public class IteratorTest
    {
        List<string> testList = new List<string>()
        {
            "Das",
            "Ist",
            "Ein",
            "Test",
        };


        [Fact]
        public void IteratorCreateReference_PassingTest()
        {
            Iterator<string> iterator = new Iterator<string>(testList);

            Assert.NotNull(iterator);
        }

        [Fact]
        public void IteratorNullReference_FailingTest()
        {
            Assert.Throws<NullReferenceException>(() => new Iterator<string>(null));
        }

        [Fact]
        public void IteratorCurrentValue_PassingTest()
        {
            Iterator<string> iterator = new Iterator<string>(testList);

            Assert.Equal(testList[0], iterator.Current);
        }

        [Fact]
        public void IteratorCurrentValue_FailingTest()
        {
            Iterator<string> iterator = new Iterator<string>(testList);

            Assert.NotEqual(testList[1], iterator.Current);
        }

        [Fact]
        public void IteratorHasNextValue_PassingTest()
        {
            Iterator<string> iterator = new Iterator<string>(testList);

            Assert.True(iterator.HasNext);
        }

        [Fact]
        public void IteratorHasNextValue_FailingTest()
        {
            Iterator<string> iterator = new Iterator<string>(testList);

            for (int i = 0; i < testList.Count; i++)
            {
                iterator.Next();
            }

            Assert.False(iterator.HasNext);
        }

        [Fact]
        public void IteratorNextValue_PassingTest()
        {
            Iterator<string> iterator = new Iterator<string>(testList);

            iterator.Next();
            Assert.Equal(testList[1], iterator.Current);
        }


        [Fact]
        public void IteratorNextValue_FailingTest()
        {
            Iterator<string> iterator = new Iterator<string>(testList);

            for (int i = 0; i < testList.Count; i++)
            {
                iterator.Next();
            }

            Assert.Throws<ArgumentOutOfRangeException>(() => iterator.Next());
        }

        [Fact]
        public void IteratorPreviousValue_PassingTest()
        {
            Iterator<string> iterator = new Iterator<string>(testList);

            for (int i = 0; i < testList.Count; i++)
            {
                iterator.Next();
            }

            iterator.Previous();

            Assert.Equal(testList[(testList.Count - 1)], iterator.Current);
        }

        [Fact]
        public void IteratorPreviousValue_FailingTest()
        {
            Iterator<string> iterator = new Iterator<string>(testList);

            Assert.Throws<ArgumentOutOfRangeException>(() => iterator.Previous());
        }
    }
}
