using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using JetBrains.Annotations;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocation : IAuditableEntity
    {
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string FederalFundCodeDisplay => FederalFundCodeID.HasValue ? FederalFundCode.FederalFundCodeAbbrev : string.Empty;
        public string ProgramIndexDisplay => ProgramIndexID.HasValue ? ProgramIndex.ProgramIndexCode : string.Empty;

        public string GrantNumberAndGrantAllocationDisplayName => $"{Grant.GrantNumber} {GrantAllocationName}";

        public HtmlString GrantNumberAndGrantAllocationDisplayNameAsUrl => UrlTemplate.MakeHrefString(SummaryUrl, GrantNumberAndGrantAllocationDisplayName);

        // ReSharper disable once InconsistentNaming
        public int RegionIDDisplay => RegionID.HasValue ? Region.RegionID: -1;
        public string RegionNameDisplay => Region != null ? Region.RegionName : string.Empty;
        public string DivisionNameDisplay => Division != null ? Division.DivisionDisplayName : string.Empty;

        public string DisplayName => this.GrantAllocationName;
        public HtmlString DisplayNameAsUrl => UrlTemplate.MakeHrefString(SummaryUrl, DisplayName);

        public string SummaryUrl
        {
            get { return SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(x => x.GrantAllocationDetail(GrantAllocationID)); }
        }

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

        public string AuditDescriptionString
        {
            get { return GrantAllocationName; }
        }

        public List<int> ProgramManagerPersonIDs
        {
            get { return ProgramManagerPersons.Select(p => p.PersonID).ToList(); }
        }

        public List<Person> ProgramManagerPersons
        {
            get { return GrantAllocationProgramManagers.Select(gapm => gapm.Person).ToList(); }
        }

        public string GetAllProgramManagerPersonNamesAsString()
        {
            return string.Join(", ", this.ProgramManagerPersons.Select(pmp => pmp.FullNameFirstLast));
        }

        public HtmlString GetAllProgramManagerPersonNamesAsUrls()
        {
            var programManagerUrlsList = this.ProgramManagerPersons.Select(pmp => pmp.GetFullNameFirstLastAsUrl()).ToList();
            var programManagerUrlsStringBuilder = new StringBuilder();
            foreach (var programManagerUrl in programManagerUrlsList)
            {
                if (programManagerUrlsStringBuilder.Length < 1)
                {
                    programManagerUrlsStringBuilder.Append(programManagerUrl);
                }
                else
                {
                    programManagerUrlsStringBuilder.Append(", " + programManagerUrl);
                }
            }
            return new HtmlString(programManagerUrlsStringBuilder.ToString());
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

        /// <summary>
        /// This is a bit speculative, and may not be what we really need. -- SLG
        /// </summary>
        public Organization BottommostOrganization
        {
            get
            {
                if (this.Organization != null)
                {
                    return this.Organization;
                }

                return this.Grant.Organization;
            }
        }

        public int? ProjectsWhereYouAreTheGrantAllocationMinCalendarYear
        {
            get { return ProjectGrantAllocationExpenditures.Any() ? ProjectGrantAllocationExpenditures.Min(x => x.CalendarYear) : (int?)null; }
        }

        public int? ProjectsWhereYouAreTheGrantAllocationMaxCalendarYear
        {
            get { return ProjectGrantAllocationExpenditures.Any() ? ProjectGrantAllocationExpenditures.Max(x => x.CalendarYear) : (int?)null; }
        }

        /// <summary>
        /// Stand-in for what used to be GrantAllocation.FixedLengthDisplayName
        /// </summary>
        public string FixedLengthDisplayName
        {
            get
            {
                if (BottommostOrganization.IsUnknown)
                {
                    return BottommostOrganization.OrganizationShortNameIfAvailable;
                }
                var organizationShortNameIfAvailable = $"({Organization.OrganizationShortNameIfAvailable})";
                return organizationShortNameIfAvailable.Length < 45 ? $"{GrantAllocationName.ToEllipsifiedString(45 - organizationShortNameIfAvailable.Length)} {organizationShortNameIfAvailable}" : $"{GrantAllocationName} {organizationShortNameIfAvailable}";
            }
        }


    }
}