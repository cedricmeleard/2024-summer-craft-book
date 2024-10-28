using System.Reflection;
using CommandProcessor.Abstractions;

namespace CommandProcessor.Commands;

public class HelpCommand(IConsoleWrapper consoleWrapper) : IActionCommand
{
    public void Execute(string parameter)
    {
        // no dependency injection yet
        var commands = Assembly
            .GetAssembly(typeof(ICommand))?
            .GetTypes()
            .Where(t => typeof(IActionCommand).IsAssignableFrom(t) && t.IsClass)
            .Select(t => t.Name.Replace("Command", "").ToLower())
            .Order()
            .ToList();
            
        consoleWrapper.WriteLine($"Command list: {string.Join(", ",commands)}");
    }
}