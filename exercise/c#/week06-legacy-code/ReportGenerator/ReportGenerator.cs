using ReportGenerator.Abstractions;
using ReportGenerator.Core;

namespace ReportGenerator;

public class ReportGenerator(IGeneratorProvider provider, List<ReportData> data)
{
    public void GenerateReport(string reportType)
    {
        provider
            .FindGenerator(reportType)
            .Match(some => some.Generate(data),
                () => UnsupportedReportTypeMessage(reportType));
    }
    
    private static void UnsupportedReportTypeMessage(string reportType)
    {
        Console.WriteLine($"Report type {reportType} not supported.");
    }
}