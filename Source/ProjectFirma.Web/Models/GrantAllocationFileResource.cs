using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationFileResource : IAuditableEntity, IEntityDocument
    {
        public string AuditDescriptionString => $"{Models.FieldDefinition.Grant.GetFieldDefinitionLabel()}  \" {FundSourceAllocation?.GrantAllocationName ?? "<Not Found>"}\" document \"{FileResource?.OriginalCompleteFileName ?? "<Not Found>"}\"";
        public string DeleteUrl => SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(x => x.DeleteGrantAllocationFile(GrantAllocationFileResourceID));
        public string EditUrl => SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(x => x.EditGrantAllocationFile(GrantAllocationFileResourceID));
        public string DisplayCssClass { get; set; }

        public void DeleteFullAndChildless(DatabaseEntities dbContext)
        {
            FileResource.DeleteFull(dbContext);
            DeleteFull(dbContext);
        }
    }
}
