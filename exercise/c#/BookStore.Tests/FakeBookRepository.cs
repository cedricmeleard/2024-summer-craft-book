namespace BookStore.Tests;

public class FakeBookRepository : IProvideBooks
{
    private readonly List<Book> _inv = new();
    public IReadOnlyCollection<Book> GetAll() => _inv.ToList();
    public void Add(Book book) 
    {
        _inv.Add(book);
    }
    public void Remove(Book book)
    {
        _inv.Remove(book);
    }
}