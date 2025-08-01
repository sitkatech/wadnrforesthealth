using System.Linq;
using ProjectFirma.Web.Views.Shared.FileResourceControls;

namespace ProjectFirma.Web.Models
{
    public partial class FundSource : IAuditableEntity, ICanUploadNewFiles
    {
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string GrantTypeDisplay => FundSourceTypeID.HasValue ? FundSourceType.GrantTypeName : string.Empty;
        public string GrantTitle => string.IsNullOrWhiteSpace(ShortName) ? FundSourceName : $"{FundSourceName} ({ShortName})";
        public string AuditDescriptionString => FundSourceName;

        public void AddNewFileResource(FileResource fileResource, string displayName, string description)
        {
            var grantFileResource = new FundSourceFileResource(this, fileResource, displayName) {Description = description};
            GrantFileResources.Add(grantFileResource);
        }

        public void DeleteFullAndChildless(DatabaseEntities dbContext)
        {
            foreach (var x in GrantFileResources.ToList())
            {
                x.DeleteFullAndChildless(dbContext);
            }

            foreach (var x in GrantAllocations.ToList())
            {
                x.DeleteFullAndChildless(dbContext);
            }

            DeleteChildren(dbContext);
            Delete(dbContext);
        }
    }
}
