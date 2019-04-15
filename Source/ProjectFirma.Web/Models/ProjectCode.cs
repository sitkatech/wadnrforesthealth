using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectCode : IAuditableEntity
    {
        public string AuditDescriptionString => ProjectCodeName;

        public static List<ProjectCode> GetListProjectCodesFromCommaDelimitedString(string commaDelimitedListOfProjectCodes)
        {
            List<ProjectCode> projectCodes = new List<ProjectCode>();

            if (commaDelimitedListOfProjectCodes != null)
            {
                foreach (var code in commaDelimitedListOfProjectCodes.Split(',').Select(s => s.Trim()))
                {
                    var foundCode = HttpRequestStorage.DatabaseEntities.ProjectCodes.SingleOrDefault(x => x.ProjectCodeName == code);
                    if (foundCode != null)
                    {
                        projectCodes.Add(foundCode);
                    }
                }
            }

            return projectCodes;
        }

    }
}