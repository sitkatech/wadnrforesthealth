using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocation
    {
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
    }
}