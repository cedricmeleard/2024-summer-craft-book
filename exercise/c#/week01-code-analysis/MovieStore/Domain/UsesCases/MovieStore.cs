using MovieStore.Domain.Entities;
using MovieStore.Domain.Ports;

namespace MovieStore.Domain.UsesCases;

public class MovieStore(ILogger logger, IProvideMovie movieProvider)
{
    public Dictionary<string, Movie> AllMovies => movieProvider
        .GetAll()
        .ToDictionary(m => m.MovieId, m => m);
    
    private StoreAccount Sales { get; } = new();

    public void BuyMovie(string customer, string id)
    {
        var eitherAMovie = movieProvider.FindById(id);
        eitherAMovie.Match(
            nothing => logger.Information(nothing.Message),
            movie =>
            {
                if (!movie.CanPurchase()) {
                    logger.Information("All copies are currently borrowed.");
                    return;
                }

                if (!movie.CanSell()) {
                    logger.Information("Movie not for sale");
                    return;
                }
        
                movie.Bought();
                Sales.Sell(movie, customer);        
            });
    }
    
    public void AddMovie(string id, string title, string director, int totalCopies, double unitPrice)
    {
        var eitherAMovie = movieProvider.FindById(id);
        eitherAMovie.Match(
            nothing =>
            {
                movieProvider.Add(new Movie(id, title, director, totalCopies, unitPrice));
            },
            movie =>
            {
                logger.Information("Movie already exists! Updating total copies.");
                UpdateMovieCopies(id, totalCopies);
            });
    }

    public void UpdateMovieCopies(string id, int newTotalCopies)
    {
        var eitherAMovie = movieProvider.FindById(id);
        eitherAMovie.Match(
            nothing => logger.Information(nothing.Message),
            movie =>
            {
                if (newTotalCopies < movie.BorrowedCopies) {
                    logger.Information("Cannot reduce total copies below borrowed copies.");
                    return;
                }
        
                movie.UpdateCopies(newTotalCopies);
            });
    }

    public void RemoveMovie(string id)
    {
        var eitherAMovie = movieProvider.FindById(id);
        eitherAMovie.Match(
            nothing => logger.Information(nothing.Message),
                movie => movieProvider.Remove(movie.MovieId));
    }

    public void BorrowMovie(string id)
    {
        var eitherAMovie = movieProvider.FindById(id);
        eitherAMovie.Match(
            nothing => logger.Information(nothing.Message),
            movie =>
            {
                if (movie.TotalCopies - movie.BorrowedCopies <= 0) {
                    logger.Information("All copies are currently borrowed.");
                    return;
                }
                movie.BorrowCopy();        
            });
    }

    public void ReturnMovie(string id)
    {
        var eitherAMovie = movieProvider.FindById(id);
        eitherAMovie.Match(
            nothing => logger.Information(nothing.Message),
            movie =>
            {
                if (movie.BorrowedCopies <= 0) {
                    logger.Information("Error: No copies were borrowed.");
                    return;
                }
                movie.ReturnCopy();        
            });
    }
    
    public List<Movie> FindMoviesByTitle(string title)
    {
        return movieProvider.GetAll()
            .Where(movie => movie.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}