using System.Collections.Immutable;
using MovieStore.Domain.Abstractions;
using MovieStore.Domain.Entities;

namespace MovieStore.Domain.Ports;

public interface IProvideMovie
{
    void Add(Movie movie);
    void Remove(string id);
    Either<Movie> FindById(string id);
    ImmutableList<Movie> GetAll();
}