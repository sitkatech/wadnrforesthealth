/*-----------------------------------------------------------------------
<copyright file="EditPersonnelAndBenefitsViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel.DataAnnotations;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class EditPersonnelAndBenefitsViewModel : FormViewModel
    {
        public int GrantAllocationAwardID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardPersonnelAndBenefitsAllocationTotal)]
        [Required]
        public Money PersonnelAndBenefitsAllocationTotal { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardPersonnelAndBenefitsForester)]
        [Required]
        public string PersonnelAndBenefitsForester { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditPersonnelAndBenefitsViewModel()
        {
        }

        public EditPersonnelAndBenefitsViewModel(Models.GrantAllocationAward grantAllocationAward)
        {
            GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID;
            PersonnelAndBenefitsAllocationTotal = grantAllocationAward.PersonnelAndBenefitsAllocationTotal ?? 0m;
            PersonnelAndBenefitsForester = grantAllocationAward.PersonnelAndBenefitsForester;

        }



        public void UpdateModel(Models.GrantAllocationAward grantAllocationAward)
        {
            grantAllocationAward.GrantAllocationAwardID = GrantAllocationAwardID;
            grantAllocationAward.PersonnelAndBenefitsAllocationTotal = PersonnelAndBenefitsAllocationTotal;
            grantAllocationAward.PersonnelAndBenefitsForester = PersonnelAndBenefitsForester;

        }
    }

}