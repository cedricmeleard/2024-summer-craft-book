namespace ReportGenerator.Abstractions;

public interface IGeneratorProvider
{
    Either<IFileGenerator> FindGenerator(string reportType);
}