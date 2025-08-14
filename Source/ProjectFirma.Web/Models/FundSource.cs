using System.Linq;
using ProjectFirma.Web.Views.Shared.FileResourceControls;

namespace ProjectFirma.Web.Models
{
    public partial class FundSource : IAuditableEntity, ICanUploadNewFiles
    {
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string FundSourceTypeDisplay => FundSourceTypeID.HasValue ? FundSourceType.FundSourceTypeName : string.Empty;
        public string FundSourceTitle => string.IsNullOrWhiteSpace(ShortName) ? FundSourceName : $"{FundSourceName} ({ShortName})";
        public string AuditDescriptionString => FundSourceName;

        public void AddNewFileResource(FileResource fileResource, string displayName, string description)
        {
            var fundSourceFileResource = new FundSourceFileResource(this, fileResource, displayName) {Description = description};
            FundSourceFileResources.Add(fundSourceFileResource);
        }

        public void DeleteFullAndChildless(DatabaseEntities dbContext)
        {
            foreach (var x in FundSourceFileResources.ToList())
            {
                x.DeleteFullAndChildless(dbContext);
            }

            foreach (var x in FundSourceAllocations.ToList())
            {
                x.DeleteFullAndChildless(dbContext);
            }

            DeleteChildren(dbContext);
            Delete(dbContext);
        }
    }
}
