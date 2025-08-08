using System;
using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Common
{
    public static class ToCsvListExtensions
    {
        /// <summary>
        /// List of ProjectCodes as a comma delimited string ("EEB, GMX" for example) 
        /// </summary>
        public static string ToDistinctOrderedCsvList(this ICollection<ProjectCode> projectCodes)
        {
            return MakeDistinctCaseInsensitiveStringListFromObjectList(projectCodes, x => x.ProjectCodeName);
        }

        /// <summary>
        /// List of ProgramIndex as a comma delimited string ("234, 25B" for example) 
        /// </summary>
        public static string ToDistinctOrderedCsvList(this ICollection<ProgramIndex> programIndices)
        {
            return MakeDistinctCaseInsensitiveStringListFromObjectList(programIndices, x => x.ProgramIndexCode);
        }

        /// <summary>
        /// List of FundSources as a comma delimited string ("234, 25B" for example) 
        /// </summary>
        public static string ToDistinctOrderedCsvListOfFundSourceNumber(this ICollection<AgreementFundSourceAllocation> agreementFundSourceAllocations)
        {
            return MakeDistinctCaseInsensitiveStringListFromObjectList(agreementFundSourceAllocations, x => x.FundSourceAllocation.FundSource.FundSourceNumber);
        }

        private static string MakeDistinctCaseInsensitiveStringListFromObjectList<T>(ICollection<T> objectList, Func<T, string> funcObjectToString)
        {
            if (objectList == null)
            {
                return string.Empty;
            }

            return String.Join(", ", objectList.Where(x => x != null).Select(funcObjectToString).Distinct(StringComparer.InvariantCultureIgnoreCase).OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase));
        }
    }
}