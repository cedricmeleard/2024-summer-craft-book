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
        if (!AllMovies.TryGetValue(id, out Movie movie)) {
            _logger.Information("Movie not available!");
            return;
        }

        if (!movie.CanPurchase()) {
            _logger.Information("All copies are currently borrowed.");
            return;
        }
        
        // there might be a problem here, bought movie but cannot sell it
        movie.Bought();

        if (!movie.CanSell()) {
            _logger.Information("Movie not for sale");
            return;
        }
        
        Sales.Sell(movie, customer);
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
        if (!AllMovies.TryGetValue(id, out Movie movie)) {
            _logger.Information("Movie not found!");
            return;
        }
        
        if (newTotalCopies < movie.BorrowedCopies) {
            _logger.Information("Cannot reduce total copies below borrowed copies.");
            return;
        }
        
        movie.UpdateCopies(newTotalCopies);
    }

    public void RemoveMovie(string id)
    {
        if (!AllMovies.ContainsKey(id)) {
            _logger.Information("Movie not found!");
            return;
        }
        
        AllMovies.Remove(id);
    }

    public void BorrowMovie(string id)
    {
        if (!AllMovies.TryGetValue(id, out Movie movie)) {
            _logger.Information("Movie not available!");
            return;
        }
        if (movie.TotalCopies - movie.BorrowedCopies <= 0) {
            _logger.Information("All copies are currently borrowed.");
            return;
        }
        movie.BorrowCopy();
    }

    public void ReturnMovie(string id)
    {
        if (!AllMovies.TryGetValue(id, out Movie movie)) {
            _logger.Information("Invalid movie ID!");
            return;
        }
        if (movie.BorrowedCopies <= 0) {
            _logger.Information("Error: No copies were borrowed.");
            return;
        }
        movie.ReturnCopy();
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