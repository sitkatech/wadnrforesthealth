using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static List<ProgramIndexProjectCode> GetProgramIndexProjectCodeFindResults(this IQueryable<ProgramIndexProjectCode> programIndexProjectCodes, string programIndexProjectCodeKeyword)
        {
            //var resultSet = programIndexProjectCodes.Where(x =>
            //                                               x.ProgramIndexProjectCodeDisplayString.StartsWith(programIndexProjectCodeKeyword) ||
            //                                               x.ProgramIndexProjectCodeDisplayString.Contains(programIndexProjectCodeKeyword)).ToList();
            //return resultSet.OrderBy(x => x.ProgramIndexProjectCodeDisplayString).ToList();

            var resultSet = programIndexProjectCodes.Where(x =>
                x.ProgramIndex.ProgramIndexTitle.StartsWith(programIndexProjectCodeKeyword) ||
                x.ProjectCode.ProjectCodeName.StartsWith(programIndexProjectCodeKeyword)).ToList();
            return resultSet.OrderBy(x => x.ProgramIndexProjectCodeDisplayString).ToList();
        }
    }
}