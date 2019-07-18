namespace ProjectFirma.Web.Models
{
    // If this is included in AuditLogging, we get quickly get a massive (20+ GB) leak in short order, and this appears to be responsible.
    // This is deliberately excluded from AuditLogging for this reason. -- SLG 7/18/2019
    /*
    public partial class SocrataDataMartRawJsonImport : IAuditableEntity
    {
        public string AuditDescriptionString => $"vSocrataDataMartRawJsonImportIndex - {this.CreateDate} - SocrataDataMartRawJsonImportID: {this.SocrataDataMartRawJsonImportID} - {this.SocrataDataMartRawJsonImportTableType.SocrataDataMartRawJsonImportTableTypeName} - JSON Data Length: {this.RawJsonString.Length}";
    }
    */
    
}