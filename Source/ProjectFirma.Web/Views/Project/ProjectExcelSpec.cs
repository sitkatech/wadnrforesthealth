/*-----------------------------------------------------------------------
<copyright file="ProjectExcelSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System;
using System.Linq;
using ProjectFirma.Web.Models;
using LtInfo.Common.ExcelWorkbookUtilities;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.Project
{
    public class ProjectExcelSpec : ExcelWorksheetSpec<Models.Project>
    {
        public ProjectExcelSpec()
        {
            
            AddColumn(Models.FieldDefinition.ProjectName.GetFieldDefinitionLabel(), x => x.ProjectName);
            AddColumn(Models.FieldDefinition.FhtProjectNumber.GetFieldDefinitionLabel(), x => x.FhtProjectNumber);
            AddColumn("Program Identifier", x => x.ProjectGisIdentifier);
            AddColumn(Models.FieldDefinition.Program.GetFieldDefinitionLabel(), x => string.Join(",", x.ProjectPrograms.Select(y => y.Program.ProgramNameDisplay)));

            var organizationFieldDefinitionLabelSingle = Models.FieldDefinition.Organization.GetFieldDefinitionLabel();
            var organizationFieldDefinitionLabelPluralized = Models.FieldDefinition.Organization.GetFieldDefinitionLabelPluralized();

            var allProjectGrantAllocationExpenditures =
                HttpRequestStorage.DatabaseEntities.ProjectGrantAllocationExpenditures.ToList();
            var projectGrantAllocationExpenditureDict = allProjectGrantAllocationExpenditures.GroupBy(x => x.ProjectID).ToDictionary(x => x.Key, y => y.ToList());

            AddColumn($"Non-Lead Implementing {organizationFieldDefinitionLabelPluralized}",
                x =>
                {
                    
                    return string.Join(",",
                        x.GetAssociatedOrganizations(organizationFieldDefinitionLabelSingle,
                            organizationFieldDefinitionLabelPluralized, projectGrantAllocationExpenditureDict)
                        .Select(pio => pio.Organization.DisplayName));
                });
            AddColumn(Models.FieldDefinition.ProjectStage.GetFieldDefinitionLabel(), x => x.ProjectStage.ProjectStageDisplayName);
            MultiTenantHelpers.GetClassificationSystems().ForEach(y =>
                {
                    AddColumn(y.ClassificationSystemNamePluralized, x => string.Join(",", x.ProjectClassifications.Where(z => z.Classification.ClassificationSystem == y).Select(tc => tc.Classification.DisplayName)));
                });
            AddColumn(Models.FieldDefinition.PriorityLandscape.GetFieldDefinitionLabelPluralized(), x => string.Join(",", x.GetProjectPriorityLandscapes().Select(y => y.PriorityLandscapeName)));
            AddColumn(Models.FieldDefinition.DNRUplandRegion.GetFieldDefinitionLabel(), x => string.Join(",", x.GetProjectRegions().Select(y => y.DNRUplandRegionName)));
            AddColumn(Models.FieldDefinition.County.GetFieldDefinitionLabel(), x => string.Join(",", x.GetProjectCounties().Select(y => y.CountyName)));
            AddColumn(Models.FieldDefinition.FocusArea.GetFieldDefinitionLabel(), x => x.FocusAreaID.HasValue ? x.FocusArea.FocusAreaName : string.Empty);
            AddColumn(Models.FieldDefinition.ProjectInitiationDate.GetFieldDefinitionLabel(), x => x.GetImplementationStartYear());
            AddColumn(Models.FieldDefinition.CompletionDate.GetFieldDefinitionLabel(), x => x.GetCompletionYear());
            AddColumn(Models.FieldDefinition.ProjectDescription.GetFieldDefinitionLabel(), x => x.ProjectDescription);
            AddColumn(Models.FieldDefinition.EstimatedTotalCost.GetFieldDefinitionLabel(), x => x.EstimatedTotalCost);
            AddColumn(Models.FieldDefinition.ProjectGrantAllocationRequestTotalAmount.GetFieldDefinitionLabel(), x => x.GetTotalFunding());
            //AddColumn("State", a => a.ProjectLocationStateProvince);
            AddColumn($"{Models.FieldDefinition.ProjectLocation.GetFieldDefinitionLabel()} Notes", a => a.ProjectLocationNotes);
        }
    }

    public class ProjectDescriptionExcelSpec : ExcelWorksheetSpec<Models.Project>
    {
        public ProjectDescriptionExcelSpec()
        {
            AddColumn($"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} ID", x => x.ProjectID);
            AddColumn($"{Models.FieldDefinition.ProjectName.GetFieldDefinitionLabel()}", x => x.ProjectName);
            AddColumn("Description", x => x.ProjectDescription);
        }
    }

    public class ProjectImplementingOrganizationOrProjectFundingOrganizationExcelSpec : ExcelWorksheetSpec<ProjectOrganizationRelationship>
    {
        public ProjectImplementingOrganizationOrProjectFundingOrganizationExcelSpec()
        {
            AddColumn($"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} ID", x => x.Project.ProjectID);
            AddColumn($"{Models.FieldDefinition.ProjectName.GetFieldDefinitionLabel()}", x => x.Project.ProjectName);
            AddColumn($"{Models.FieldDefinition.Organization.GetFieldDefinitionLabel()} ID", x => x.Organization.OrganizationID);
            AddColumn($"{Models.FieldDefinition.Organization.GetFieldDefinitionLabel()} Name", x => x.Organization.OrganizationName);
            AddColumn($"{Models.FieldDefinition.OrganizationPrimaryContact.GetFieldDefinitionLabel()} for {Models.FieldDefinition.Organization.GetFieldDefinitionLabel()}", x => x.Organization.PrimaryContactPersonWithOrgAsString);
            AddColumn(Models.FieldDefinition.OrganizationType.GetFieldDefinitionLabel(), x => x.Organization.OrganizationType?.OrganizationTypeName);
            AddColumn($"{Models.FieldDefinition.Organization.GetFieldDefinitionLabel()} Relationship To {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}", x => x.RelationshipType.RelationshipTypeName);
        }
    }

    public class ProjectNoteExcelSpec : ExcelWorksheetSpec<ProjectNote>
    {
        public ProjectNoteExcelSpec()
        {
            AddColumn($"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} ID", x => x.Project.ProjectID);
            AddColumn($"{Models.FieldDefinition.ProjectName.GetFieldDefinitionLabel()}", x => x.Project.ProjectName);
            AddColumn($"{Models.FieldDefinition.ProjectNote.GetFieldDefinitionLabel()}", x => x.Note);
            AddColumn("Create Person", x => x.CreatePersonName);
            AddColumn("Create Date", x => x.CreateDate);
        }
    }

    public class PerformanceMeasureExpectedExcelSpec : ExcelWorksheetSpec<PerformanceMeasureExpected>
    {
        public PerformanceMeasureExpectedExcelSpec()
        {
            AddColumn($"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} ID", x => x.Project.ProjectID);
            AddColumn($"{Models.FieldDefinition.ProjectName.GetFieldDefinitionLabel()}", x => x.Project.ProjectName);
            AddColumn(MultiTenantHelpers.GetPerformanceMeasureName() + " ID", x => x.PerformanceMeasureID);
            AddColumn(MultiTenantHelpers.GetPerformanceMeasureName(), x => x.PerformanceMeasure.PerformanceMeasureDisplayName);
            AddColumn($"{Models.FieldDefinition.ExpectedValue.GetFieldDefinitionLabel()}", x => x.ExpectedValue);
        }
    }

    public class PerformanceMeasureActualExcelSpec : ExcelWorksheetSpec<PerformanceMeasureReportedValue>
    {
        public PerformanceMeasureActualExcelSpec()
        {
            AddColumn($"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} ID", x => x.Project.ProjectID);
            AddColumn($"{Models.FieldDefinition.ProjectName.GetFieldDefinitionLabel()}", x => x.Project.ProjectName);
            AddColumn(MultiTenantHelpers.GetPerformanceMeasureName() + " ID", x => x.PerformanceMeasureID);
            AddColumn(MultiTenantHelpers.GetPerformanceMeasureName(), x => x.PerformanceMeasure.PerformanceMeasureDisplayName);
            AddColumn("Calendar Year", x => x.CalendarYear);
            AddColumn($"{Models.FieldDefinition.PerformanceMeasureSubcategory.GetFieldDefinitionLabel()} 1 Name", x => x.PerformanceMeasureSubcategoryOptions.Count > 1 ? x.PerformanceMeasureSubcategoryOptions[0].PerformanceMeasureSubcategory.PerformanceMeasureSubcategoryDisplayName : string.Empty);
            AddColumn($"{Models.FieldDefinition.PerformanceMeasureSubcategory.GetFieldDefinitionLabel()} 1 Option", x => x.PerformanceMeasureSubcategoryOptions.Count > 1 ? x.PerformanceMeasureSubcategoryOptions[0].PerformanceMeasureSubcategoryOption.PerformanceMeasureSubcategoryOptionName : string.Empty);
            AddColumn($"{Models.FieldDefinition.PerformanceMeasureSubcategory.GetFieldDefinitionLabel()} 2 Name", x => x.PerformanceMeasureSubcategoryOptions.Count > 2 ? x.PerformanceMeasureSubcategoryOptions[1].PerformanceMeasureSubcategory.PerformanceMeasureSubcategoryDisplayName : string.Empty);
            AddColumn($"{Models.FieldDefinition.PerformanceMeasureSubcategory.GetFieldDefinitionLabel()} 2 Option", x => x.PerformanceMeasureSubcategoryOptions.Count > 2 ? x.PerformanceMeasureSubcategoryOptions[1].PerformanceMeasureSubcategoryOption.PerformanceMeasureSubcategoryOptionName : string.Empty);
            AddColumn($"{Models.FieldDefinition.PerformanceMeasureSubcategory.GetFieldDefinitionLabel()} 3 Name", x => x.PerformanceMeasureSubcategoryOptions.Count > 3 ? x.PerformanceMeasureSubcategoryOptions[2].PerformanceMeasureSubcategory.PerformanceMeasureSubcategoryDisplayName : string.Empty);
            AddColumn($"{Models.FieldDefinition.PerformanceMeasureSubcategory.GetFieldDefinitionLabel()} 3 Option", x => x.PerformanceMeasureSubcategoryOptions.Count > 3 ? x.PerformanceMeasureSubcategoryOptions[2].PerformanceMeasureSubcategoryOption.PerformanceMeasureSubcategoryOptionName : string.Empty);
            AddColumn($"{Models.FieldDefinition.PerformanceMeasureSubcategory.GetFieldDefinitionLabel()} 4 Name", x => x.PerformanceMeasureSubcategoryOptions.Count > 4 ? x.PerformanceMeasureSubcategoryOptions[3].PerformanceMeasureSubcategory.PerformanceMeasureSubcategoryDisplayName : string.Empty);
            AddColumn($"{Models.FieldDefinition.PerformanceMeasureSubcategory.GetFieldDefinitionLabel()} 4 Option", x => x.PerformanceMeasureSubcategoryOptions.Count > 4 ? x.PerformanceMeasureSubcategoryOptions[3].PerformanceMeasureSubcategoryOption.PerformanceMeasureSubcategoryOptionName : string.Empty);
            AddColumn($"{Models.FieldDefinition.ReportedValue.GetFieldDefinitionLabel()}", x => x.ReportedValue);
        }
    }

    public class ProjectGrantAllocationExpenditureExcelSpec : ExcelWorksheetSpec<Models.ProjectGrantAllocationExpenditure>
    {
        public ProjectGrantAllocationExpenditureExcelSpec()
        {
            AddColumn($"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} ID", x => x.Project.ProjectID);
            AddColumn($"{Models.FieldDefinition.ProjectName.GetFieldDefinitionLabel()}", x => x.Project.ProjectName);
            AddColumn($"{Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()}", x => x.GrantAllocation.GrantAllocationName);
            AddColumn($"Funding {Models.FieldDefinition.Organization.GetFieldDefinitionLabel()}", x => x.GrantAllocation.BottommostOrganization.OrganizationName);
            AddColumn(Models.FieldDefinition.OrganizationType.GetFieldDefinitionLabel(), x => x.GrantAllocation.BottommostOrganization.OrganizationType?.OrganizationTypeName);
            AddColumn("Calendar Year", x => x.CalendarYear);
            AddColumn("Expenditure Amount", x => x.ExpenditureAmount);
        }
    }

    public class ProjectClassificationExcelSpec : ExcelWorksheetSpec<ProjectClassification>
    {
        public ProjectClassificationExcelSpec()
        {
            AddColumn($"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} ID", x => x.Project.ProjectID);
            AddColumn($"{Models.FieldDefinition.ProjectName.GetFieldDefinitionLabel()}", x => x.Project.ProjectName);
            AddColumn(Models.FieldDefinition.Classification.GetFieldDefinitionLabel(), x => x.Classification.DisplayName);
        }
    }
}
