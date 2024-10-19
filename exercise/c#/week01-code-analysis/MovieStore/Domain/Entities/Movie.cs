namespace MovieStore.Domain.Entities;

public class Movie(string movieId, string title, string director, int totalCopies, double unitPrice)
{
    public string MovieId => movieId;
    public string Title => title;
    public string Director => director;
    public double UnitPrice { get; private set; }= unitPrice;
    public int TotalCopies { get; private set; } = totalCopies;
    public int BorrowedCopies { get; private set; } = 0;

    public void BorrowCopy() => BorrowedCopies++;
    public void ReturnCopy() => BorrowedCopies--;
    public void Bought() => TotalCopies--;
    
    private const double MinimumSellPrice = 0d;
    public bool CanSell() => UnitPrice != MinimumSellPrice;
    public void UpdateCopies(int newTotalCopies)
    {
        TotalCopies = newTotalCopies;
    }
    public void UpdatePrice(double newPrice)
    {
        UnitPrice = newPrice;
    }
    public bool CanPurchase() => TotalCopies - BorrowedCopies > 0;
}