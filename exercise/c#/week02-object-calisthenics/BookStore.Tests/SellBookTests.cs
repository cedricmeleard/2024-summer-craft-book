namespace BookStore.Tests;

public class SellBookTests
{
    [Fact]
    public void WhenABookIsSold_ThenItsCopyNumberShouldDecrease()
    {
        var bookRepository = new FakeBookRepositoryBuilder()
            .WithBook("Hyperion", "Dan Simmons", 4)
            .WithBook("Neuromancer", "Wiliam Gibson", 5)
            .Build();
        
        var sut = new BookStore(bookRepository);
        
        sut.SellBook("Neuromancer", "Wiliam Gibson", 2);
        
        var book = bookRepository.GetAll()
            .FirstOrDefault(t => t.Title == "Neuromancer");
        
        Assert.Equal(3, book.Copies);
    }
    
    [Fact]
    public void WhenLastCopyOfABookIsSold_ThenBookDoesNotExistAnymore()
    {
        var bookRepository = new FakeBookRepositoryBuilder()
            .WithBook("Hyperion", "Dan Simmons", 4)
            .WithBook("Neuromancer", "Wiliam Gibson", 1)
            .Build();
        
        var sut = new BookStore(bookRepository);
        
        sut.SellBook("Neuromancer", "Wiliam Gibson", 1);
        
        Assert.Single(bookRepository.GetAll());
        Assert.Equal("Hyperion", bookRepository.GetAll().First().Title);
    }
    
    [Fact]
    public void WhenThereIsNoBook_SellingABookDoesNothing()
    {
        var bookRepository = new FakeBookRepositoryBuilder()
            .WithBook("Hyperion", "Dan Simmons", 4)
            .Build();
        
        var sut = new BookStore(bookRepository);
        
        sut.SellBook("Neuromancer", "Wiliam Gibson", 1);
        
        Assert.Single(bookRepository.GetAll());
        Assert.Equal("Hyperion", bookRepository.GetAll().First().Title);
    }
}