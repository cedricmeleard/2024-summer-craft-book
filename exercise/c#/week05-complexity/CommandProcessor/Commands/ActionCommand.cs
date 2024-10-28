using CommandProcessor.Abstractions;

namespace CommandProcessor.Commands;

public class UnknownCommand(IConsoleWrapper consoleWrapper) : ICommand
{
    public void Execute(string parameter) => consoleWrapper.WriteLine("Unknown command");
}