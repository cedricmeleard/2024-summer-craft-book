using ReportGenerator.Abstractions;
using ReportGenerator.Core;

namespace ReportGenerator.Generators;

public class CsvGenerator : IFileGenerator
{
    public void Generate(List<ReportData> data)
    {
        Console.WriteLine("Starting CSV Report Generation...");
        Console.WriteLine("CSV Header: ID, Value, Description");
        foreach (var d in data)
        {
            Console.WriteLine($"{d.Id},{d.Value},{d.Description}");
        }

        Console.WriteLine("CSV Report Generated Successfully.");
    }
}