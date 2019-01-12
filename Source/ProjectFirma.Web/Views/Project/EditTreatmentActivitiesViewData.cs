/*-----------------------------------------------------------------------
<copyright file="EditTreatmentActivitiesViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class EditTreatmentActivitiesViewData : FirmaViewData
    {
        public IEnumerable<SelectListItem> TreatmentActivityStatus { get; }

        public int ProjectID { get; }
        public string ProjectUrl { get; }

        public EditTreatmentActivitiesViewData(Models.Project project, Person currentPerson) : base(currentPerson)
        {
            ProjectID = project.ProjectID;
            ProjectUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.Detail(ProjectID));

            EntityName = Models.FieldDefinition.Project.GetFieldDefinitionLabel();
            PageTitle = $"Edit {project.DisplayName} Treatments";

            //TreatmentActivityStatus = treatmentActivityStatus;
        }
    }
}