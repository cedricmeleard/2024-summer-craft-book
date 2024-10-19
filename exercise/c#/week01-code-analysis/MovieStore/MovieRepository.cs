using System.Collections.Immutable;
using JetBrains.Core;

namespace MovieStore;

public sealed class MovieRepository : IProvideMovie
{
    private readonly Dictionary<string, Movie> _movies = new();

    public void Add(Movie movie) => _movies.Add(movie.MovieId, movie);

    public void Remove(string id)
    {
        if (!_movies.ContainsKey(id))
            return;
        
        _movies.Remove(id);
    }

    public Either<Movie> FindById(string id)
    {
        var movie = _movies.GetValueOrDefault(id);
        return movie is null
            ? Either<Movie>.Nothing("Movie not found!")
            : Either<Movie>.Just(movie);
    }

    public ImmutableList<Movie> GetAll() => _movies.Values.ToImmutableList();
}

public class Nothing(string message)
{
    public string Message { get; private set; } = message;
}