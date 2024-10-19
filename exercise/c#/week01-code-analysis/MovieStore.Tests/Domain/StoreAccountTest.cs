using MovieStore.Domain.Entities;
using Xunit;

namespace MovieStore.Tests.Domain;

public class StoreAccountTest
{
    [Fact]
    public void TestSell()
    {
        var sut = new StoreAccount();
        var movie = new Movie("001", "Inception", "Christopher Nolan", 10, 0d);
        
        sut.Sell(movie, "Michel");

        Assert.Single(sut.AllSales);
        Assert.Equal(0d, sut.TotalSold);
    }
    
    [Fact]
    public void TestSell_WithCorrectPrice()
    {
        var sut = new StoreAccount();
        
        var inception = new Movie("001", "Inception", "Christopher Nolan", 10, 10d);
        var matrix = new Movie("002", "The Matrix", "Lana Wachowski, Lilly Wachowski", 8, 5d);
        
        sut.Sell(inception, "Michel");
        sut.Sell(matrix, "Michel");

        Assert.Equal(2, sut.AllSales.Count);
        Assert.Equal(15d, sut.TotalSold);
        Assert.All(sut.AllSales, movieSale => Assert.Equal("Michel", movieSale.ClientName));
    }
}