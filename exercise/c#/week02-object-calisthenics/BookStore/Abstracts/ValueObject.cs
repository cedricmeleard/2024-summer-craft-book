namespace BookStore.Abstracts;

/// <summary>
/// Represents an abstract base class for value objects in the domain.
/// Provides functionality for checking equality based on components.
/// <see href="https://learn.microsoft.com/fr-fr/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects"/>
/// </summary>
public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode() => GetEqualityComponents()
        .Select(x => x.GetHashCode())
        .Aggregate((x, y) => x ^ y);
    
    
}