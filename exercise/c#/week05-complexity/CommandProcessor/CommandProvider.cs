using System.Reflection;
using CommandProcessor.Abstractions;

namespace CommandProcessor;

public class CommandProvider : ICommandProvider
{
    private readonly IEnumerable<Type>? _commands;
    public CommandProvider()
    {
        _commands = Assembly
            .GetAssembly(typeof(ICommand))?
            .GetTypes()
            .Where(t => typeof(IActionCommand).IsAssignableFrom(t) && t.IsClass);
    }
    public Type FindByName(string command) => _commands
        .FirstOrDefault(c => c.Name
            .StartsWith(command, StringComparison.InvariantCultureIgnoreCase));
}