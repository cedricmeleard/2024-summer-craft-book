using MovieStore.Domain.Entities;
using MovieStore.Domain.Ports;
using MovieStore.Infrastructure.Adapters;

namespace MovieStore.Tests.Builders;

public class MovieRepositoryBuilder
{
    private readonly List<Movie> _movies = new();
    public IProvideMovie Build()
    {
        var provider = new MovieRepository();
        _movies.ForEach(m => provider.Add(m));
        return provider;
    }
    public MovieRepositoryBuilder WithMovie(string movieId, string title, string director, int numberOfCopies, double price)
    {
        var movie = new Movie(movieId, title, director, numberOfCopies, price);
        
        _movies.Add(movie);
        
        return this;
    }
}