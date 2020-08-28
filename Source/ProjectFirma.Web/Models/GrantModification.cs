using System.Linq;
using ProjectFirma.Web.Views.Shared.FileResourceControls;

namespace ProjectFirma.Web.Models
{
    public partial class GrantModification : IAuditableEntity, ICanUploadNewFiles
    {
        public string GrantModificationNameForDisplay => $"{Grant.GrantNumber} - {GrantModificationName}";
        public string StartDateDisplay => GrantModificationStartDate.ToShortDateString();
        public string EndDateDisplay => GrantModificationEndDate.ToShortDateString();
        public string AuditDescriptionString => GrantModificationName;

        public string GrantModificationPurposeNamesAsCommaDelimitedString
        {
            get
            {
                var listOfNames = GrantModificationGrantModificationPurposes.Select(gmgmp => gmgmp.GrantModificationPurpose.GrantModificationPurposeName).ToList();
                return string.Join(", ", listOfNames);
            }
        }

        public void AddNewFileResource(FileResource fileResource, string displayName, string description)
        {
            var grantModificationFileResource = new GrantModificationFileResource(this, fileResource, displayName) {Description = description};
            GrantModificationFileResources.Add(grantModificationFileResource);
        }

        public void DeleteFullAndChildless(DatabaseEntities dbContext)
        {
            foreach (var x in GrantModificationFileResources.ToList())
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