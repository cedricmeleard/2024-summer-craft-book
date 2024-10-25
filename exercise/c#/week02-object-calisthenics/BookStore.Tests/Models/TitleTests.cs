using System;
using BookStore.Models;
using Xunit;

namespace BookStore.Tests.Models;

public class TitleTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Title_ShouldHaveAValue(string title)
    {
        Assert.Throws<ArgumentException>(() => new BookTitle(title));
    }
    
    [Theory]
    [InlineData("Neuromancer", "Hyperion", false)]
    [InlineData("Neuromancer", "Neuromancer", true)]
    
    public void Title_ShouldEquals(string title, string compareTo, bool expected)
    {
        Assert.Equal(expected, new BookTitle(title).Equals(new BookTitle(compareTo)));
    }
}