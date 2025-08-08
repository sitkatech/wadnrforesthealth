using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Views.Shared.FileResourceControls;

namespace ProjectFirma.Web.Models
{
    public partial class FundSourceAllocation : IAuditableEntity, ICanUploadNewFiles
    {
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string FederalFundCodeDisplay => FederalFundCodeID.HasValue ? FederalFundCode.FederalFundCodeAbbrev : string.Empty;

        public string FundSourceNumberAndFundSourceAllocationDisplayName => $"{FundSource.FundSourceNumber} {FundSourceAllocationName}";

        public string AllocationAmountCurrencyDisplay => $"{AllocationAmount.ToStringCurrency()}";

        public string FundSourceNumberAndFundSourceAllocationWithAllocationAmountDisplay => $"{FundSourceNumberAndFundSourceAllocationDisplayName} ({AllocationAmountCurrencyDisplay})";

        public HtmlString FundSourceNumberAndFundSourceAllocationDisplayNameAsUrl => UrlTemplate.MakeHrefString(SummaryUrl, FundSourceNumberAndFundSourceAllocationDisplayName);

        // ReSharper disable once InconsistentNaming
        public int RegionIDDisplay => DNRUplandRegionID.HasValue ? DNRUplandRegion.DNRUplandRegionID : -1;
        public string RegionNameDisplay => DNRUplandRegion != null ? DNRUplandRegion.DNRUplandRegionName : string.Empty;
        public string DivisionNameDisplay => Division != null ? Division.DivisionDisplayName : string.Empty;

        public string DisplayName => this.FundSourceAllocationName;
        public HtmlString DisplayNameAsUrl => UrlTemplate.MakeHrefString(SummaryUrl, DisplayName);

        public string SummaryUrl
        {
            get { return SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(x => x.FundSourceAllocationDetail(FundSourceAllocationID)); }
        }


        public string AuditDescriptionString
        {
            get { return FundSourceAllocationName; }
        }

        public List<int> ProgramManagerPersonIDs
        {
            get { return ProgramManagerPersons.Select(p => p.PersonID).ToList(); }
        }

