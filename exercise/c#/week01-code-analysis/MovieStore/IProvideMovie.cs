using System.Collections.Immutable;

namespace MovieStore;

public interface IProvideMovie
{
    void Add(Movie movie);
    void Remove(string id);
    Either<Movie> FindById(string id);
    ImmutableList<Movie> GetAll();
}