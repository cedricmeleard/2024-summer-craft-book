using MovieStore.Domain.Ports;

namespace MovieStore.Infrastructure.Adapters;

public class DummyLogger : ILogger
{
    public void Information(string message)
    {
        Console.WriteLine(message);
    }
}