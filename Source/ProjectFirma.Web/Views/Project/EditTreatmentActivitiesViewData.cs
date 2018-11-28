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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class EditTreatmentActivitiesViewData : FirmaViewData
    {
        public List<TreatmentTypeSimple> AllTreatmentTypes { get; }
        public int ProjectID { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; }
        public string ProjectUrl { get; }

        public EditTreatmentActivitiesViewData(Models.Project project, List<TreatmentTypeSimple> allTreatmentTypes, Person currentPerson) : base(currentPerson)
        {
            AllTreatmentTypes = allTreatmentTypes;
            ProjectID = project.ProjectID;
            ViewDataForAngular = new ViewDataForAngularClass(AllTreatmentTypes, ProjectID);
            ProjectUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.Detail(ProjectID));

            EntityName = Models.FieldDefinition.Project.GetFieldDefinitionLabel();
            PageTitle = $"Edit {project.DisplayName} Treatment Activities";
        }

        public class ViewDataForAngularClass
        {
            public List<TreatmentTypeSimple> AllTreatmentTypes { get; }
            public int ProjectID { get; }
            public List<FundingSourceSimple> AllFundingSources { get; }

            public ViewDataForAngularClass(List<TreatmentTypeSimple> allTreatmentTypes, int projectID)
            {
                // todo: pull this out to the controller
                AllFundingSources = HttpRequestStorage.DatabaseEntities.FundingSources.ToList().Select(x => new FundingSourceSimple(x)).OrderBy(p => p.DisplayName).ToList();
                AllTreatmentTypes = allTreatmentTypes;
                ProjectID = projectID;
            }
        }
    }
}