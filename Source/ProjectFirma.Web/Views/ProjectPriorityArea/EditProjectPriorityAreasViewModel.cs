/*-----------------------------------------------------------------------
<copyright file="EditProjectPriorityAreasViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectPriorityArea
{
    public class EditProjectPriorityAreasViewModel : FormViewModel, IValidatableObject
    {
        [DisplayName("Project Priority Areas")]
        public IEnumerable<int> PriorityAreaIDs { get; set; }

        [DisplayName("Notes")]
        public string NoPriorityAreasExplanation { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditProjectPriorityAreasViewModel()
        {
        }

        public EditProjectPriorityAreasViewModel(List<int> priorityAreaIDs, string noPriorityAreasExplanation)
        {
            PriorityAreaIDs = priorityAreaIDs;
            NoPriorityAreasExplanation = noPriorityAreasExplanation;
        }

        public void UpdateModel(Models.Project project, List<Models.ProjectPriorityArea> currentProjectPriorityAreas, IList<Models.ProjectPriorityArea> allProjectPriorityAreas)
        {
            var newProjectPriorityAreas = PriorityAreaIDs?.Select(x => new Models.ProjectPriorityArea(project.ProjectID, x)).ToList() ?? new List<Models.ProjectPriorityArea>();
            currentProjectPriorityAreas.Merge(newProjectPriorityAreas, allProjectPriorityAreas, (x, y) => x.ProjectID == y.ProjectID && x.PriorityAreaID == y.PriorityAreaID);
        }

        public void UpdateModel(ProjectUpdateBatch project, List<ProjectPriorityAreaUpdate> currentProjectPriorityAreas, IList<ProjectPriorityAreaUpdate> allProjectPriorityAreas)
        {
            var newProjectPriorityAreas = PriorityAreaIDs?.Select(x => new ProjectPriorityAreaUpdate(project.ProjectUpdateBatchID, x)).ToList() ?? new List<ProjectPriorityAreaUpdate>();
            currentProjectPriorityAreas.Merge(newProjectPriorityAreas, allProjectPriorityAreas, (x, y) => x.ProjectUpdateBatchID == y.ProjectUpdateBatchID && x.PriorityAreaID == y.PriorityAreaID);
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GetValidationResults();
        }

        public IEnumerable<ValidationResult> GetValidationResults()
        {
            var errors = new List<ValidationResult>();
            var noPriorityAreasSelected = PriorityAreaIDs == null || PriorityAreaIDs.Count().Equals(0);
            if (noPriorityAreasSelected && string.IsNullOrWhiteSpace(NoPriorityAreasExplanation))
            {
                errors.Add(
                    new SitkaValidationResult<EditProjectPriorityAreasViewModel, string>(
                        "Select at least one priority area or provide explanatory information in the Notes section if there are no applicable priority areas for this Project.",
                        x => x.NoPriorityAreasExplanation));
            }

            return errors;
        }
    }
}
