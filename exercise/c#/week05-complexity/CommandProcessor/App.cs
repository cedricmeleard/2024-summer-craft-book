namespace CommandProcessor;

public static class App
{
    public static void Main(string[] args)
    {
        var cp = new CommandProcessor(
            new ConsoleWrapper(), 
            new CommandProvider());

        // pas foufou
        cp.ProcessCommand("greet", "Cédric" );   // Outputs: Hello, Cédric!
        cp.ProcessCommand("greet");             // Outputs: Hello, World!
        cp.ProcessCommand("exit");              // Outputs: Exiting application...
        cp.ProcessCommand("help");              // Outputs: exit, help, greet
    }
}