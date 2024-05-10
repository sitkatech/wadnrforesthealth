using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;

namespace ProjectFirma.Web.Models
{
    public static class ProjectProgramModelExtensions
    {
        public static HtmlString ToProgramListDisplay(this IEnumerable<ProjectProgram> projectPrograms)
        {
            return projectPrograms.ToProgramListDisplay(true);
        }

        public static HtmlString ToProgramListDisplay(this IEnumerable<ProjectProgram> projectPrograms, bool showDefaultsAsWell)
        {

            var programs = projectPrograms.Select(x => x.Program).ToList();

            var listOfStrings = new List<string>();
            foreach (var program in programs)
            {
                if (!program.IsDefaultProgramForImportOnly || showDefaultsAsWell)
                {
                    //5/9/24 TK: todo: making this text only to prevent performance issues ideally this would be a list of links
                    var stringReturn = program.DisplayName; //UrlTemplate.MakeHrefString(program.GetDetailUrl(), program.DisplayName).ToString();
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