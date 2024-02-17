/*-----------------------------------------------------------------------
<copyright file="EditCrosswalkValuesViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web.Mvc;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Program
{
    public class EditCrosswalkValuesViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> ProjectTypeSelectListItems { get; }
        public IEnumerable<SelectListItem> ProjectStageSelectListItems { get; }

        public IEnumerable<SelectListItem> TreatmentTypeSelectListItems { get; }
        public IEnumerable<SelectListItem> TreatmentDetailedActivityTypeSelectListItems { get; }

        public int ProjectTypeFieldDefinitionID { get; }
        public int ProjectStageFieldDefinitionID { get; }




        public EditCrosswalkValuesViewData(IEnumerable<SelectListItem> projectTypeSelectListItems, IEnumerable<SelectListItem> projectStageSelectListItems, IEnumerable<SelectListItem> treatmentTypeSelectListItems, IEnumerable<SelectListItem> treatmentDetailedActivityTypeSelectListItems)
        {
            ProjectStageSelectListItems = projectStageSelectListItems;
            ProjectTypeSelectListItems = projectTypeSelectListItems;
            TreatmentTypeSelectListItems = treatmentTypeSelectListItems;
            TreatmentDetailedActivityTypeSelectListItems = treatmentDetailedActivityTypeSelectListItems;
            ProjectTypeFieldDefinitionID = Models.FieldDefinition.ProjectType.FieldDefinitionID;
            ProjectStageFieldDefinitionID = Models.FieldDefinition.ProjectStage.FieldDefinitionID;
        }
    }
}
