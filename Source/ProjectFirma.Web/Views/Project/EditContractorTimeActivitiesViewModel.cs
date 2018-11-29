/*-----------------------------------------------------------------------
<copyright file="EditContractorTimeActivitysViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
    public class EditContractorTimeActivitiesViewModel : FormViewModel, IValidatableObject
    {
        public List<ContractorTimeActivitySimple> ContractorTimeActivities { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditContractorTimeActivitiesViewModel()
        {
        }

        public EditContractorTimeActivitiesViewModel(Models.Project project,
            List<ContractorTimeActivitySimple> contractorTimeActivities)
        {
            ContractorTimeActivities = contractorTimeActivities;
        }

        public void UpdateModel(List<ContractorTimeActivity> currentContractorTimeActivitys,
            IList<ContractorTimeActivity> allContractorTimeActivitys, Models.Project project)
        {
            var contractorTimeActivitysUpdated = new List<ContractorTimeActivity>();
            if (ContractorTimeActivities != null)
            {
                contractorTimeActivitysUpdated = ContractorTimeActivities.Select(x => x.ToContractorTimeActivity()).ToList();
            }

            project.ContractorTimeActivities.AddAll(contractorTimeActivitysUpdated.Where(x =>
                x.ContractorTimeActivityID == ModelObjectHelpers.NotYetAssignedID));
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            currentContractorTimeActivitys.Merge(contractorTimeActivitysUpdated,
                allContractorTimeActivitys,
                (x, y) => x.ContractorTimeActivityID == y.ContractorTimeActivityID,
                (x, y) =>
                {
                    x.ContractorTimeActivityEndDate = y.ContractorTimeActivityEndDate;
                    x.ContractorTimeActivityHours = y.ContractorTimeActivityHours;
                    x.ContractorTimeActivityAcreage = y.ContractorTimeActivityAcreage;
                    x.ContractorTimeActivityNotes = y.ContractorTimeActivityNotes;
                    x.ContractorTimeActivityRate = y.ContractorTimeActivityRate;
                    x.ContractorTimeActivityStartDate = y.ContractorTimeActivityStartDate;
                });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ContractorTimeActivities?.Any(x =>
                x.ContractorTimeActivityEndDate != null && x.ContractorTimeActivityEndDate.Value < x.ContractorTimeActivityStartDate) ?? false)
            {
                yield return new ValidationResult("End Date cannot be before Start Date");
            }
        }
    }
}
