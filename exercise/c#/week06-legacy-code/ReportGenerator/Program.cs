using ReportGenerator.Core;

namespace ReportGenerator;

public static class Program
{
    public static void Main(string[] args)
    {
        List<ReportData> data =
        [
            new(1, 100.0, "Sample Data 1"),
            new(2, 200.0, "Sample Data 2")
        ];
        
        // use dependency injection latter
        var generator = new ReportGenerator(new FileGeneratorProvider(), data);
            
        generator.GenerateReport("CSV");
        generator.GenerateReport("PDF");
        generator.GenerateReport("JSON");
    }
}