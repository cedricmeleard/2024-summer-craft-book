using ReportGenerator.Abstractions;
using ReportGenerator.Generators;

namespace ReportGenerator;

public class FileGeneratorProvider : IGeneratorProvider
{
    private readonly List<(string Type, IFileGenerator? Generator)> _generators
        =
        [
            ("Pdf", new PdfGenerator()),
            ("Csv", new CsvGenerator())
        ];

    public Either<IFileGenerator> FindGenerator(string reportType)
    {
        var generator = _generators
            .Find(p => p.Type.Equals(reportType, StringComparison.InvariantCultureIgnoreCase));

        return generator.Generator is null
            ? Either<IFileGenerator>.Nothing()
            : Either<IFileGenerator>.Found(generator.Generator);
    }
}

public class NotAGenerator(string reportType)
{
}