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
        public IEnumerable<SelectListItem> ProjectStageSelectListItems { get; }
        public IEnumerable<SelectListItem> LeadImplementerSelectListItems { get; }

        public IEnumerable<SelectListItem> TreatmentTypeSelectListItems { get; }
        public IEnumerable<SelectListItem> TreatmentDetailedActivityTypeSelectListItems { get; }

        //These FieldDefinitionIDs are used by the angular controller
        public int ProjectStageFieldDefinitionID { get; }
        public int LeadImplementerFieldDefinitionID { get; }
        public int TreatmentTypeFieldDefinitionID { get; }
        public int TreatmentDetailedActivityTypeFieldDefinitionID { get; }

        public bool ImportIsFlattened { get; }




        public EditCrosswalkValuesViewData(IEnumerable<SelectListItem> leadImplementerSelectListItems, IEnumerable<SelectListItem> projectStageSelectListItems, IEnumerable<SelectListItem> treatmentTypeSelectListItems, IEnumerable<SelectListItem> treatmentDetailedActivityTypeSelectListItems, bool importIsFlattened)
        {
            ProjectStageSelectListItems = projectStageSelectListItems;
            LeadImplementerSelectListItems = leadImplementerSelectListItems;
            TreatmentTypeSelectListItems = treatmentTypeSelectListItems;
            TreatmentDetailedActivityTypeSelectListItems = treatmentDetailedActivityTypeSelectListItems;

            ProjectStageFieldDefinitionID = Models.FieldDefinition.ProjectStage.FieldDefinitionID;
            LeadImplementerFieldDefinitionID = Models.FieldDefinition.LeadImplementerOrganization.FieldDefinitionID;
            TreatmentTypeFieldDefinitionID = Models.FieldDefinition.TreatmentType.FieldDefinitionID;
            TreatmentDetailedActivityTypeFieldDefinitionID = Models.FieldDefinition.TreatmentDetailedActivityType.FieldDefinitionID;
            ImportIsFlattened = importIsFlattened;
        }
    }
}
