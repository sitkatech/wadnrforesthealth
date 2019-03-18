using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static List<ProjectCode> GetProjectCodeFindResults(this IQueryable<ProjectCode> projectCodes, string projectCodeKeyword)
        {
            return projectCodes.Where(x => x.ProjectCodeAbbrev.StartsWith(projectCodeKeyword))
                    .OrderBy(x => x.ProjectCodeAbbrev)
                    .ToList();
        }

    }
}