/*-----------------------------------------------------------------------
<copyright file="EditStaffTimeActivitysViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class EditStaffTimeActivitiesViewModel : FormViewModel, IValidatableObject
    {
        public List<StaffTimeActivitySimple> StaffTimeActivities { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditStaffTimeActivitiesViewModel()
        {
        }

        public EditStaffTimeActivitiesViewModel(Models.Project project,
            List<StaffTimeActivitySimple> staffTimeActivities)
        {
            StaffTimeActivities = staffTimeActivities;
        }

        public void UpdateModel(List<StaffTimeActivity> currentStaffTimeActivitys,
            IList<StaffTimeActivity> allStaffTimeActivitys, Models.Project project)
        {
            var staffTimeActivitysUpdated = new List<StaffTimeActivity>();
            if (StaffTimeActivities != null)
            {
                staffTimeActivitysUpdated = StaffTimeActivities.Select(x => x.ToStaffTimeActivity()).ToList();
            }
            
            currentStaffTimeActivitys.Merge(staffTimeActivitysUpdated,
                allStaffTimeActivitys,
                (x, y) => x.StaffTimeActivityID == y.StaffTimeActivityID,
                (x, y) =>
                {
                    x.StaffTimeActivityEndDate = y.StaffTimeActivityEndDate;
                    x.StaffTimeActivityHours = y.StaffTimeActivityHours;
                    x.StaffTimeActivityNotes = y.StaffTimeActivityNotes;
                    x.StaffTimeActivityRate = y.StaffTimeActivityRate;
                    x.StaffTimeActivityStartDate = y.StaffTimeActivityStartDate;
                    x.StaffTimeActivityTotalAmount = y.StaffTimeActivityTotalAmount;
                });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
