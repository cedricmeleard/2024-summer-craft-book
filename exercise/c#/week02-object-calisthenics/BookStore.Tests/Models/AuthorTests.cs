using System;
using BookStore.Models;
using Xunit;

namespace BookStore.Tests.Models;

public class AuthorTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Author_ShouldHaveAValue(string author)
    {
        Assert.Throws<ArgumentException>(() => new Author(author));
    }
    
    [Theory]
    [InlineData("Wiliam Gibson", "Dan Simmons", false)]
    [InlineData("Wiliam Gibson", "Wiliam Gibson", true)]
    
    public void Author_ShouldEquals(string author, string compareTo, bool expected)
    {
        Assert.Equal(expected, new Author(author).Equals(new Author(compareTo)));
    }
}