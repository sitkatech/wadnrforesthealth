using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class FundSourceAllocationFileResource : IAuditableEntity, IEntityDocument
    {
        public string AuditDescriptionString => $"{Models.FieldDefinition.FundSource.GetFieldDefinitionLabel()}  \" {FundSourceAllocation?.FundSourceAllocationName ?? "<Not Found>"}\" document \"{FileResource?.OriginalCompleteFileName ?? "<Not Found>"}\"";
        public string DeleteUrl => SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(x => x.DeleteFundSourceAllocationFile(FundSourceAllocationFileResourceID));
        public string EditUrl => SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(x => x.EditFundSourceAllocationFile(FundSourceAllocationFileResourceID));
        public string DisplayCssClass { get; set; }

        public void DeleteFullAndChildless(DatabaseEntities dbContext)
        {
            FileResource.DeleteFull(dbContext);
            DeleteFull(dbContext);
        }
    }
}
