namespace ProjectFirma.Web.Models
{
    public partial class SocrataDataMartRawJsonImport : IAuditableEntity
    {
        public string AuditDescriptionString => $"vSocrataDataMartRawJsonImportIndex - {this.CreateDate} - SocrataDataMartRawJsonImportID: {this.SocrataDataMartRawJsonImportID} - {this.SocrataDataMartRawJsonImportTableType.SocrataDataMartRawJsonImportTableTypeName} - JSON Data Length: {this.RawJsonString.Length}";
    }
}