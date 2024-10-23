namespace BookStore;

public class BookStore(IProvideBooks bookRepository)
{
    public void AddBook(string? title, string? author, int copies)
    {
        if (title != null && author != null && copies > 0)
        {
            Book? foundBook = null;
            foreach (var book in bookRepository.GetAll())
            {
                if (book.Title == title && book.Author == author)
                {
                    foundBook = book;
                    break;
                }
            }

            if (foundBook != null)
            {
                foundBook.AddCopies(copies);
            }
            else
            {
                bookRepository.Add(new Book(title, author, copies));
            }
        }
    }

    public void SellBook(string title, string author, int copies)
    {
        foreach (var book in bookRepository.GetAll())
        {
            if (book.Title == title && book.Author == author)
            {
                book.RemoveCopies(copies);
                if (book.Copies <= 0)
                {
                    bookRepository.Remove(book);
                }

                break;
            }
        }
    }
}