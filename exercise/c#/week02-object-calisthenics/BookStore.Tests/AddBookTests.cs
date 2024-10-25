using System;
using System.Linq;
using BookStore.Models;
using BookStore.Tests.TestHelpers;
using Xunit;

namespace BookStore.Tests;

public class AddBookTests
{
    [Fact]
    public void AddBook_WhenNotExists_ShouldAddNewBook()
    {
        var bookRepository = new FakeBookRepositoryBuilder()
            .WithBook("Hyperion", "Dan Simmons", 4)
            .Build();
        
        var sut = new BookStore(bookRepository);
        
        sut.AddBook(BookHelper.Neuromancer, BookHelper.WillamGibson, new CopiesCount(1));
        
        var book = bookRepository.GetAll()
            .FirstOrDefault(t => t.Title.Equals(BookHelper.Neuromancer));
        Assert.NotNull(book);
        Assert.Equal(new BookTitle("Neuromancer"), book.Title);
        Assert.Equal(new Author("Wiliam Gibson"), book.Author);
        Assert.Equal(new CopiesCount(1), book.Copies);
    }
    
    [Fact]
    public void AddBook_WhenExists_ShouldIncrementCopy()
    {
        var bookRepository = new FakeBookRepositoryBuilder()
            .WithBook("Hyperion", "Dan Simmons", 4)
            .WithBook("Neuromancer", "Wiliam Gibson", 1)
            .Build();
        
        var sut = new BookStore(bookRepository);
        
        sut.AddBook(BookHelper.Neuromancer, BookHelper.WillamGibson, new CopiesCount(1));
        
        var book = bookRepository.GetAll()
            .FirstOrDefault(b => b.Title.Equals(BookHelper.Neuromancer));
        Assert.NotNull(book);
        Assert.Equal(new BookTitle("Neuromancer"), book.Title);
        Assert.Equal(new Author("Wiliam Gibson"), book.Author);
        Assert.Equal(new CopiesCount(2), book.Copies);
        
        book = bookRepository.GetAll()
            .FirstOrDefault(b => b.Title.Equals(new BookTitle("Hyperion")) );
        Assert.Equal(new CopiesCount(4), book.Copies);
    }
    
    [Theory]
    [InlineData("Neuromancer", null, 1)]
    [InlineData(null, "Wiliam Gibson", 1)]
    [InlineData("Neuromancer", "Wiliam Gibson", 0)]
    [InlineData("Neuromancer", "Wiliam Gibson", -1)]
    [InlineData(null, null, 1)]
    [InlineData("Neuromancer", "", 1)]
    [InlineData("", "Wiliam Gibson", 1)]
    [InlineData("Neuromancer", "Wiliam Gibson", 0)]
    [InlineData("Neuromancer", "Wiliam Gibson", -1)]
    [InlineData("", "", 1)]
    public void AddBook_WhenInvalid_ShouldNotAdd(string title, string author, int copies)
    {
        var bookRepository = new FakeBookRepositoryBuilder()
            .Build();
        
        var sut = new BookStore(bookRepository);
            
        var act = () => sut.AddBook(new BookTitle(title), new Author(author), new CopiesCount(copies));
        Assert.Throws<ArgumentException>(act);
    }
}