using System.Collections.Generic;
using System.Linq;
using BookStore.Models;

namespace BookStore.Tests.TestHelpers;

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
        var book = new Book(new BookTitle(title), new Author(author), new CopiesCount(quantity));
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
    public Book? FindBookByTitleAndAuthor(BookTitle title, Author author) => 
        _inv.Find(b => b.Title.Equals(title)
                       && b.Author.Equals(author));
    
    // For Testing purpose
    public IReadOnlyCollection<Book> GetAll() => _inv.ToList();
}