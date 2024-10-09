namespace MovieStore;

public class MovieStore
{
    private readonly ILogger<MovieStore> _logger;
    public Dictionary<string, Movie> AllMovies { get; set; }
    public StoreAccount Sales { get; set; }

    public MovieStore(ILogger<MovieStore> logger)
    {
        _logger = logger;
            
        AllMovies = new Dictionary<string, Movie>();
        Sales = new StoreAccount();
    }

    public void BuyMovie(string customer, string id)
    {
        if (AllMovies.TryGetValue(id, out Movie movie))
        {
            if (movie.TotalCopies - movie.BorrowedCopies > 0)
            {
                movie.TotalCopies--;
                if (movie.CanSell())
                {
                    Sales.Sell(movie, customer);
                }
                else
                {
                    _logger.Information("Movie not for sale");
                }
            }
            else
            {
                _logger.Information("All copies are currently borrowed.");
            }
        }
        else
        {
            _logger.Information("Movie not available!");
        }
    }

    public void AddMovie(string id, string title, string director, int totalCopies, double unitPrice)
    {
        if (AllMovies.ContainsKey(id))
        {
            _logger.Information("Movie already exists! Updating total copies.");
            UpdateMovieCopies(id, totalCopies);
        }
        else
        {
            AllMovies.Add(id, new Movie(id, title, director, totalCopies, unitPrice));
        }
    }

    public void UpdateMovieCopies(string id, int newTotalCopies)
    {
        if (AllMovies.TryGetValue(id, out Movie movie))
        {
            if (newTotalCopies < movie.BorrowedCopies)
            {
                _logger.Information("Cannot reduce total copies below borrowed copies.");
            }
            else
            {
                movie.TotalCopies = newTotalCopies;
            }
        }
        else
        {
            _logger.Information("Movie not found!");
        }
    }

    public void RemoveMovie(string id)
    {
        if (AllMovies.ContainsKey(id))
        {
            AllMovies.Remove(id);
        }
        else
        {
            _logger.Information("Movie not found!");
        }
    }

    public void BorrowMovie(string id)
    {
        if (AllMovies.TryGetValue(id, out Movie movie))
        {
            if (movie.TotalCopies - movie.BorrowedCopies > 0)
            {
                movie.BorrowedCopies++;
            }
            else
            {
                _logger.Information("All copies are currently borrowed.");
            }
        }
        else
        {
            _logger.Information("Movie not available!");
        }
    }

    public void ReturnMovie(string id)
    {
        if (AllMovies.TryGetValue(id, out Movie movie))
        {
            if (movie.BorrowedCopies > 0)
            {
                movie.BorrowedCopies--;
            }
            else
            {
                _logger.Information("Error: No copies were borrowed.");
            }
        }
        else
        {
            _logger.Information("Invalid movie ID!");
        }
    }
    
    public List<Movie> FindMoviesByTitle(string title)
    {
        return AllMovies
            .Values
            .Where(movie => 
                movie.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}