using BookStore.Abstracts;

namespace BookStore.Models;

public sealed class Author : ValueObject
{
    private readonly string _author;
    public Author(string author)
    {
        if (string.IsNullOrWhiteSpace(author)) 
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(author));
        
        _author = author;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _author;
    }
}