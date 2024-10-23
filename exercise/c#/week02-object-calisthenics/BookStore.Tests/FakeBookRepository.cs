namespace BookStore.Tests;

public class FakeBookRepositoryBuilder
{
    private readonly List<Book> books = [];
    
    public FakeBookRepository Build()
    {
        var repo =  new FakeBookRepository();
        foreach (var book in books) {
            repo.Add(book);    
        }
        return repo;
    }
    public FakeBookRepositoryBuilder WithBook(string title, string author, int quantity)
    {
        var book = new Book(title, author, quantity);
        books.Add(book);

        return this;
    }
}

public class FakeBookRepository : IProvideBooks
{
    private readonly List<Book> _inv = new();
    public void Add(Book book) 
    {
        _inv.Add(book);
    }
    public void Remove(Book book)
    {
        _inv.Remove(book);
    }
    public Book? FindBookByTitleAndAuthor(string title, string author) => _inv.Find(b => b.Title == title && b.Author == author);
    
    // For Testing purpose
    public IReadOnlyCollection<Book> GetAll() => _inv.ToList();
}