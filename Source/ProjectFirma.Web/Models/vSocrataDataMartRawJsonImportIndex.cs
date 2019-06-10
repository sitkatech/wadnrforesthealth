namespace ProjectFirma.Web.Models
{
    public partial class vSocrataDataMartRawJsonImportIndex : IAuditableEntity
    {
        public string AuditDescriptionString => $"vSocrataDataMartRawJsonImportIndex - {this.CreateDate} - SocrataDataMartRawJsonImportID: {this.SocrataDataMartRawJsonImportID} - {this.SocrataDataMartRawJsonImportTableTypeName} - JSON Data Length: {this.RawJsonStringLength}";
    }
}