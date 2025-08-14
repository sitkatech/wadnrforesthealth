using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;

namespace ProjectFirma.Web.Models
{
    public static class ProjectUpdateProgramModelExtensions
    {
        public static HtmlString ToProgramListDisplay(this IEnumerable<ProjectUpdateProgram> projectUpdatePrograms, bool showDefaultsAsWell)
        {

            var programs = projectUpdatePrograms.Select(x => x.Program).ToList();

            var listOfStrings = new List<string>();
            foreach (var program in programs)
            {
                if (!program.IsDefaultProgramForImportOnly || showDefaultsAsWell)
                {
                    var stringReturn = UrlTemplate.MakeHrefString(program.GetDetailUrl(), program.DisplayName).ToString();
                    listOfStrings.Add(stringReturn);
                }
            }

            var returnList = string.Join(", ", listOfStrings);
            if (listOfStrings.Any())
            {
                return new HtmlString(returnList);
            }

            return new HtmlString(string.Empty);

        }
    }
}