namespace ProjectFirma.Web.Models
{
    public partial class GisDefaultMapping : IAuditableEntity
    {
        public string AuditDescriptionString =>
            $"{GisDefaultMappingID} - {this.GisUploadSourceOrganization?.GisUploadSourceOrganizationName} - {FieldDefinition.FieldDefinitionDisplayName} - {GisDefaultMappingColumnName}";
    }
}