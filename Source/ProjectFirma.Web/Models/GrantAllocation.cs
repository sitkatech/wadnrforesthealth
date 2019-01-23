using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocation : IAuditableEntity
    {
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string FederalFundCodeDisplay => FederalFundCodeID.HasValue ? FederalFundCode.FederalFundCodeAbbrev : string.Empty;
        public string ProgramIndexDisplay => ProgramIndexID.HasValue ? ProgramIndex.ProgramIndexAbbrev : string.Empty;

        public List<ProjectCode> ProjectCodes
        {
            get
            {
                return this.GrantAllocationProjectCodes.Select(x => x.ProjectCode).Distinct().ToList();
            }
        }

        // List of ProjectCodes as a comma delimited string ("EEB, GMX" for example)
        public string ProjectCodesString
        {
            get
            {
                return string.Join(", ", ProjectCodes.Select(x => x.ProjectCodeAbbrev));
            }
        }

        public string AuditDescriptionString
        {
            get { return ProjectName; }
        }
    }
}