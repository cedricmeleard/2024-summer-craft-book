namespace BookStore.Models;

public class Book
{
    public BookTitle Title { get; }
    public Author Author { get; }
    public CopiesCount? Copies { get; private set; }
    public bool HasNotCopyLeft => Copies.Equals(CopiesCount.Empty);
    
    public Book(BookTitle title, Author author, CopiesCount copies)
    {
        Title = title;
        Author = author;
        Copies = copies;
    }

    public void AddCopies(CopiesCount additionalCopies)
    {
        Copies = Copies.Increment(additionalCopies);
    }

    public void RemoveCopies(CopiesCount soldCopies)
    {
        Copies = soldCopies.Equals(Copies)
            ? CopiesCount.Empty
            : Copies.Decrement(soldCopies);
    }
}