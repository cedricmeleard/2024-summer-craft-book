using Moq;
using Xunit;

namespace MovieStore.Tests;

public class MovieStoreTest
{
    private readonly MovieStore _store;
    private readonly Mock<ILogger<MovieStore>> _logger;
    public MovieStoreTest()
    {
        _logger = new Mock<ILogger<MovieStore>>();
        _store = new MovieStore(_logger.Object);
        SetUp();
    }

    private void SetUp()
    {
        _store.AddMovie("001", "Inception", "Christopher Nolan", 10, 0d);
        _store.AddMovie("002", "The Matrix", "Lana Wachowski, Lilly Wachowski", 8, 0d);
        _store.AddMovie("003", "Dunkirk", "Christopher Nolan", 5, 0d);
    }

    [Fact]
    public void TestAddMovie()
    {
        _store.AddMovie("002", "The Matrix", "Lana Wachowski, Lilly Wachowski", 9, 0d);
            
        Assert.NotNull(_store.AllMovies["002"]);
        Assert.Equal(9, _store.AllMovies["002"].TotalCopies);
        _logger.Verify(l => l.Information("Movie already exists! Updating total copies."), Times.Once());
    }
        
    [Fact]
    public void TestAddMovie_WithTotalCopiesLowerThanBorrowed()
    {
        // Prepare
        _store.BorrowMovie("002");
        _store.BorrowMovie("002");
            
        // Act
        _store.AddMovie("002", "The Matrix", "Lana Wachowski, Lilly Wachowski", 1, 0d);
            
        // Assert
        _logger.Verify(l => l.Information("Cannot reduce total copies below borrowed copies."), Times.Once());
    }

    [Fact]
    public void TestRemoveMovie()
    {
        _store.RemoveMovie("001");
            
        Assert.False(_store.AllMovies.ContainsKey("001"));
        _logger.Verify(l => l.Information(It.IsAny<string>()), Times.Never());
    }
        
    [Fact]
    public void TestRemoveMovie_WhenMovieNotFound()
    {
        _store.RemoveMovie("004");
            
        _logger.Verify(l => l.Information("Movie not found!"), Times.Once());
    }

    [Fact]
    public void TestBorrowMovie()
    {
        _store.BorrowMovie("001");
        Assert.Equal(1, _store.AllMovies["001"].BorrowedCopies);
        _logger.Verify(l => l.Information(It.IsAny<string>()), Times.Never());
    }
        
    [Fact]
    public void TestBorrowMovie_WhenAllCopiesAreAlreadyBorrowed_ShouldLogError()
    {
        for (var i = 0; i < 5; i++)
            _store.BorrowMovie("003");
            
        _store.BorrowMovie("003");
        _logger.Verify(l => l.Information("All copies are currently borrowed."), Times.Once());
    }
        
    [Fact]
    public void TestBorrowMovie_WhenMovieDoesNotExist_ShouldLogError()
    {
        _store.BorrowMovie("004");
        _logger.Verify(l => l.Information("Movie not available!"), Times.Once());
    }

    [Fact]
    public void TestBuyMovie()
    {
        var movie = _store.AllMovies["001"];
        movie.UnitPrice = 5d;

        _store.BuyMovie("Durant", "001");

        Assert.Equal(9, _store.AllMovies["001"].TotalCopies);
            
        _logger.Verify(l => l.Information(It.IsAny<string>()), Times.Never());
    }
        
    [Fact]
    public void TestBuyMovie_WhenMovieNotForSale_ShouldLogError()
    {
        _store.BuyMovie("Durant", "001");
        _logger.Verify(l => l.Information("Movie not for sale"), Times.Once());
    }
        
    [Fact]
    public void TestBuyMovie_WhenMovieDoesNotExist_ShouldLogError()
    {
        _store.BuyMovie("Durant", "004");
        _logger.Verify(l => l.Information("Movie not available!"), Times.Once());
    }
        
    [Fact]
    public void TestBuyMovie_WhenAllCopiesAreBorrowed_ShouldLogError()
    {
        for (var i = 0; i < 5; i++)
            _store.BorrowMovie("003");
        _store.BuyMovie("Durant", "003");
        _logger.Verify(l => l.Information("All copies are currently borrowed."), Times.Once());
    }

    [Fact]
    public void TestReturnMovie_WhenNoCopyWereBorrowed_ShouldLogAnError()
    {
        _store.ReturnMovie("001");
        Assert.Equal(0, _store.AllMovies["001"].BorrowedCopies);
        _logger.Verify(l => l.Information("Error: No copies were borrowed."), Times.Once());
    }
        
    [Fact]
    public void TestReturnMovie()
    {
        // Prepare
        _store.BorrowMovie("001");
        // Act
        _store.ReturnMovie("001");
        // Assert
        Assert.Equal(0, _store.AllMovies["001"].BorrowedCopies);
        _logger.Verify(l => l.Information("Error: No copies were borrowed."), Times.Never());
    }
        
    [Fact]
    public void TestReturnMovie_WhenMovieDoesNotExist_ShouldLogError()
    {
        // Act
        _store.ReturnMovie("004");
        // Assert
        _logger.Verify(l => l.Information("Invalid movie ID!"), Times.Once());
    }

    [Fact]
    public void TestFindMoviesByTitle()
    {
        var movies = _store.FindMoviesByTitle("Inception");
        Assert.Single(movies);
        Assert.Equal("Inception", movies[0].Title);
    }

    [Fact]
    public void TestUpdateMovieCopies_WhenMovieDoesNotExist_ShouldLogError()
    {
        _store.UpdateMovieCopies("004", 1);
        
        _logger.Verify(l => l.Information("Movie not found!"), Times.Once());
    }
}