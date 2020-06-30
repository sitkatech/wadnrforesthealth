using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public partial class GrantModification : IAuditableEntity
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
    }
}