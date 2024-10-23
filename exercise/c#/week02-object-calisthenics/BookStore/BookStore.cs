namespace BookStore;

public class BookStore(IProvideBooks bookRepository)
{
    public void AddBook(string? title, string? author, int copies)
    {
        if (title == null || author == null || copies <= 0) {
            return;
        }

        Book? foundBook = bookRepository.FindBookByTitleAndAuthor(title, author);
        if (foundBook != null) {
            foundBook.AddCopies(copies);
            return;
        }
        
        bookRepository.Add(new Book(title, author, copies));
    }

    public void SellBook(string title, string author, int copies)
    {
        Book? book = bookRepository.FindBookByTitleAndAuthor(title, author);
        if(book is null) return;
        
        book.RemoveCopies(copies);
        if (book.Copies <= 0) {
            bookRepository.Remove(book);
        }
    }
}