﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocation : IAuditableEntity
    {
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string FederalFundCodeDisplay => FederalFundCodeID.HasValue ? FederalFundCode.FederalFundCodeAbbrev : string.Empty;
        public string ProgramIndexDisplay => ProgramIndexID.HasValue ? ProgramIndex.ProgramIndexAbbrev : string.Empty;
        // ReSharper disable once InconsistentNaming
        public int RegionIDDisplay => RegionID.HasValue ? Region.RegionID: -1;
        public string RegionNameDisplay => Region != null ? Region.RegionName : string.Empty;

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

        public List<ProjectCode> ConvertIntsToProjectCodes(List<int> desiredProjectCodeIDs)
        {
            var convertedProjectCodes = new List<ProjectCode>();
            if (desiredProjectCodeIDs != null)
            {
                foreach (var desiredProjectCodeId in desiredProjectCodeIDs)
                {
                    convertedProjectCodes.Add(HttpRequestStorage.DatabaseEntities.ProjectCodes.SingleOrDefault(c => c.ProjectCodeID == desiredProjectCodeId));
                }
            }
            // Deal with null case
            return convertedProjectCodes;
        }

        public static List<GrantAllocation> OrderGrantAllocationsByYearPrefixedGrantNumbersThenEverythingElse(List<GrantAllocation> grantAllocations)
        {
            // Find all the GrantAllocations that have a proper year prefix ("2016-....")
            var allGrantAllocationsPrefixedWithGrantYear =
                grantAllocations.Where(ga => GetGrantYearPrefixIfPresent(ga) != null).ToList();
            var allGrantAllocationIDSPrefixedWithGrantYear =
                allGrantAllocationsPrefixedWithGrantYear.Select(ga => ga.GrantAllocationID).ToList();

            // Start out showing properly prefixed year entries, with most recent on top
            List<Models.GrantAllocation> outgoingGrantAllocations = new List<Models.GrantAllocation>();
            outgoingGrantAllocations.AddRange(
                allGrantAllocationsPrefixedWithGrantYear.OrderByDescending(x => GetGrantYearPrefixIfPresent(x)));

            // Then show everything else, alpha sorted.
            var grantAllocationsWithoutYears = grantAllocations
                .Where(ga => !allGrantAllocationIDSPrefixedWithGrantYear.Contains(ga.GrantAllocationID)).ToList();
            outgoingGrantAllocations.AddRange(grantAllocationsWithoutYears.OrderBy(x => x.Grant.GrantNumber));
            return outgoingGrantAllocations;
        }

        private static string GetGrantYearPrefixIfPresent(GrantAllocation ga)
        {
            const string yearMatchPattern = @"^(?<year>[1-9][0-9][0-9][0-9])-";

            var grantYearRegex = new Regex(yearMatchPattern);
            MatchCollection matches = grantYearRegex.Matches(ga.Grant.GrantNumber);
            if (matches.Count > 0)
            {
                var firstMatch = matches[0];
                // Grant year prefix
                return firstMatch.Groups["year"].Value;
            }

            // No grant year prefix
            return null;
        }

    }
}