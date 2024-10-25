using System;
using BookStore.Models;
using Xunit;

namespace BookStore.Tests.Models;

public class CopiesCountTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void CopiesCount_ShouldBeGreaterThanZero(int count)
    {
        Assert.Throws<ArgumentException>(() => new CopiesCount(count));
    }
    
    [Theory]
    [InlineData(1, 3, false)]
    [InlineData(3, 3, true)]
    public void CopiesCount_ShouldEquals(int coppies, int compareTo, bool expected)
    {
        Assert.Equal(expected, new CopiesCount(coppies).Equals(new CopiesCount(compareTo)));
    }
}