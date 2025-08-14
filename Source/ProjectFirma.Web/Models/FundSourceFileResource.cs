using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class FundSourceFileResource : IAuditableEntity, IEntityDocument
    {
        public string AuditDescriptionString => $"{Models.FieldDefinition.FundSource.GetFieldDefinitionLabel()}  \" {FundSource?.FundSourceName ?? "<Not Found>"}\" document \"{FileResource?.OriginalCompleteFileName ?? "<Not Found>"}\"";
        public string DeleteUrl => SitkaRoute<FundSourceController>.BuildUrlFromExpression(x => x.DeleteFundSourceFile(FundSourceFileResourceID));
        public string EditUrl => SitkaRoute<FundSourceController>.BuildUrlFromExpression(x => x.EditFundSourceFile(FundSourceFileResourceID));
        public string DisplayCssClass { get; set; }

        public void DeleteFullAndChildless(DatabaseEntities dbContext)
        {
            FileResource.DeleteFull(dbContext);
            DeleteFull(dbContext);
        }
    }
}
