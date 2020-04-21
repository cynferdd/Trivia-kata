using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Trivia.Test
{
    public class CircularIteratorTest
    {
        [Fact]
        public void ShouldStartOnFirstElement_WhenNotMoving()
        {
            var iterator = new CircularIterator<string>("toto", "tata", "titi");
            Assert.Equal("toto", iterator.Current);

        }

        [Fact]
        public void ShouldGoToSecondElement_WhenMovingByOneUnderTheLimit()
        {
            var iterator = new CircularIterator<string>("toto", "tata", "titi");
            iterator.Move(1);
            Assert.Equal("tata", iterator.Current);

        }

        [Fact]
        public void ShouldGoBackToFirstElement_WhenMovingByAnAmountEqualToTheLimit()
        {
            var iterator = new CircularIterator<string>("toto", "tata", "titi");
            iterator.Move(iterator.Count);
            Assert.Equal("toto", iterator.Current);

        }
    }
}
