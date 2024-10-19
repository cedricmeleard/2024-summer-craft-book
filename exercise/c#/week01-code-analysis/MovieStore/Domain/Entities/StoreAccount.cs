namespace MovieStore.Domain.Entities;

public class StoreAccount
{
    public double TotalSold { get; set; }
    public List<MovieSale> AllSales { get; } = new();

    public void Sell(Movie movie, string to)
    {
        TotalSold += movie.UnitPrice;
        AllSales.Add(new MovieSale(to, movie.Title));
    }

    public record MovieSale(string ClientName, string MovieName);
}