using CommandProcessor.Abstractions;

namespace CommandProcessor;

public class ConsoleWrapper : IConsoleWrapper
{
    public void WriteLine(string message) => Console.WriteLine(message);
}