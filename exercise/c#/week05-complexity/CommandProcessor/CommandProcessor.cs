using Ardalis.GuardClauses;
using CommandProcessor.Abstractions;
using CommandProcessor.Commands;

namespace CommandProcessor;

public class CommandProcessor
{
    private readonly IConsoleWrapper _consoleWrapper;
    private readonly ICommandProvider _commandProvider;
    public CommandProcessor(IConsoleWrapper consoleWrapper, ICommandProvider commandProvider)
    {
        _consoleWrapper = consoleWrapper;
        _commandProvider = commandProvider;
    }
    public void ProcessCommand(string command, string parameter = "")
    {
        var foundCommand = _commandProvider.FindByName(command);
        
        if (foundCommand is null) {
            new UnknownCommand(_consoleWrapper).Execute(string.Empty);
            return;
        }

        Execute(foundCommand, parameter);
    }
    
    private void Execute(Type foundCommand, string parameter)
    {
        Guard.Against.Null(foundCommand);
        
        (Activator.CreateInstance(foundCommand!, _consoleWrapper) as IActionCommand)?
            .Execute(parameter);
    }
}