/*-----------------------------------------------------------------------
<copyright file="ProjectGrantAllocationExpenditureControllerTest.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using ApprovalUtilities.Utilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.UnitTestCommon;
using NUnit.Framework;

namespace ProjectFirma.Web.Controllers
{
    [TestFixture]
    public class ProjectGrantAllocationExpenditureControllerTest
    {
        [Test]
        public void ProjectWithNoExpendituresShouldReturnEmptyDictionary()
        {
            var project = TestFramework.TestProject.Create();
            var projectGrantAllocationExpenditures = project.ProjectGrantAllocationExpenditures.ToList();
            var rangeOfYears = GetRangeOfYears(projectGrantAllocationExpenditures);

            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.BottommostOrganization.DisplayName,
                new List<string>(), x => x.GrantAllocation.BottommostOrganization.DisplayName, rangeOfYears);

            Assert.That(fullOrganizationTypeAndYearDictionary, Is.Empty);
        }

        private static List<int> GetRangeOfYears(List<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures)
        {
            if (!projectGrantAllocationExpenditures.Any())
            {
                return new List<int>();
            }

            var beginCalendarYear = projectGrantAllocationExpenditures.Min(x => x.CalendarYear);
            var endCalendarYear = projectGrantAllocationExpenditures.Max(x => x.CalendarYear);
            var rangeOfYears = FirmaDateUtilities.GetRangeOfYears(beginCalendarYear, endCalendarYear);
            return rangeOfYears;
        }

        [Test]
        public void SimpleProjectSumsCorrectly()
        {
            var project = TestFramework.TestProject.Create();

            var org1 = TestFramework.TestOrganization.Create();

            var grantModification = TestFramework.TestGrantModification.Create(org1);
            var grantAllocation = TestFramework.TestGrantAllocation.Create(grantModification, "test grant allocation 1");

            TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation, 2010, 100.00m);
            TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation, 2011, 100.00m);
            TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation, 2012, 100.00m);
            TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation, 2013, 100.00m);

            var projectGrantAllocationExpenditures = project.ProjectGrantAllocationExpenditures.ToList();
            var rangeOfYears = GetRangeOfYears(projectGrantAllocationExpenditures);
            var filterValues = new List<string> {org1.DisplayName};
            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.BottommostOrganization.DisplayName,
                filterValues, x => x.GrantAllocation.BottommostOrganization.DisplayName, rangeOfYears);

            Assert.That(fullOrganizationTypeAndYearDictionary.Sum(x => x.Value.Sum(y => y.Value)), Is.EqualTo(400.00m));
        }


        [Test]
        public void MultipleProjectsSumCorrectly()
        {
            var org1 = TestFramework.TestOrganization.Create();
            var projectGrantAllocationExpenditures = BuildExpenditures(org1, "grant allocation 1", new Dictionary<int, decimal> {{2010, 100.0m}, {2011, 200.0m}, {2012, 300.0m}, {2013, 400.0m}});
            var org2 = TestFramework.TestOrganization.Create();
            projectGrantAllocationExpenditures.AddAll(BuildExpenditures(org2, "grant allocation 2", new Dictionary<int, decimal> {{2010, 100.0m}, {2011, 200.0m}, {2012, 300.0m}, {2013, 400.0m}}));

            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.BottommostOrganization.DisplayName,
                new List<string> { org1.DisplayName, org2.DisplayName },
                x => x.GrantAllocation.BottommostOrganization.DisplayName,
                GetRangeOfYears(projectGrantAllocationExpenditures));

            Assert.That(fullOrganizationTypeAndYearDictionary.Sum(x => x.Value.Sum(y => y.Value)), Is.EqualTo(2000.0m));
        }

        [Test]
        public void DictionaryBuildsCorrectly()
        {
            var org1 = TestFramework.TestOrganization.Create();
            var projectGrantAllocationExpenditures = BuildExpenditures(org1, "grant allocation 1", new Dictionary<int, decimal> 
                {{2010, 100.0m}, {2011, 200.0m}, {2012, 300.0m}, {2013, 400.0m}});

            var org2 = TestFramework.TestOrganization.Create();
            projectGrantAllocationExpenditures.AddAll(BuildExpenditures(org2, "grant allocation 2", new Dictionary<int, decimal> 
                {{2010, 100.0m}, {2011, 200.0m}, {2012, 300.0m}, {2013, 400.0m}}));

            var beginCalendarYear = projectGrantAllocationExpenditures.Min(x => x.CalendarYear);
            var endCalendarYear = projectGrantAllocationExpenditures.Max(x => x.CalendarYear);
            var rangeOfYears = FirmaDateUtilities.GetRangeOfYears(beginCalendarYear, endCalendarYear);

            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.BottommostOrganization.DisplayName,
                                                                                                                         new List<string> {org1.DisplayName, org2.DisplayName }, 
                                                                                                                         x=> x.GrantAllocation.BottommostOrganization.DisplayName, 
                                                                                                                         rangeOfYears);

            Assert.That(fullOrganizationTypeAndYearDictionary[org1.DisplayName][2012], Is.EqualTo(300.00m));
        }

        [Test]
        public void YearSumsCorrectly()
        {
            var org1 = TestFramework.TestOrganization.Create();
            var projectGrantAllocationExpenditures = BuildExpenditures(org1, "grant allocation 1", new Dictionary<int, decimal> 
                {{2010, 100.0m}, {2011, 200.0m}, {2012, 300.0m}, {2013, 400.0m}});

            var org2 = TestFramework.TestOrganization.Create();
            projectGrantAllocationExpenditures.AddAll(BuildExpenditures(org2, "grant allocation 2", new Dictionary<int, decimal> 
                {{2010, 100.0m}, {2011, 200.0m}, {2012, 300.0m}, {2013, 400.0m}}));

            var beginCalendarYear = projectGrantAllocationExpenditures.Min(x => x.CalendarYear);
            var endCalendarYear = projectGrantAllocationExpenditures.Max(x => x.CalendarYear);
            var rangeOfYears = FirmaDateUtilities.GetRangeOfYears(beginCalendarYear, endCalendarYear);

            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.BottommostOrganization.DisplayName,
                new List<string> { org1.DisplayName, org2.DisplayName }, x => x.GrantAllocation.BottommostOrganization.DisplayName, rangeOfYears);

            Assert.That(fullOrganizationTypeAndYearDictionary.Sum(x => x.Value[2012]), Is.EqualTo(600.0m));
        }

        [Test]
        public void GroupByOrgSumsCorrectly()
        {
            var org1 = TestFramework.TestOrganization.Create();
            var projectGrantAllocationExpenditures = BuildExpenditures(org1, "grant allocation 1", new Dictionary<int, decimal> { { 2010, 100.0m }, { 2011, 200.0m }, { 2012, 300.0m }, { 2013, 400.0m } });

            var org2 = TestFramework.TestOrganization.Create();
            projectGrantAllocationExpenditures.AddAll(BuildExpenditures(org2, "grant allocation 2", new Dictionary<int, decimal> { { 2010, 100.0m }, { 2011, 200.0m }, { 2012, 300.0m }, { 2013, 400.0m } }));

            var beginCalendarYear = projectGrantAllocationExpenditures.Min(x => x.CalendarYear);
            var endCalendarYear = projectGrantAllocationExpenditures.Max(x => x.CalendarYear);
            var rangeOfYears = FirmaDateUtilities.GetRangeOfYears(beginCalendarYear, endCalendarYear);

            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.BottommostOrganization.DisplayName,
                new List<string> { org1.DisplayName, org2.DisplayName }, x => x.GrantAllocation.BottommostOrganization.DisplayName, rangeOfYears);

            Assert.That(fullOrganizationTypeAndYearDictionary[org1.DisplayName].Sum(x => x.Value), Is.EqualTo(1000.0m));
        }

        [Test]
        public void GroupByGrantAllocationSumsCorrectly()
        {
            var projectGrantAllocationExpenditures = BuildExpenditures(TestFramework.TestOrganization.Create(), "grant allocation 1", new Dictionary<int, decimal> { { 2010, 100.0m }, { 2011, 100.0m } });
            projectGrantAllocationExpenditures.AddAll(BuildExpenditures(TestFramework.TestOrganization.Create(), "grant allocation 2", new Dictionary<int, decimal> { { 2010, 200.0m }, { 2011, 200.0m } }));
            projectGrantAllocationExpenditures.AddAll(BuildExpenditures(TestFramework.TestOrganization.Create(), "grant allocation 2", new Dictionary<int, decimal> { { 2010, 200.0m }, { 2011, 200.0m } }));

            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.GrantAllocationName,
                new List<string> { "grant allocation 1", "grant allocation 2" }, x => x.GrantAllocation.BottommostOrganization.DisplayName, GetRangeOfYears(projectGrantAllocationExpenditures));

            Assert.That(fullOrganizationTypeAndYearDictionary["grant allocation 2"].Sum(x => x.Value), Is.EqualTo(800.0m));
        }


        [Test]
        public void ExcludedOrgSumsCorrectly()
        {
            var org1 = TestFramework.TestOrganization.Create();
            var projectGrantAllocationExpenditures = BuildExpenditures(org1, "grant allocation 1", new Dictionary<int, decimal> 
                { { 2010, 100.0m }, { 2011, 200.0m }, { 2012, 300.0m }, { 2013, 400.0m } });
            var org2 = TestFramework.TestOrganization.Create();
            projectGrantAllocationExpenditures.AddAll(BuildExpenditures(org2, "grant allocation 2", new Dictionary<int, decimal> 
                { { 2010, 100.0m }, { 2011, 200.0m }, { 2012, 300.0m }, { 2013, 400.0m } }));

            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.BottommostOrganization.DisplayName,
                new List<string> { org1.DisplayName }, x => x.GrantAllocation.BottommostOrganization.DisplayName, GetRangeOfYears(projectGrantAllocationExpenditures));

            Assert.That(fullOrganizationTypeAndYearDictionary.ContainsKey(org2.DisplayName), Is.False);
            Assert.That(fullOrganizationTypeAndYearDictionary.Sum(x => x.Value.Sum(y => y.Value)), Is.EqualTo(1000.0m));
        }


        private static List<ProjectGrantAllocationExpenditure> BuildExpenditures(Organization organization,
            string grantAllocationName, Dictionary<int, decimal> yearAndExpenditureDictionary)
        {
            var project = TestFramework.TestProject.Create();
            var grantAllocation = TestFramework.TestGrantAllocation.CreateWithoutChangingName(grantAllocationName, organization);

            return yearAndExpenditureDictionary.Select(x => TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation, x.Key, x.Value)).ToList();

        }

    }
}
