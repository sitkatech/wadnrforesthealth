/*-----------------------------------------------------------------------
<copyright file="EditAgreementPersonViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.Agreement
{
    public class EditAgreementGrantAllocationsViewModel : FormViewModel
    {
        public int AgreementId { get; set; }

        //public int AgreementGrantAllocationId { get; }

        //[DisplayName("Grant Allocations")]
        //public int GrantAllocationId { get; set; }
        //public int? GrantId { get; }

        // GrantAllocationIDs for the relevant agreement
        public List<int> GrantAllocationIDs { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditAgreementGrantAllocationsViewModel()
        {
        }

        public EditAgreementGrantAllocationsViewModel(Models.Agreement agreement)
        {
            AgreementId = agreement.AgreementID;
            GrantAllocationIDs = agreement.AgreementGrantAllocations.Select(aga => aga.GrantAllocationID).ToList();
        }

        public void UpdateModel(Models.Agreement agreement)
        {
            //agreementGrantAllocation.AgreementID = AgreementId;
            //agreementGrantAllocation.AgreementGrantAllocationID = AgreementGrantAllocationId;

            //agreementGrantAllocation.GrantAllocationID = AgreementGrantAllocationId;
        }
    }
}