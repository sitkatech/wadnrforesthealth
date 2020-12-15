namespace ProjectFirma.Web.Models
{
    public partial class GisCrossWalkDefault : IAuditableEntity
    {
        // I believe none of this is nullable, so should always work -- SLG
        public string AuditDescriptionString =>
            $"{GisCrossWalkDefaultID} - {this.GisUploadSourceOrganization?.GisUploadSourceOrganizationName} - {FieldDefinition.FieldDefinitionDisplayName} - {GisCrossWalkSourceValue} - {GisCrossWalkMappedValue}";
    }
}