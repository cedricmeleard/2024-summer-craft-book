using ReportGenerator.Abstractions;
using ReportGenerator.Core;

namespace ReportGenerator.Generators;

public class PdfGenerator : IFileGenerator
{
    public void Generate(List<ReportData> data)
    {
        Console.WriteLine("Starting PDF Report Generation...");
        Console.WriteLine("PDF Report Title: Comprehensive Data Report");
        Console.WriteLine("------------------------------------------------");
        foreach (var d in data)
        {
            Console.WriteLine($"Data ID: {d.Id} | Data Value: {d.Value} | Description: {d.Description}");
        }

        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("PDF Report Generated Successfully.");
    }
}