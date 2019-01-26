using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using MoreLinq;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocation : IAuditableEntity
    {
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string FederalFundCodeDisplay => FederalFundCodeID.HasValue ? FederalFundCode.FederalFundCodeAbbrev : string.Empty;
        public string ProgramIndexDisplay => ProgramIndexID.HasValue ? ProgramIndex.ProgramIndexAbbrev : string.Empty;
        public int RegionIDDisplay => RegionID.HasValue ? Region.RegionID: -1;

        [NotNull]
        public List<ProjectCode> ProjectCodes
        {
            get
            {
                return this.GrantAllocationProjectCodes.Select(x => x.ProjectCode).Distinct().ToList();
            }

            set
            {
                // Cleanup old records
                this.GrantAllocationProjectCodes.ToList().ForEach(gapc => gapc.DeleteFull(HttpRequestStorage.DatabaseEntities));
                // Create entirely new records
                this.GrantAllocationProjectCodes = value.Select(pc => new GrantAllocationProjectCode(this, pc)).ToList();
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

        public List<ProjectCode> ConvertIntsToProjectCodes(List<int> desiredProjectCodeIDs)
        {
            var convertedProjectCodes = new List<ProjectCode>();
            foreach (var desiredProjectCodeId in desiredProjectCodeIDs)
            {
                convertedProjectCodes.Add(HttpRequestStorage.DatabaseEntities.ProjectCodes.SingleOrDefault(c => c.ProjectCodeID == desiredProjectCodeId));
            }

            return convertedProjectCodes;
        }

        public void SetProjectCodesFromCsvString(string projectCode)
        {
            throw new NotImplementedException();
        }
    }
}