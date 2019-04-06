/*-----------------------------------------------------------------------
<copyright file="ProjectGrantAllocationExpenditureControllerTest.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
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
                return new List<int>();

            var beginCalendarYear = projectGrantAllocationExpenditures.Min(x => x.CalendarYear);
            var endCalendarYear = projectGrantAllocationExpenditures.Max(x => x.CalendarYear);
            var rangeOfYears = FirmaDateUtilities.GetRangeOfYears(beginCalendarYear, endCalendarYear);
            return rangeOfYears;
        }

        [Test]
        public void SimpleProjectSumsCorrectly()
        {
            var project = TestFramework.TestProject.Create();

            var organization = TestFramework.TestOrganization.Create("test organization 1");
            var grant = TestFramework.TestGrant.Create(organization, "test grant 1");
            var grantAllocation = TestFramework.TestGrantAllocation.Create(grant, "test grant allocation 1");

            TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation, 2010, 100.00m);
            TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation, 2011, 100.00m);
            TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation, 2012, 100.00m);
            TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation, 2013, 100.00m);

            var projectGrantAllocationExpenditures = project.ProjectGrantAllocationExpenditures.ToList();
            var rangeOfYears = GetRangeOfYears(projectGrantAllocationExpenditures);
            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.BottommostOrganization.DisplayName,
                new List<string> {"test organization 1"}, x => x.GrantAllocation.BottommostOrganization.DisplayName, rangeOfYears);

            Assert.That(fullOrganizationTypeAndYearDictionary.Sum(x => x.Value.Sum(y => y.Value)), Is.EqualTo(400.00m));
        }


        [Test]
        public void MultipleProjectsSumCorrectly()
        {
            var projectGrantAllocationExpenditures = BuildExpenditures("org 1", "grant allocation 1", new Dictionary<int, decimal> {{2010, 100.0m}, {2011, 200.0m}, {2012, 300.0m}, {2013, 400.0m}});
            projectGrantAllocationExpenditures.AddAll(BuildExpenditures("org 2", "grant allocation 2", new Dictionary<int, decimal> {{2010, 100.0m}, {2011, 200.0m}, {2012, 300.0m}, {2013, 400.0m}}));

            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.BottommostOrganization.DisplayName,
                new List<string> {"org 1", "org 2"},
                x => x.GrantAllocation.BottommostOrganization.DisplayName,
                GetRangeOfYears(projectGrantAllocationExpenditures));

            Assert.That(fullOrganizationTypeAndYearDictionary.Sum(x => x.Value.Sum(y => y.Value)), Is.EqualTo(2000.0m));
        }

        [Test]
        public void DictionaryBuildsCorrectly()
        {
            var projectGrantAllocationExpenditures = BuildExpenditures("org 1", "grant allocation 1", new Dictionary<int, decimal> 
            {{2010, 100.0m}, {2011, 200.0m}, {2012, 300.0m}, {2013, 400.0m}});

            projectGrantAllocationExpenditures.AddAll(BuildExpenditures("org 2", "grant allocation 2", new Dictionary<int, decimal> 
            {{2010, 100.0m}, {2011, 200.0m}, {2012, 300.0m}, {2013, 400.0m}}));

            var beginCalendarYear = projectGrantAllocationExpenditures.Min(x => x.CalendarYear);
            var endCalendarYear = projectGrantAllocationExpenditures.Max(x => x.CalendarYear);
            var rangeOfYears = FirmaDateUtilities.GetRangeOfYears(beginCalendarYear, endCalendarYear);

            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.BottommostOrganization.DisplayName,
                                                                                                                         new List<string> {"org 1", "org 2"}, 
                                                                                                                         x=> x.GrantAllocation.BottommostOrganization.DisplayName, 
                                                                                                                         rangeOfYears);

            Assert.That(fullOrganizationTypeAndYearDictionary["org 1"][2012], Is.EqualTo(300.00m));
        }

        [Test]
        public void YearSumsCorrectly()
        {
            var projectGrantAllocationExpenditures = BuildExpenditures("org 1", "grant allocation 1", new Dictionary<int, decimal> 
            {{2010, 100.0m}, {2011, 200.0m}, {2012, 300.0m}, {2013, 400.0m}});

            projectGrantAllocationExpenditures.AddAll(BuildExpenditures("org 2", "grant allocation 2", new Dictionary<int, decimal> 
            {{2010, 100.0m}, {2011, 200.0m}, {2012, 300.0m}, {2013, 400.0m}}));

            var beginCalendarYear = projectGrantAllocationExpenditures.Min(x => x.CalendarYear);
            var endCalendarYear = projectGrantAllocationExpenditures.Max(x => x.CalendarYear);
            var rangeOfYears = FirmaDateUtilities.GetRangeOfYears(beginCalendarYear, endCalendarYear);

            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.BottommostOrganization.DisplayName,
                new List<string> { "org 1", "org 2" }, x => x.GrantAllocation.BottommostOrganization.DisplayName, rangeOfYears);

            Assert.That(fullOrganizationTypeAndYearDictionary.Sum(x => x.Value[2012]), Is.EqualTo(600.0m));
        }

        [Test]
        public void GroupByOrgSumsCorrectly()
        {
            var projectGrantAllocationExpenditures = BuildExpenditures("org 1", "grant allocation 1", new Dictionary<int, decimal> { { 2010, 100.0m }, { 2011, 200.0m }, { 2012, 300.0m }, { 2013, 400.0m } });

            projectGrantAllocationExpenditures.AddAll(BuildExpenditures("org 2", "grant allocation 2", new Dictionary<int, decimal> { { 2010, 100.0m }, { 2011, 200.0m }, { 2012, 300.0m }, { 2013, 400.0m } }));

            var beginCalendarYear = projectGrantAllocationExpenditures.Min(x => x.CalendarYear);
            var endCalendarYear = projectGrantAllocationExpenditures.Max(x => x.CalendarYear);
            var rangeOfYears = FirmaDateUtilities.GetRangeOfYears(beginCalendarYear, endCalendarYear);

            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.BottommostOrganization.DisplayName,
                new List<string> { "org 1", "org 2" }, x => x.GrantAllocation.BottommostOrganization.DisplayName, rangeOfYears);

            Assert.That(fullOrganizationTypeAndYearDictionary["org 1"].Sum(x => x.Value), Is.EqualTo(1000.0m));
        }

        [Test]
        public void GroupByGrantAllocationSumsCorrectly()
        {
            var projectGrantAllocationExpenditures = BuildExpenditures("org 1", "grant allocation 1", new Dictionary<int, decimal> { { 2010, 100.0m }, { 2011, 100.0m } });
            projectGrantAllocationExpenditures.AddAll(BuildExpenditures("org 2", "grant allocation 2", new Dictionary<int, decimal> { { 2010, 200.0m }, { 2011, 200.0m } }));
            projectGrantAllocationExpenditures.AddAll(BuildExpenditures("org 3", "grant allocation 2", new Dictionary<int, decimal> { { 2010, 200.0m }, { 2011, 200.0m } }));

            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.GrantAllocationName,
                new List<string> { "grant allocation 1", "grant allocation 2" }, x => x.GrantAllocation.BottommostOrganization.DisplayName, GetRangeOfYears(projectGrantAllocationExpenditures));

            Assert.That(fullOrganizationTypeAndYearDictionary["grant allocation 2"].Sum(x => x.Value), Is.EqualTo(800.0m));
        }


        [Test]
        public void ExcludedOrgSumsCorrectly()
        {
            var projectGrantAllocationExpenditures = BuildExpenditures("org 1", "grant allocation 1", new Dictionary<int, decimal> 
            { { 2010, 100.0m }, { 2011, 200.0m }, { 2012, 300.0m }, { 2013, 400.0m } });
            projectGrantAllocationExpenditures.AddAll(BuildExpenditures("org 2", "grant allocation 2", new Dictionary<int, decimal> 
            { { 2010, 100.0m }, { 2011, 200.0m }, { 2012, 300.0m }, { 2013, 400.0m } }));

            var fullOrganizationTypeAndYearDictionary = projectGrantAllocationExpenditures.GetFullCategoryYearDictionary(x => x.GrantAllocation.BottommostOrganization.DisplayName,
                new List<string> { "org 1" }, x => x.GrantAllocation.BottommostOrganization.DisplayName, GetRangeOfYears(projectGrantAllocationExpenditures));

            Assert.That(fullOrganizationTypeAndYearDictionary.ContainsKey("org 2"), Is.False);
            Assert.That(fullOrganizationTypeAndYearDictionary.Sum(x => x.Value.Sum(y => y.Value)), Is.EqualTo(1000.0m));
        }


        private static List<ProjectGrantAllocationExpenditure> BuildExpenditures(string organizationName, string grantAllocationName, Dictionary<int, decimal> yearAndExpenditureDictionary)
        {
            var project = TestFramework.TestProject.Create();
            var organization = TestFramework.TestOrganization.Create(organizationName);
            var grantAllocation = TestFramework.TestGrantAllocation.CreateWithoutChangingName(grantAllocationName, organization);

            return yearAndExpenditureDictionary.Select(x => TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation, x.Key, x.Value)).ToList();

        }

    }
}
