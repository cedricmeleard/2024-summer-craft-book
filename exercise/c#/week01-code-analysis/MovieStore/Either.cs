namespace MovieStore;

public class Either<TData> 
    where TData : class
{
    private readonly bool _isSome;
    public readonly string Message;
    private readonly TData _data;
    public TData Data => _isSome ? _data : throw new NotSupportedException();

    private Either(TData data)
    {
        _isSome = true;
        _data = data;
    }
    private Either(string message)
    {
        _isSome = false;
        Message = message;
    }

    public static Either<TData> Nothing(string movieNotFound)
    {
        return new Either<TData>(movieNotFound);
    }
    public static Either<TData> Just(TData data)
    {
        return new Either<TData>(data);
    }
    public void Match(Action<Nothing> none, Action<TData> some)
    {
        switch (_isSome) {
            case true:
                some(Data);
                return;
            default:
                none(new Nothing(Message));
                break;
        }
    }
}