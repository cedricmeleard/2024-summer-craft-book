using CommandProcessor.Abstractions;

namespace CommandProcessor.Commands;

public class GreetCommand(IConsoleWrapper consoleWrapper) : IActionCommand
{
    public void Execute(string parameter) 
        => consoleWrapper.WriteLine($"Hello, {(string.IsNullOrWhiteSpace(parameter) ? "World" : parameter)}!");
}