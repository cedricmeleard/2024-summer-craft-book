using BookStore.Abstracts;

namespace BookStore.Models;

public sealed class BookTitle : ValueObject
{
    private readonly string _title;
    public BookTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) 
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(title));
        
        _title = title;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _title;
    }
}