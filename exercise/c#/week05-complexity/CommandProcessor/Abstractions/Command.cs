namespace CommandProcessor.Abstractions;

public interface ICommand
{
    void Execute(string parameter);
}

public interface IActionCommand : ICommand
{
}