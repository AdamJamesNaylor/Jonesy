namespace AJN.Jonesy.Common.UnitTests {
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class IEnumerableExtensions {
        private readonly List<int> _sut = new List<int> {1, 2, 3, 4, 5, 111, 222, 333, 444, 555};

        [Fact]
        public void TakeRandom_WithZero_ReturnsEmptyCollection() {
            var result = _sut.TakeRandom(0);

            Assert.NotNull(result);
            Assert.Equal(0, result.Count());
        }

        [Fact]
        public void TakeRandom_WithOne_ReturnsOneElement() {
            var result = _sut.TakeRandom(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void TakeRandom_WithMoreThanOne_ReturnsSameNumber() {
            var result = _sut.TakeRandom(5);

            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void TakeRandom_Consecutively_ReturnsRandomElements() {
            var result = _sut.TakeRandom(1);

            Assert.NotNull(result);
            var initial = result.First();
            var tries= 10000;
            while (tries-- > 0) {
                if (_sut.TakeRandom(1).First() != initial)
                    return;
            }
            Assert.True(false, "Consecutive calls to TakeRandom() returned the same element too many times.");
        }

        [Fact]
        public void TakeRandomExclusive_Never_ReturnsTheSameElement() {
            var result = _sut.TakeRandomExclusive(3);

            Assert.NotNull(result);

            Assert.NotEqual(result.First(), result.ElementAt(1));
            Assert.NotEqual(result.First(), result.ElementAt(2));

            Assert.NotEqual(result.Last(), result.ElementAt(1));
            Assert.NotEqual(result.Last(), result.First());
        }
    }
}