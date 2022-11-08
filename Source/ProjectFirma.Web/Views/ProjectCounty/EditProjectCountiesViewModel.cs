/*-----------------------------------------------------------------------
<copyright file="EditProjectCountiesViewModel.cs" company="Tahoe Countyal Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Countyal Planning Agency and Environmental Science Associates. All rights reserved.
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectCounty
{
    public class EditProjectCountiesViewModel : FormViewModel, IValidatableObject
    {
        [DisplayName("Project Counties")]
        public IEnumerable<int> CountyIDs { get; set; }

        [DisplayName("Notes")]
        public string NoCountiesExplanation { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditProjectCountiesViewModel()
        {
        }

        public EditProjectCountiesViewModel(List<int> countyIDs, string noCountiesExplanation)
        {
            CountyIDs = countyIDs;
            NoCountiesExplanation = noCountiesExplanation;
        }

        public void UpdateModel(Models.Project project, List<Models.ProjectCounty> currentProjectCounties, IList<Models.ProjectCounty> allProjectCounties)
        {
            var newProjectCounties = CountyIDs?.Select(x => new Models.ProjectCounty(project.ProjectID, x)).ToList() ?? new List<Models.ProjectCounty>();
            currentProjectCounties.Merge(newProjectCounties, allProjectCounties, (x, y) => x.ProjectID == y.ProjectID && x.CountyID == y.CountyID);
        }

        public void UpdateModel(ProjectUpdateBatch project, List<ProjectCountyUpdate> currentProjectCounties, IList<ProjectCountyUpdate> allProjectCounties)
        {
            var newProjectCounties = CountyIDs?.Select(x => new ProjectCountyUpdate(project.ProjectUpdateBatchID, x)).ToList() ?? new List<ProjectCountyUpdate>();
            currentProjectCounties.Merge(newProjectCounties, allProjectCounties, (x, y) => x.ProjectUpdateBatchID == y.ProjectUpdateBatchID && x.CountyID == y.CountyID);
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GetValidationResults();
        }

        public IEnumerable<ValidationResult> GetValidationResults()
        {
            var errors = new List<ValidationResult>();
            var noCountiesSelected = CountyIDs == null || CountyIDs.Count().Equals(0);
            if (noCountiesSelected && string.IsNullOrWhiteSpace(NoCountiesExplanation))
            {
                errors.Add(
                    new SitkaValidationResult<EditProjectCountiesViewModel, string>(
                        $"Select at least one {Models.FieldDefinition.County.GetFieldDefinitionLabel()} or provide explanatory information in the Notes section if there are no applicable {Models.FieldDefinition.County.GetFieldDefinitionLabelPluralized()} for this Project.",
                        x => x.NoCountiesExplanation));
            }

            return errors;
        }
    }
}
