
namespace ReportGenerator.Tests;

public class ProgramTests
{
    [Fact]
    public async Task GoldenMasterTest()
    {
        // Rediriger la sortie de la console
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Appeler la méthode qui écrit dans la console
        Program.Main(new string[0]);

        // Capturer la sortie de la console
        var output = consoleOutput.ToString();

        // Vérifier la sortie avec Verify.Xunit
        await Verifier.Verify(output);
    }
}