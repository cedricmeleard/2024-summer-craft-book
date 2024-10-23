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
        
        sut.AddBook("Neuromancer", "Wiliam Gibson", 1);
        
        var book = bookRepository.GetAll()
            .FirstOrDefault(t => t.Title == "Neuromancer");
        Assert.NotNull(book);
        Assert.Equal("Neuromancer", book.Title);
        Assert.Equal("Wiliam Gibson", book.Author);
        Assert.Equal(1, book.Copies);
    }
    
    [Fact]
    public void AddBook_WhenExists_ShouldIncrementCopy()
    {
        var bookRepository = new FakeBookRepositoryBuilder()
            .WithBook("Hyperion", "Dan Simmons", 4)
            .WithBook("Neuromancer", "Wiliam Gibson", 1)
            .Build();
        
        var sut = new BookStore(bookRepository);
        
        sut.AddBook("Neuromancer", "Wiliam Gibson", 1);
        
        var book = bookRepository.GetAll()
            .FirstOrDefault(b => b.Title == "Neuromancer");
        Assert.NotNull(book);
        Assert.Equal("Neuromancer", book.Title);
        Assert.Equal("Wiliam Gibson", book.Author);
        Assert.Equal(2, book.Copies);
        
        book = bookRepository.GetAll()
            .FirstOrDefault(b => b.Title == "Hyperion");
        Assert.Equal(4, book.Copies);
    }
    
    [Theory]
    [InlineData("Neuromancer", null, 1)]
    [InlineData(null, "Wiliam Gibson", 1)]
    [InlineData("Neuromancer", "Wiliam Gibson", 0)]
    [InlineData("Neuromancer", "Wiliam Gibson", -1)]
    [InlineData(null, null, 1)]
    public void AddBook_WhenInvalid_ShouldNotAdd(string title, string author, int copies)
    {
        var bookRepository = new FakeBookRepositoryBuilder()
            .Build();
        
        var sut = new BookStore(bookRepository);
        
        // TODO there is a smell here, only null and not null or empty    
        sut.AddBook(title, author, copies);

        Assert.Empty(bookRepository.GetAll());
    }
}