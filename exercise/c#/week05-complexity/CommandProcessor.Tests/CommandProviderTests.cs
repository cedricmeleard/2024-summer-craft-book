namespace CommandProcessor.Tests;

public class CommandProviderTests
{
    [Theory]
    [InlineData("help")]
    [InlineData("Help")]
    [InlineData("Greet")]
    [InlineData("exit")]
    public void GetCommands_FindByName_ReturnsCommands(string command)
    {
        // Arrange
        var commandProvider = new CommandProvider();
        // Act
        var result = commandProvider.FindByName(command);
        // Assert
        Assert.NotNull(result);
        Assert.StartsWith(command, result.Name, StringComparison.InvariantCultureIgnoreCase);
    }
    
    [Fact]
    public void GetCommands_Unknown_ReturnsNull()
    {
        // Arrange
        var commandProvider = new CommandProvider();
        // Act
        var result = commandProvider.FindByName("unknown");
        // Assert
        Assert.Null(result);
    }
}