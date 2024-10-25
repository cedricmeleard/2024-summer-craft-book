using BookStore.Models;

namespace BookStore;

public class BookStore(IProvideBooks bookRepository)
{
    public void AddBook(BookTitle title, Author author, CopiesCount copies)
    {
        Book? foundBook = bookRepository.FindBookByTitleAndAuthor(title, author);
        if (foundBook is not null) {
            foundBook.AddCopies(copies);
            return;
        }
        
        bookRepository.Add(new Book(title, author, copies));
    }

    public void SellBook(BookTitle title, Author author, CopiesCount copies)
    {
        Book? book = bookRepository.FindBookByTitleAndAuthor(title, author);
        if(book is null) return;
        
        book.RemoveCopies(copies);
        if (book.HasNotCopyLeft) {
            bookRepository.Remove(book);
        }
    }
}