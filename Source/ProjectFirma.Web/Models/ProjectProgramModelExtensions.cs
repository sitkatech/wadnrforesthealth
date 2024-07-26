using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;

namespace ProjectFirma.Web.Models
{
    public static class ProjectProgramModelExtensions
    {
        public static string ToProgramListDisplayForAgGrid(this IEnumerable<ProjectProgram> projectPrograms)
        {
            return projectPrograms.ToProgramListDisplayForAgGrid(true);
        }

        public static string ToProgramListDisplayForAgGrid(this IEnumerable<ProjectProgram> projectPrograms, bool showDefaultsAsWell)
        {

            var programs = projectPrograms.Select(x => x.Program).ToList();

            var listOfJsonStrings = new List<HtmlLinkObject>();
            foreach (var program in programs)
            {
                if (!program.IsDefaultProgramForImportOnly || showDefaultsAsWell)
                {
                    var stringReturn = new HtmlLinkObject(program.DisplayName, program.GetDetailUrl());
                    listOfJsonStrings.Add(stringReturn);
                }
            }

            if (listOfJsonStrings.Any())
            {
                return listOfJsonStrings.ToJsonArrayForAgGrid();
            }

            return string.Empty;

        }

        public static HtmlString ToProgramListDisplay(this IEnumerable<ProjectProgram> projectPrograms, bool showDefaultsAsWell)
        {

            var programs = projectPrograms.Select(x => x.Program).ToList();

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