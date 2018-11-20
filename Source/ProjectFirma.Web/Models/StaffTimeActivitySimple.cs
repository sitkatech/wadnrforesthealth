/*-----------------------------------------------------------------------
<copyright file="StaffTimeActivitySimple.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.ProjectUpdate;

namespace ProjectFirma.Web.Models
{
    public class StaffTimeActivitySimple
    {
        public int? StaffTimeActivityID { get; set; }

        [Required(ErrorMessage="Funding Source is required.")]
        public int? FundingSourceID { get; set; }

        [Required(ErrorMessage="Project is required.")]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime? StaffTimeActivityStartDate { get; set; }

        public DateTime? StaffTimeActivityEndDate { get; set; }

        [Required(ErrorMessage = "Number of Hours is required.")]
        public decimal? StaffTimeActivityHours { get; set; }

        [Required(ErrorMessage = "Rate is required.")]
        public decimal? StaffTimeActivityRate { get; set; }

        [Required(ErrorMessage = "Total Amount is required.")]
        public decimal? StaffTimeActivityTotalAmount { get; set; }

        public string StaffTimeActivityNotes { get; set; }

        // Needed by ModelBinder
        public StaffTimeActivitySimple()
        {

        }

        public StaffTimeActivitySimple(StaffTimeActivity y)
        {
            StaffTimeActivityID = y.StaffTimeActivityID;
            StaffTimeActivityEndDate = y.StaffTimeActivityEndDate;
            StaffTimeActivityHours = y.StaffTimeActivityHours;
            StaffTimeActivityNotes = y.StaffTimeActivityNotes;
            StaffTimeActivityRate = y.StaffTimeActivityRate;
            StaffTimeActivityStartDate = y.StaffTimeActivityStartDate;
            StaffTimeActivityTotalAmount = y.StaffTimeActivityTotalAmount;
            FundingSourceID = y.FundingSourceID;
            ProjectID = y.ProjectID;
        }

        public StaffTimeActivity ToStaffTimeActivity()
        {
            // None of the nullables will ever default, thanks to RequiredAttribute
            return new StaffTimeActivity(StaffTimeActivityID ?? ModelObjectHelpers.NotYetAssignedID, ProjectID, FundingSourceID.GetValueOrDefault(),
                StaffTimeActivityHours.GetValueOrDefault(), StaffTimeActivityRate.GetValueOrDefault(),
                StaffTimeActivityTotalAmount.GetValueOrDefault(), StaffTimeActivityStartDate.GetValueOrDefault(),
                StaffTimeActivityEndDate, StaffTimeActivityNotes);
        }
    }
}