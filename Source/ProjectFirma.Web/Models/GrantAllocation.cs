﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Views.Shared.FileResourceControls;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocation : IAuditableEntity, ICanUploadNewFiles
    {
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string FederalFundCodeDisplay => FederalFundCodeID.HasValue ? FederalFundCode.FederalFundCodeAbbrev : string.Empty;

        public string GrantNumberAndGrantAllocationDisplayName => $"{GrantModification.Grant.GrantNumber} {GrantAllocationName}";

        public string AllocationAmountCurrencyDisplay => $"{AllocationAmount.ToStringCurrency()}";

        public string GrantNumberAndGrantAllocationWithAllocationAmountDisplay => $"{GrantNumberAndGrantAllocationDisplayName} ({AllocationAmountCurrencyDisplay})";

        public HtmlString GrantNumberAndGrantAllocationDisplayNameAsUrl => UrlTemplate.MakeHrefString(SummaryUrl, GrantNumberAndGrantAllocationDisplayName);

        // ReSharper disable once InconsistentNaming
        public int RegionIDDisplay => DNRUplandRegionID.HasValue ? DNRUplandRegion.DNRUplandRegionID : -1;
        public string RegionNameDisplay => DNRUplandRegion != null ? DNRUplandRegion.DNRUplandRegionName : string.Empty;
        public string DivisionNameDisplay => Division != null ? Division.DivisionDisplayName : string.Empty;

        public string DisplayName => this.GrantAllocationName;
        public HtmlString DisplayNameAsUrl => UrlTemplate.MakeHrefString(SummaryUrl, DisplayName);

        public string SummaryUrl
        {
            get { return SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(x => x.GrantAllocationDetail(GrantAllocationID)); }
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

        public HtmlString GetAllLikelyToUsePersonNamesAsUrls()
        {
            var likelyToUseUrlsList = this.GrantAllocationLikelyPeople.Select(ltup => ltup.Person.GetFullNameFirstLastAsUrl()).ToList();
            var likelyToUseUrlsStringBuilder = new StringBuilder();
            foreach (var likelyToUseUrl in likelyToUseUrlsList)
            {
                if (likelyToUseUrlsStringBuilder.Length < 1)
                {
                    likelyToUseUrlsStringBuilder.Append(likelyToUseUrl);
                }
                else
                {
                    likelyToUseUrlsStringBuilder.Append(", " + likelyToUseUrl);
                }
            }
            return new HtmlString(likelyToUseUrlsStringBuilder.ToString());
        }

        /// <summary>
        /// Allocation is the percentage based on pay amount total from "expected funding by project" section from the grant allocation detail page divided by the contractual amount from the "grant allocation budget line items" section on the grant allocation detail page
        /// </summary>
        /// <returns></returns>
        public decimal? GetAllocation()
        {
            var expectedFundingByProject = ProjectGrantAllocationRequests.Sum(y => y.PayAmount);
            var contractualAmount = GrantAllocationBudgetLineItems.Where(z => z.CostType == CostType.Contractual)
                .Sum(z => z.GrantAllocationBudgetLineItemAmount);
            if (contractualAmount == 0) return expectedFundingByProject;
            return expectedFundingByProject / contractualAmount;
        }

        public decimal GetOverallBalance()
        {
            var allocationCurrentBalance =
                this.GetTotalBudgetVsActualLineItem().BudgetMinusExpendituresFromDatamart;
            var indirect = GrantAllocationBudgetLineItems.Where(z => z.CostType == CostType.IndirectCosts)
                .Sum(z => z.GrantAllocationBudgetLineItemAmount);
            return allocationCurrentBalance - indirect;
        }


        public HtmlString ToLikelyToUsePeopleListDisplay()
        {

            var likelyToUse = GrantAllocationLikelyPeople.Select(x=>x.Person.GetFullNameFirstLastAsUrl()).ToList();
            
            var returnList = string.Join(", ", likelyToUse);
            if (likelyToUse.Any())
            {
                return new HtmlString(returnList);
            }

            return new HtmlString(string.Empty);

        }
        private Dictionary<int, string> AllocationColor = new Dictionary<int, string>()
        {
            {0,  "#00B050"},
            {10, "#22B756"},
            {20, "#44BF5D"},
            {30, "#66C764"},
            {40, "#88CF6B"},
            {50, "#AAD772"},
            {60, "#CCDF79"},
            {70, "#EEE780"},
            {80, "#FFDC7C"},
            {90, "#FFBD6A"},
            {100, "#FF9D59"},
            {110, "#FF7E47"},
            {120, "#FF5E35"},
            {130, "#FF3F24"},
            {140, "#FF2012"},
            {150, "#FF0000"}
        };
        public string GetAllocationCssClass(decimal? percentage)
        {
            
            var integerLookup = Math.Floor((percentage ?? 0) * 10)*10;
            integerLookup = Math.Min(integerLookup, 150);
            integerLookup = Math.Max(integerLookup, 0);
            return AllocationColor[(int)integerLookup];
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
            outgoingGrantAllocations.AddRange(grantAllocationsWithoutYears.OrderBy(x => x.GrantModification.Grant.GrantNumber));
            return outgoingGrantAllocations;
        }

        private static string GetGrantYearPrefixIfPresent(GrantAllocation ga)
        {
            const string yearMatchPattern = @"^(?<year>[1-9][0-9][0-9][0-9])-";

            var grantYearRegex = new Regex(yearMatchPattern);
            var grantNumber = ga.GrantModification.Grant.GrantNumber;
            if (string.IsNullOrEmpty(grantNumber))
            {
                return null;
            }
            MatchCollection matches = grantYearRegex.Matches(grantNumber);
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

                return this.GrantModification.Grant.Organization;
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
                var organizationShortNameIfAvailable = $"({BottommostOrganization.OrganizationShortNameIfAvailable})";
                return organizationShortNameIfAvailable.Length < 45 ? $"{GrantAllocationName.ToEllipsifiedString(45 - organizationShortNameIfAvailable.Length)} {organizationShortNameIfAvailable}" : $"{GrantAllocationName} {organizationShortNameIfAvailable}";
            }
        }

        public void AddNewFileResource(FileResource fileResource, string displayName, string description)
        {
            var grantAllocationFileResource =
                new GrantAllocationFileResource(this, fileResource, displayName) {Description = description};
            GrantAllocationFileResources.Add(grantAllocationFileResource);
        }

        public void DeleteFullAndChildless(DatabaseEntities dbContext)
        {
            foreach (var x in GrantAllocationFileResources.ToList())
            {
                x.DeleteFullAndChildless(dbContext);
            }

            DeleteChildren(dbContext);
            Delete(dbContext);
        }
    }
}
