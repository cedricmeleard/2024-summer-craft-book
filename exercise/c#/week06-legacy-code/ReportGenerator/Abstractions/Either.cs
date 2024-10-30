namespace ReportGenerator.Abstractions;

public class Either<TSome>
    where TSome : class
{
    private readonly TSome _some = null!;
    private readonly bool _isSome;
    
    private Either(TSome generatorGenerator)
    {
        _some = generatorGenerator;
        _isSome = true;
    }
    private Either()
    {
    }
    public static Either<TSome> Found(TSome some)
    {
        return new Either<TSome>(some);
    }
    public static Either<TSome> Nothing()
    {
        return new Either<TSome>();
    }

    public void Match(Action<TSome> someAction, Action nothingAction)
    {
        if (_isSome) {
            someAction(_some);
            return;
        }
        
        nothingAction();
    }
}