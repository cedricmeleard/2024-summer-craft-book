namespace CommandProcessor;

public interface ICommandProvider
{
    Type FindByName(string command);
}