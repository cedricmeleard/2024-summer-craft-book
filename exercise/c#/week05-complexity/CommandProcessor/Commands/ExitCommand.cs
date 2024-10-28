using CommandProcessor.Abstractions;

namespace CommandProcessor.Commands;

public class ExitCommand(IConsoleWrapper consoleWrapper) : IActionCommand
{
    public void Execute(string parameter) => consoleWrapper.WriteLine($"Exiting application...");
}