/*-----------------------------------------------------------------------
<copyright file="EditTreatmentActivitysViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ApprovalUtilities.Utilities;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class EditTreatmentActivitiesViewModel : FormViewModel, IValidatableObject
    {
        public List<TreatmentActivitySimple> TreatmentActivities { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditTreatmentActivitiesViewModel()
        {
        }

        public EditTreatmentActivitiesViewModel(Models.Project project,
            List<TreatmentActivitySimple> treatmentActivities)
        {
            TreatmentActivities = treatmentActivities;
        }

        public void UpdateModel(List<TreatmentActivity> currentTreatmentActivitys,
            IList<TreatmentActivity> allTreatmentActivitys, Models.Project project)
        {
            var treatmentActivitysUpdated = new List<TreatmentActivity>();
            if (TreatmentActivities != null)
            {
                treatmentActivitysUpdated = TreatmentActivities.Select(x => x.ToTreatmentActivity()).ToList();
            }

            project.TreatmentActivities.AddAll(treatmentActivitysUpdated.Where(x =>
                x.TreatmentActivityID == ModelObjectHelpers.NotYetAssignedID));
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            currentTreatmentActivitys.Merge(treatmentActivitysUpdated,
                allTreatmentActivitys,
                (x, y) => x.TreatmentActivityID == y.TreatmentActivityID,
                (x, y) =>
                {
                    x.TreatmentActivityEndDate = y.TreatmentActivityEndDate;
                    x.TreatmentActivityNotes = y.TreatmentActivityNotes;
                    x.TreatmentActivityStartDate = y.TreatmentActivityStartDate;
                    x.TreatmentActivityAcresTreated = y.TreatmentActivityAcresTreated;
                    x.TreatmentActivityTypeID = y.TreatmentActivityTypeID;
                });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TreatmentActivities?.Any(x =>
                x.TreatmentActivityEndDate != null && x.TreatmentActivityEndDate.Value < x.TreatmentActivityStartDate) ?? false)
            {
                yield return new ValidationResult("End Date cannot be before Start Date");
            }
        }
    }
}
