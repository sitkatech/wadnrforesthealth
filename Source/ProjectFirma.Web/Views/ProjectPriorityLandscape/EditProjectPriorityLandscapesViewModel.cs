/*-----------------------------------------------------------------------
<copyright file="EditProjectPriorityLandscapesViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace ProjectFirma.Web.Views.ProjectPriorityLandscape
{
    public class EditProjectPriorityLandscapesViewModel : FormViewModel, IValidatableObject
    {
        [DisplayName("Project Priority Landscapes")]
        public IEnumerable<int> PriorityLandscapeIDs { get; set; }

        [DisplayName("Notes")]
        public string NoPriorityLandscapesExplanation { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditProjectPriorityLandscapesViewModel()
        {
        }

        public EditProjectPriorityLandscapesViewModel(List<int> priorityLandscapeIDs, string noPriorityLandscapesExplanation)
        {
            PriorityLandscapeIDs = priorityLandscapeIDs;
            NoPriorityLandscapesExplanation = noPriorityLandscapesExplanation;
        }

        public void UpdateModel(Models.Project project, List<Models.ProjectPriorityLandscape> currentProjectPriorityLandscapes, IList<Models.ProjectPriorityLandscape> allProjectPriorityLandscapes)
        {
            var newProjectPriorityLandscapes = PriorityLandscapeIDs?.Select(x => new Models.ProjectPriorityLandscape(project.ProjectID, x)).ToList() ?? new List<Models.ProjectPriorityLandscape>();
            currentProjectPriorityLandscapes.Merge(newProjectPriorityLandscapes, allProjectPriorityLandscapes, (x, y) => x.ProjectID == y.ProjectID && x.PriorityLandscapeID == y.PriorityLandscapeID);
        }

        public void UpdateModel(ProjectUpdateBatch project, List<ProjectPriorityLandscapeUpdate> currentProjectPriorityLandscapes, IList<ProjectPriorityLandscapeUpdate> allProjectPriorityLandscapes)
        {
            var newProjectPriorityLandscapes = PriorityLandscapeIDs?.Select(x => new ProjectPriorityLandscapeUpdate(project.ProjectUpdateBatchID, x)).ToList() ?? new List<ProjectPriorityLandscapeUpdate>();
            currentProjectPriorityLandscapes.Merge(newProjectPriorityLandscapes, allProjectPriorityLandscapes, (x, y) => x.ProjectUpdateBatchID == y.ProjectUpdateBatchID && x.PriorityLandscapeID == y.PriorityLandscapeID);
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GetValidationResults();
        }

        public IEnumerable<ValidationResult> GetValidationResults()
        {
            var errors = new List<ValidationResult>();
            var noPriorityLandscapesSelected = PriorityLandscapeIDs == null || PriorityLandscapeIDs.Count().Equals(0);
            if (noPriorityLandscapesSelected && string.IsNullOrWhiteSpace(NoPriorityLandscapesExplanation))
            {
                errors.Add(
                    new SitkaValidationResult<EditProjectPriorityLandscapesViewModel, string>(
                        "Select at least one priority landscape or provide explanatory information in the Notes section if there are no applicable priority areas for this Project.",
                        x => x.NoPriorityLandscapesExplanation));
            }

            return errors;
        }
    }
}
