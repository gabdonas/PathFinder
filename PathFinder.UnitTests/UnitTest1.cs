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
        [InlineData(new[] { 1, 2, 0, 1, 0, 2, 0}, false)]
        [InlineData(new[] { 1, 2, 0, -1, 0, 2, 0}, false)]
        [InlineData(new[] {1, 2, 1, -1, 0, 2, 0}, false)]
        public void FindsWhetherTraversablePathExists(int[] data, bool expected)
        {
            Assert.True(expected == sut.Find(data));
        }
        
        [Theory]
        [InlineData(new[] { 1, 5, 0, 8, 0, 3, -4, 2, -1, 0 }, true)]
        public void FindsWhetherTraversablePathExistss(int[] data, bool expected)
        {
            Assert.True(expected == sut.Find(data));
        }
        //[Theory]
        //[InlineData(new[] {-1 })]
        //[InlineData(new[] {0})]
        //public void ShouldThrowException(int[] data)
        //{
        //    Assert.Throws<Exception>(()=>sut.Find(data));
        //}

        //[Theory]
        //[InlineData(new[] {1, -1 })]
       
        //public void ShouldThrowExceptionWhenLoop(int[] data)
        //{
        //    Assert.Throws<Exception>(()=>sut.Find(data));
        //}
    }
}
