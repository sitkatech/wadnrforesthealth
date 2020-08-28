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

        public void AddNewFileResource(FileResource fileResource)
        {
            var grantModificationFileResource = new GrantModificationFileResource(this, fileResource, fileResource.OriginalCompleteFileName);
            GrantModificationFileResources.Add(grantModificationFileResource);
        }
    }
}