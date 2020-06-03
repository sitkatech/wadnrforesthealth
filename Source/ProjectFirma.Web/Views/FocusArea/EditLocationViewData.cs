/*-----------------------------------------------------------------------
<copyright file="ProjectLocationDetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.FocusArea
{
    public class EditLocationViewData : FirmaViewData
    {
        public Models.FocusArea FocusArea { get; }
        public string EditBoundaryUrl { get; }
        public string ApproveGisUploadUrl { get; }
        public string FocusAreaDetailUrl { get; }

        public EditLocationViewData(Person currentPerson, Models.FocusArea focusArea) : base(currentPerson)
        {
            PageTitle = $"Edit {Models.FieldDefinition.FocusArea.GetFieldDefinitionLabel()} Location";

            FocusArea = focusArea;
            EditBoundaryUrl =
                SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.EditLocation(focusArea));
            ApproveGisUploadUrl =
                SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.ApproveUploadGis(focusArea));
            FocusAreaDetailUrl =
                SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.Detail(focusArea));
        }
    }
}
