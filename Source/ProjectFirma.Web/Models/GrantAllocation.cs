using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MoreLinq;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocation : IAuditableEntity
    {
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string FederalFundCodeDisplay => FederalFundCodeID.HasValue ? FederalFundCode.FederalFundCodeAbbrev : string.Empty;
        public string ProgramIndexDisplay => ProgramIndexID.HasValue ? ProgramIndex.ProgramIndexAbbrev : string.Empty;
        public string RegionNameDisplay => RegionID.HasValue ? Region.RegionName : string.Empty;

        public List<ProjectCode> ProjectCodes
        {
            get
            {
                return this.GrantAllocationProjectCodes.Select(x => x.ProjectCode).Distinct().ToList();
            }
        }

        // List of ProjectCodes as a comma delimited string ("EEB, GMX" for example)
        public string ProjectCodesAsCsvString
        {
            get
            {
                return string.Join($"{ProjectCodeSeparator} ", ProjectCodes.OrderBy(x => x.ProjectCodeAbbrev).Select(x => x.ProjectCodeAbbrev));
            }
        }

        private const string ProjectCodeSeparator = ",";
        

        public string AuditDescriptionString
        {
            get { return ProjectName; }
        }

        //private List<ProjectCode> MakeProjectCodeFromCsvString(string projectCodeCsvString)
        //{
        //    if (string.IsNullOrWhiteSpace(projectCodeCsvString))
        //    {
        //        return new List<ProjectCode>();
        //    }
        //    var projectCodeAbbreviationsStringList = Regex.Split(projectCodeCsvString, $@"\s*{ProjectCodeSeparator}\s*").Select(x => x.Trim()).Distinct(StringComparer.InvariantCultureIgnoreCase).ToList();
        //    var projectCodeList = projectCodeAbbreviationsStringList.Select(x => ProjectCodes.Single(c =>
        //        String.Equals(c.ProjectCodeAbbrev, projectCodeCsvString, StringComparison.InvariantCultureIgnoreCase))).ToList();
        //    return projectCodeList;
        //}

        public void SetProjectCodesFromCsvString(string projectCode)
        {
            throw new NotImplementedException();
        }
    }
}