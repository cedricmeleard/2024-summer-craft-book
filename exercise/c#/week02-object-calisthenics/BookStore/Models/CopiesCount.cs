using BookStore.Abstracts;

namespace BookStore.Models;

public class CopiesCount : ValueObject
{
    protected int _numberOfCopies;
    public CopiesCount(int numberOfCopies)
    {
        if (numberOfCopies <= 0) throw new ArgumentException("Value cannot lower or Equal to 0.",nameof(numberOfCopies));
        
        _numberOfCopies = numberOfCopies;
    }

    protected CopiesCount()
    {
        _numberOfCopies = 0;
    }
    
    public static CopiesCount Empty => new CopiesCountEmpty();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _numberOfCopies;
    }
    public CopiesCount Increment(CopiesCount additionalCopies)
    {
        return new CopiesCount(_numberOfCopies + additionalCopies._numberOfCopies);
    }
    public CopiesCount? Decrement(CopiesCount soldCopies)
    {
        return _numberOfCopies >= soldCopies._numberOfCopies
            ? new CopiesCount(_numberOfCopies - soldCopies._numberOfCopies)
            : CopiesCount.Empty;
    }
}

public class CopiesCountEmpty : CopiesCount
{
    public CopiesCountEmpty()
    {
        _numberOfCopies = 0;
    }
}