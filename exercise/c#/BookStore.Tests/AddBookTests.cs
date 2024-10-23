namespace BookStore.Tests;

public class AddBookTests
{
    [Fact]
    public void AddBook_WhenNotExists_ShouldAddNewBook()
    {
        var bookRepository = new FakeBookRepository();
        
        var sut = new BookStore(bookRepository);
        
        sut.AddBook("Neuromancer", "Wiliam Gibson", 1);
        
        var book = bookRepository.GetAll().FirstOrDefault();
        Assert.NotNull(book);
        Assert.Equal("Neuromancer", book.Title);
        Assert.Equal("Wiliam Gibson", book.Author);
        Assert.Equal(1, book.Copies);
    }
    
    [Fact]
    public void AddBook_WhenExists_ShouldIncrementCopy()
    {
        var bookRepository = new FakeBookRepository();
        
        var sut = new BookStore(bookRepository);
        
        sut.AddBook("Neuromancer", "Wiliam Gibson", 1);
        
        var book = bookRepository.GetAll().FirstOrDefault();
        Assert.NotNull(book);
        Assert.Equal("Neuromancer", book.Title);
        Assert.Equal("Wiliam Gibson", book.Author);
        Assert.Equal(1, book.Copies);
    }
}