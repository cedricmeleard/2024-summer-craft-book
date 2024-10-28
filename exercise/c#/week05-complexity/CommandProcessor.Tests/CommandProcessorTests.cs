using CommandProcessor.Abstractions;
using Moq;

namespace CommandProcessor.Tests;

public class CommandProcessorTests
{
    [Theory]
    [InlineData("greet", "Hello, World!")]
    [InlineData("exit", "Exiting application...")]
    [InlineData("something", "Unknown command")]
    [InlineData("help", "Command list: exit, greet, help")]
    public void CommandProcessor_ShouldCall_CorrectAction(string command, string expectedResult)
    {
        var consoleMock = new Mock<IConsoleWrapper>();
        var sut = new CommandProcessor(consoleMock.Object, new CommandProvider());
        sut.ProcessCommand(command);
        
        consoleMock
            .Verify(c => c.WriteLine(expectedResult), Times.Once);
    }
    
    [Fact]
    public void GreetCommand_CanHaveParameter()
    {
        var consoleMock = new Mock<IConsoleWrapper>();
        var sut = new CommandProcessor(consoleMock.Object, new CommandProvider());
        sut.ProcessCommand("greet", "Cédric");
        
        consoleMock
            .Verify(c => c.WriteLine("Hello, Cédric!"), Times.Once);
    }
}