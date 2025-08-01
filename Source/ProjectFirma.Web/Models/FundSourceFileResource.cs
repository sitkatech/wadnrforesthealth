using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class FundSourceFileResource : IAuditableEntity, IEntityDocument
    {
        public string AuditDescriptionString => $"{Models.FieldDefinition.Grant.GetFieldDefinitionLabel()}  \" {FundSource?.FundSourceName ?? "<Not Found>"}\" document \"{FileResource?.OriginalCompleteFileName ?? "<Not Found>"}\"";
        public string DeleteUrl => SitkaRoute<FundSourceController>.BuildUrlFromExpression(x => x.DeleteGrantFile(GrantFileResourceID));
        public string EditUrl => SitkaRoute<FundSourceController>.BuildUrlFromExpression(x => x.EditGrantFile(GrantFileResourceID));
        public string DisplayCssClass { get; set; }

        public void DeleteFullAndChildless(DatabaseEntities dbContext)
        {
            FileResource.DeleteFull(dbContext);
            DeleteFull(dbContext);
        }
    }
}
