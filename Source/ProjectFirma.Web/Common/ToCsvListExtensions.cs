using System;
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Common
{
    public static class ToCsvListExtensions
    {
        /// <summary>
        /// List of ProjectCodes as a comma delimited string ("EEB, GMX" for example) 
        /// </summary>
        public static string ToDistinctOrderedCsvList(this List<ProjectCode> projectCodes)
        {
            return MakeDistinctCaseInsensitiveStringListFromObjectList(projectCodes, x => x.ProjectCodeAbbrev);
        }

        /// <summary>
        /// List of ProgramIndex as a comma delimited string ("234, 25B" for example) 
        /// </summary>
        public static string ToDistinctOrderedCsvList(this List<ProgramIndex> programIndices)
        {
            return MakeDistinctCaseInsensitiveStringListFromObjectList(programIndices, x => x.ProgramIndexAbbrev);
        }

        private static string MakeDistinctCaseInsensitiveStringListFromObjectList<T>(List<T> projectCodes, Func<T, string> funcObjectToString)
        {
            return String.Join(", ", projectCodes.Select(funcObjectToString).OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).Distinct(StringComparer.InvariantCultureIgnoreCase));
        }
    }
}