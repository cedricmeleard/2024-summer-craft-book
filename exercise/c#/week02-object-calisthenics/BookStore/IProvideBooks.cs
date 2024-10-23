namespace BookStore;

public interface IProvideBooks
{
    public IReadOnlyCollection<Book> GetAll();
    void Add(Book book);
    void Remove(Book book);
}
