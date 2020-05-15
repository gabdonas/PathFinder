using System;
using Xunit;

namespace PathFinder.UnitTests
{
    public class PathFinderTests
    {
        private IPathFinder sut;

        public PathFinderTests()
        {
            sut = new PathFinder();
        }

        [Theory]
        [InlineData(new[] { 1, 2, 0, 3, 0, 2, 0 }, true)]
        [InlineData(new[] { 1, 2, 0, 1, 0, 2, 0 }, false)]
        [InlineData(new[] { 1, 2, 0, -1, 0, 2, 0 }, false)]
        [InlineData(new[] { 1, 2, 1, -1, 0, 2, 0 }, false)]
        public void FindsWhetherTraversablePathExists(int[] data, bool expected)
        {
            Assert.Equal(expected, sut.Find(data).IsTraversable);
        }


        [Theory]
        [InlineData(new[] { 1, 2, 0, 2, 0, 3, 0 }, true, new[] { 0, 1, 3, 5, 6 })]
        [InlineData(new[] { 1, 2, 0, 3, 0, 3, 0, 0 }, true, new[] { 0, 1, 3, 5, 7 })]
        [InlineData(new[] { 1, 2, 0, 3, 0, 3, 0, 0 ,0}, true, new[] { 0, 1, 3, 5, 8 })]
        [InlineData(new[] { 1, 2, 0, 2, 0, 3, 3, 3 }, true, new[] { 0, 1, 3, 5, 7 })]
        [InlineData(new[] { 1, 2, 0, 2, 0, 3, 0, -1 }, true, new[] { 0, 1, 3, 5, 7 })]
        [InlineData(new[] { 1, 2, 0, 2, 0, 3, 0, -1, -1 }, true, new[] { 0, 1, 3, 5, 8 })]
        [InlineData(new[] { 9, 2, 0, 2, 0, 3, 0, -1, -1, -1 }, true, new[] { 0, 9 })]
        [InlineData(new[] { 2, 5, 10, 0, 0, 0, 5, 0, 0, 0, 0, 5, 2, 1, 1, 1, 4, 0, 0, 0, 0 }, true, new[] { 0, 2, 11, 16, 20 })]
        [InlineData(new[] { 2, 5, 10, 0, 0, 0, 5, 0, 0, 0, 0, 5, 2, 1, 1, 1, 5, 0, 0, 0, 0 }, true, new[] { 0, 2, 11, 16, 20 })]
        [InlineData(new[] { 2, 5, 10, 0, 0, 0, 5, 0, 0, 0, 0, 5, 2, 1, 1, 6, 2, 0, 2, 0, 0 }, true, new[] { 0, 2, 11, 15, 20 })]
        [InlineData(new[] { 2, 5, -10, 0, 0, 0, 5, 0, 0, 0, 0, 5, 2, 1, 1, 1, 4, 0, 0, 0, 0 }, true, new[] { 0, 1, 6, 11, 16, 20 })]
        [InlineData(new[] { 2, 5, -10, 0, 0, 0, 5, 0, 0, 0, 0, 5, 2, 1, 1, 1, 5, 0, 0, -1, -10 }, true, new[] { 0, 1, 6, 11, 16, 20 })]
        [InlineData(new[] { 2, 5, 10, 0, 0, 0, 5, 0, 0, 0, 0, 5, 2, 1, 1, 6, 2, 0, 2, -1, -1 }, true, new[] { 0, 2, 11, 15, 20 })]
        public void FindsAndReturnsShortestPath(int[] data, bool expectedResult, int[] expectedPath)
        {
            var result = sut.Find(data);
            Assert.Equal(expectedResult, result.IsTraversable);
            Assert.Equal(expectedPath, result.Indices);
        }

        [Theory]
        [InlineData(new[] { 1, 2, 2, -1, 0, 3, 0 }, false, null)]
        [InlineData(new[] { 4, -1, -1, -1, -1, -1 }, false, null)]
        [InlineData(new[] { 3, -1, 1, -1, 1, -1 }, false, null)]
        public void DoesNotTraverseWhenEndNotReachable(int[] data, bool expectedResult, int[] expectedPath)
        {
            var result = sut.Find(data);
            Assert.Equal(expectedResult, result.IsTraversable);
            Assert.Equal(expectedPath, result.Indices);
        }

        [Theory]
        [InlineData(new[] { 5, -10, -100, -1, -1, -1 }, true, new[] { 0, 5 })]
        [InlineData(new[] { -1, -10, -100, -1, -1, -1 }, false, null)]
        [InlineData(new[] { 0, -10, -100, -1, -1, -1 }, false, null)]
        public void CorrectlyHandlesArraysWithNegativeStartOrEnd(int[] data, bool expectedResult, int[] expectedPath)
        {
            var result = sut.Find(data);
            Assert.Equal(expectedResult, result.IsTraversable);
            Assert.Equal(expectedPath, result.Indices);
        }

        [Theory]
        [InlineData(new[] { 1, -1 }, true, new[] { 0, 1 })]
        [InlineData(new[] { 5, -1 }, true, new[] { 0, 1 })]
        [InlineData(new[] { -5, -1 }, false, null)]
        [InlineData(new[] { int.MaxValue, int.MinValue }, true, new[] { 0, 1 })]
        [InlineData(new[] { int.MinValue, int.MaxValue }, false, null)]
        public void CorrectlyHandles2ItemArrays(int[] data, bool expectedResult, int[] expectedPath)
        {
            var result = sut.Find(data);
            Assert.Equal(expectedResult, result.IsTraversable);
            Assert.Equal(expectedPath, result.Indices);
        }

        [Theory]
        [InlineData(new[] { -1 })]
        [InlineData(new[] { 5 })]
        [InlineData(new[] { int.MaxValue })]
        [InlineData(new int[] { })]
        [InlineData(null)]
        public void ShouldThrowExceptionFor1ItemOrEmptyArray(int[] data)
        {
            Assert.ThrowsAny<Exception>(() => sut.Find(data));
        }


    }
}
