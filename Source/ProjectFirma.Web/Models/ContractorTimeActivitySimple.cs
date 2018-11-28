/*-----------------------------------------------------------------------
<copyright file="ContractorTimeActivitySimple.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Models
{
    public class ContractorTimeActivitySimple
    {
        public int? ContractorTimeActivityID { get; set; }

        [Required(ErrorMessage="Grant is required.")]
        public int? FundingSourceID { get; set; }

        [Required(ErrorMessage="Project is required.")]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DisplayName("Start Date")]
        public DateTime? ContractorTimeActivityStartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime? ContractorTimeActivityEndDate { get; set; }

        [Required(ErrorMessage = "Number of Hours is required.")]
        [Range(0d, 50000000)]
        public decimal? ContractorTimeActivityHours { get; set; }

        [Required(ErrorMessage = "Rate is required.")]
        [Range(0d, 50000000)]
        public decimal? ContractorTimeActivityRate { get; set; }

        [Required(ErrorMessage = "Acreage is required.")]
        [Range(0d, 50000000)]
        public decimal? ContractorTimeActivityAcreage { get; set; }

        public string ContractorTimeActivityNotes { get; set; }

        // Needed by ModelBinder
        public ContractorTimeActivitySimple()
        {

        }

        public ContractorTimeActivitySimple(ContractorTimeActivity y)
        {
            ContractorTimeActivityID = y.ContractorTimeActivityID;
            ContractorTimeActivityEndDate = y.ContractorTimeActivityEndDate;
            ContractorTimeActivityHours = y.ContractorTimeActivityHours;
            ContractorTimeActivityNotes = y.ContractorTimeActivityNotes;
            ContractorTimeActivityRate = y.ContractorTimeActivityRate;
            ContractorTimeActivityStartDate = y.ContractorTimeActivityStartDate;
            ContractorTimeActivityAcreage = y.ContractorTimeActivityAcreage;
            FundingSourceID = y.FundingSourceID;
            ProjectID = y.ProjectID;
        }

        public ContractorTimeActivity ToContractorTimeActivity()
        {
            // None of the nullables will ever default, thanks to RequiredAttribute
            return new ContractorTimeActivity(ContractorTimeActivityID ?? ModelObjectHelpers.NotYetAssignedID,
                ProjectID,
                FundingSourceID.GetValueOrDefault(),
                ContractorTimeActivityAcreage.GetValueOrDefault(),
                ContractorTimeActivityHours.GetValueOrDefault(),
                ContractorTimeActivityRate.GetValueOrDefault(),
                ContractorTimeActivityStartDate.GetValueOrDefault(),
                ContractorTimeActivityEndDate,
                ContractorTimeActivityNotes);
        }
    }
}