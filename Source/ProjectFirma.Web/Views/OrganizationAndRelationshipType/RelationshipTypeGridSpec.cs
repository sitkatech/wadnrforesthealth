﻿/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.OrganizationAndRelationshipType
{
    public class RelationshipTypeGridSpec : GridSpec<RelationshipType>
    {
        public RelationshipTypeGridSpec(bool hasManagePermissions, List<OrganizationType> allOrganizationTypes)
        {
            var basicsColumnGroupCount = 5;

            if (hasManagePermissions)
            {
                Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.DeleteUrl, true, x.CanDelete(), true), 30, AgGridColumnFilterType.None);
                Add("Edit", a => AgGridHtmlHelpers.MakeLtInfoEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(SitkaRoute<OrganizationAndRelationshipTypeController>.BuildUrlFromExpression(t => t.EditRelationshipType(a)),
                        $"Edit {Models.FieldDefinition.ProjectRelationshipType.GetFieldDefinitionLabel()} \"{a.RelationshipTypeName}\"")),
                    30, AgGridColumnFilterType.None);
                basicsColumnGroupCount += 2;
            }

            Add($"{Models.FieldDefinition.ProjectRelationshipType.GetFieldDefinitionLabel()} Name", a => a.RelationshipTypeName, 240);
            Add($"Can Steward {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}?", a => a.CanStewardProjects.ToCheckboxImageOrEmptyForGrid(), 90);
            Add("Serves as Primary Contact?", a => a.IsPrimaryContact.ToCheckboxImageOrEmptyForGrid(), 90);
            Add($"Must be Related to a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Once?", a => a.CanOnlyBeRelatedOnceToAProject.ToCheckboxImageOrEmptyForGrid(), 90);
            Add($"Show on {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Fact Sheet", a => a.ShowOnFactSheet.ToCheckboxImageOrEmptyForGrid(), 90);

            foreach (var organizationType in allOrganizationTypes)
            {
                Add(organizationType.OrganizationTypeName, a => a.IsAssociatedWithOrganiztionType(organizationType).ToCheckboxImageOrEmptyForGrid(), 90);
            }

            GroupingHeader =
                BuildGroupingHeader(new ColumnHeaderGroupingList
                {
                    {"", basicsColumnGroupCount},
                    {$"Applicable to the following {Models.FieldDefinition.OrganizationType.GetFieldDefinitionLabelPluralized()}:", allOrganizationTypes.Count}                    
                });

        }
    }
}
