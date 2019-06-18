/*-----------------------------------------------------------------------
<copyright file="EditLandownerCostShareViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
    public class EditLandownerCostShareViewModel : FormViewModel
    {
        public int GrantAllocationAwardID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareAllocationTotal)]
        [Required]
        public Money LandownerCostShareAllocationTotal { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareTargetFootprintAcreage)]
        public int LandownerCostShareTargetFootprintAcreage { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareTargetTotalAcreage)]
        public int LandownerCostShareTargetTotalAcreage { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareForester)]
        public string LandownerCostShareForester { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditLandownerCostShareViewModel()
        {
        }

        public EditLandownerCostShareViewModel(Models.GrantAllocationAward grantAllocationAward)
        {
            GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID;
            LandownerCostShareAllocationTotal = grantAllocationAward.LandownerCostShareAllocationTotal ?? 0m;
            LandownerCostShareTargetFootprintAcreage = grantAllocationAward.LandownerCostShareTargetFootprintAcreage ?? 0;
            LandownerCostShareTargetTotalAcreage = grantAllocationAward.LandownerCostShareTargetTotalAcreage ?? 0;
            LandownerCostShareForester = grantAllocationAward.LandownerCostShareForester;

        }



        public void UpdateModel(Models.GrantAllocationAward grantAllocationAward)
        {
            grantAllocationAward.GrantAllocationAwardID = GrantAllocationAwardID;
            grantAllocationAward.LandownerCostShareAllocationTotal = LandownerCostShareAllocationTotal;
            grantAllocationAward.LandownerCostShareTargetFootprintAcreage = LandownerCostShareTargetFootprintAcreage;
            grantAllocationAward.LandownerCostShareTargetTotalAcreage = LandownerCostShareTargetTotalAcreage;
            grantAllocationAward.LandownerCostShareForester = LandownerCostShareForester;

        }
    }

}