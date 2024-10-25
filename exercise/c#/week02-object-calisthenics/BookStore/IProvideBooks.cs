using BookStore.Models;

namespace BookStore;

public interface IProvideBooks
{
    void Add(Book book);
    void Remove(Book book);
    Book? FindBookByTitleAndAuthor(BookTitle title, Author author);
}
