namespace BookStore;

public interface IProvideBooks
{
    void Add(Book book);
    void Remove(Book book);
    Book? FindBookByTitleAndAuthor(string title, string author);
}