        public List<Person> ProgramManagerPersons
        {
            get { return FundSourceAllocationProgramManagers.Select(gapm => gapm.Person).ToList(); }
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
            if (LikelyToUse == null)
            {
                return new HtmlString("N/A");
            }
            else if (LikelyToUse == false)
            {
                return new HtmlString("Contractual Only");
            }
            var likelyToUseUrlsList = this.FundSourceAllocationLikelyPeople.Select(ltup => ltup.Person.GetFullNameFirstLastAsUrl()).ToList();
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
        /// Allocation is the percentage based on pay amount total from "expected funding by project" section from the fundSource allocation detail page divided by the contractual amount from the "fundSource allocation budget line items" section on the fundSource allocation detail page
        /// </summary>
        /// <returns></returns>
        public HtmlString GetAllocationStringForDnrUplandRegionGrid()
        {
            var expectedFundingByProject = ProjectFundSourceAllocationRequests.Sum(y => y.PayAmount);
            var budgetLineItem = FundSourceAllocationBudgetLineItems.Single(z => z.CostType == CostType.Contractual);

            var contractualAmount = budgetLineItem.FundSourceAllocationBudgetLineItemAmount;
            if (contractualAmount == 0)
            {
                return new HtmlString($"<div style=\"padding-right:30%;height: 94%;margin-left: -5px; width:130%;padding-top: 7px; background-color:{AllocationColor[150]}\">N/A - Cannot divide by 0</div>");
            }

            var allocationAmount = expectedFundingByProject / contractualAmount;

            return new HtmlString($"<div style=\"padding-right:30%;height: 94%;margin-left: -5px; width:130%;padding-top: 7px; background-color:{GetAllocationCssClass(allocationAmount)}\">{allocationAmount.ToStringPercent()}</div>");


        }

        public decimal GetOverallBalance()
        {
            var allocationCurrentBalance =
                this.GetTotalBudgetVsActualLineItem().BudgetMinusExpendituresFromDatamart;
            var indirect = FundSourceAllocationBudgetLineItems.Where(z => z.CostType == CostType.IndirectCosts)
                .Sum(z => z.FundSourceAllocationBudgetLineItemAmount);
            return allocationCurrentBalance - indirect;
        }

        public string ToLikelyToUsePeopleListDisplayForAgGrid()
        {
            var listOfHtmlLinkObjects = new List<HtmlLinkObject>();

            if (LikelyToUse == true)
            {
                var likelyToUse = FundSourceAllocationLikelyPeople.Select(x => x.Person != null ? new HtmlLinkObject(x.Person.FullNameFirstLast, x.Person.GetDetailUrl()) : new HtmlLinkObject(string.Empty, string.Empty)).ToList();
                if (likelyToUse.Any())
                {
                    return likelyToUse.ToJsonArrayForAgGrid();
                }
                listOfHtmlLinkObjects.Add(new HtmlLinkObject(string.Empty, string.Empty));
                return listOfHtmlLinkObjects.ToJsonArrayForAgGrid();
            }
            else if (LikelyToUse == null)
            {
                listOfHtmlLinkObjects.Add(new HtmlLinkObject("N/A", string.Empty));
                return listOfHtmlLinkObjects.ToJsonArrayForAgGrid();
            }

            listOfHtmlLinkObjects.Add(new HtmlLinkObject("Contractual Only", string.Empty));
            return listOfHtmlLinkObjects.ToJsonArrayForAgGrid();

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

        public static List<FundSourceAllocation> OrderFundSourceAllocationsByYearPrefixedFundSourceNumbersThenEverythingElse(List<FundSourceAllocation> fundSourceAllocations)
        {
            // Find all the FundSourceAllocations that have a proper year prefix ("2016-....")
            var allFundSourceAllocationsPrefixedWithFundSourceYear =
                fundSourceAllocations.Where(ga => GetFundSourceYearPrefixIfPresent(ga) != null).ToList();
            var allFundSourceAllocationIDSPrefixedWithFundSourceYear =
                allFundSourceAllocationsPrefixedWithFundSourceYear.Select(ga => ga.FundSourceAllocationID).ToList();

            // Start out showing properly prefixed year entries, with most recent on top
            List<Models.FundSourceAllocation> outgoingFundSourceAllocations = new List<Models.FundSourceAllocation>();
            outgoingFundSourceAllocations.AddRange(
                allFundSourceAllocationsPrefixedWithFundSourceYear.OrderByDescending(x => GetFundSourceYearPrefixIfPresent(x)));

            // Then show everything else, alpha sorted.
            var fundSourceAllocationsWithoutYears = fundSourceAllocations
                .Where(ga => !allFundSourceAllocationIDSPrefixedWithFundSourceYear.Contains(ga.FundSourceAllocationID)).ToList();
            outgoingFundSourceAllocations.AddRange(fundSourceAllocationsWithoutYears.OrderBy(x => x.FundSource.FundSourceNumber));
            return outgoingFundSourceAllocations;
        }

        private static string GetFundSourceYearPrefixIfPresent(FundSourceAllocation ga)
        {
            const string yearMatchPattern = @"^(?<year>[1-9][0-9][0-9][0-9])-";

            var fundSourceYearRegex = new Regex(yearMatchPattern);
            var fundSourceNumber = ga.FundSource.FundSourceNumber;
            if (string.IsNullOrEmpty(fundSourceNumber))
            {
                return null;
            }
            MatchCollection matches = fundSourceYearRegex.Matches(fundSourceNumber);
            if (matches.Count > 0)
            {
                var firstMatch = matches[0];
                // FundSource year prefix
                return firstMatch.Groups["year"].Value;
            }

            // No fundSource year prefix
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

                return this.FundSource.Organization;
            }
        }


        /// <summary>
        /// Stand-in for what used to be FundSourceAllocation.FixedLengthDisplayName
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
                return organizationShortNameIfAvailable.Length < 45 ? $"{FundSourceAllocationName.ToEllipsifiedString(45 - organizationShortNameIfAvailable.Length)} {organizationShortNameIfAvailable}" : $"{FundSourceAllocationName} {organizationShortNameIfAvailable}";
            }
        }

        public void AddNewFileResource(FileResource fileResource, string displayName, string description)
        {
            var fundSourceAllocationFileResource =
                new FundSourceAllocationFileResource(this, fileResource, displayName) {Description = description};
            FundSourceAllocationFileResources.Add(fundSourceAllocationFileResource);
        }

        public void DeleteFullAndChildless(DatabaseEntities dbContext)
        {
            foreach (var x in FundSourceAllocationFileResources.ToList())
            {
                x.DeleteFullAndChildless(dbContext);
            }

            DeleteChildren(dbContext);
            Delete(dbContext);
        }

    }
}
