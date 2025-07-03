using System.Linq;
using ProjectFirma.Web.Views.Shared.FileResourceControls;

namespace ProjectFirma.Web.Models
{
    public partial class Grant : IAuditableEntity, ICanUploadNewFiles
    {
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string GrantTypeDisplay => GrantTypeID.HasValue ? GrantType.GrantTypeName : string.Empty;
        public string GrantTitle => string.IsNullOrWhiteSpace(ShortName) ? GrantName : $"{GrantName} ({ShortName})";
        public string AuditDescriptionString => GrantName;

        public void AddNewFileResource(FileResource fileResource, string displayName, string description)
        {
            var grantFileResource = new GrantFileResource(this, fileResource, displayName) {Description = description};
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
