namespace Documents
{
    public record Template(
        DocumentTemplate DocumentTemplate,
        RecordType RecordType,
        string DocumentType)
    {
        private static IEnumerable<Template> TemplateMappings() => new[]
        {
            new Template(DocumentTemplate.DEERPP, RecordType.IndividualProspect, "DEER"),
            new Template(DocumentTemplate.DEERPM, RecordType.LegalProspect, "DEER"),
            new Template(DocumentTemplate.AUTP, RecordType.IndividualProspect, "AUTP"),
            new Template(DocumentTemplate.AUTM, RecordType.LegalProspect, "AUTM"),
            new Template(DocumentTemplate.SPEC, RecordType.All, "SPEC"),
            new Template(DocumentTemplate.GLPP, RecordType.IndividualProspect, "GLPP"),
            new Template(DocumentTemplate.GLPM, RecordType.LegalProspect, "GLPM")
        };

        public static Template FindTemplateFor(string documentType, string recordType) 
            => TemplateMappings()
                   .FirstOrDefault(t => t.IsMatchDocumentAndRecordType(documentType, recordType)) 
                   ?? throw new ArgumentException("Invalid Document template type or record type");
        
        private bool IsMatchDocumentAndRecordType(string documentType, string recordType) 
            => DocumentType.Equals(documentType, StringComparison.InvariantCultureIgnoreCase) 
               && RecordType.ToString().Equals(recordType, StringComparison.InvariantCultureIgnoreCase);
    }
}