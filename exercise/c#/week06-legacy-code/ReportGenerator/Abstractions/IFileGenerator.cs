using ReportGenerator.Core;

namespace ReportGenerator.Abstractions;

public interface IFileGenerator
{
    void Generate(List<ReportData> data);
}