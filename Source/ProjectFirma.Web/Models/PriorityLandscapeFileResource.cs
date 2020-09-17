using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class PriorityLandscapeFileResource : IAuditableEntity, IEntityDocument
    {
        public string AuditDescriptionString => $"{FieldDefinition.PriorityLandscape.GetFieldDefinitionLabel()}  \"{PriorityLandscape?.PriorityLandscapeName ?? "<Not Found>"}\" document \"{FileResource?.OriginalCompleteFileName ?? "<Not Found>"}\"";
        public string DeleteUrl => SitkaRoute<PriorityLandscapeController>.BuildUrlFromExpression(x => x.DeletePriorityLandscapeFile(PriorityLandscapeFileResourceID));
        public string EditUrl => SitkaRoute<PriorityLandscapeController>.BuildUrlFromExpression(x => x.EditPriorityLandscapeFile(PriorityLandscapeFileResourceID));
        public string DisplayCssClass { get; set; }

        public void DeleteFullAndChildless(DatabaseEntities dbContext)
        {
            FileResource.DeleteFull(dbContext);
            DeleteFull(dbContext);
        }
    }
}