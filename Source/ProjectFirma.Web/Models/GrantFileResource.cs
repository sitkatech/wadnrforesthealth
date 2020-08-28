using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class GrantFileResource : IAuditableEntity, IEntityDocument
    {
        public string AuditDescriptionString => $"{Models.FieldDefinition.Grant.GetFieldDefinitionLabel()}  \" {Grant?.GrantName ?? "<Not Found>"}\" document \"{FileResource?.OriginalCompleteFileName ?? "<Not Found>"}\"";
        public string DeleteUrl => SitkaRoute<GrantController>.BuildUrlFromExpression(x => x.DeleteGrantFile(GrantFileResourceID));
        public string EditUrl => SitkaRoute<GrantController>.BuildUrlFromExpression(x => x.EditGrantFile(GrantFileResourceID));
        public string DisplayCssClass { get; set; }
    }
}