/*-----------------------------------------------------------------------
<copyright file="EditProjectViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class EditGrantAllocationAwardViewModel : FormViewModel
    {
        public int GrantAllocationAwardID { get; set; }

        public int FocusAreaID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocation)]
        [Required]
        public int GrantAllocationID { get; set; }

        [StringLength(Models.GrantAllocationAward.FieldLengths.GrantAllocationAwardName)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardName)]
        [Required]
        public string GrantAllocationAwardName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAward)]
        [Required]
        public Money GrantAllocationAwardAmount { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardExpirationDate)]
        [Required]
        public DateTime GrantAllocationAwardExpirationDate { get; set; }

        

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationAwardViewModel()
        {
            GrantAllocationAwardExpirationDate = DateTime.Today;
        }

        public EditGrantAllocationAwardViewModel(Models.GrantAllocationAward grantAllocationAward)
        {
            GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID;
            GrantAllocationAwardName = grantAllocationAward.GrantAllocationAwardName;
            GrantAllocationAwardAmount = grantAllocationAward.GrantAllocationAwardAmount;
            if (grantAllocationAward.GrantAllocationAwardExpirationDate == DateTime.MinValue)
            {
                GrantAllocationAwardExpirationDate = DateTime.Today;
            }
            else
            {
                GrantAllocationAwardExpirationDate = grantAllocationAward.GrantAllocationAwardExpirationDate;
            }

            GrantAllocationID = grantAllocationAward.GrantAllocationID;
            FocusAreaID = grantAllocationAward.FocusAreaID;

        }



        public void UpdateModel(Models.GrantAllocationAward grantAllocationAward)
        {
            grantAllocationAward.GrantAllocationAwardID = GrantAllocationAwardID;
            grantAllocationAward.GrantAllocationAwardName = GrantAllocationAwardName;
            grantAllocationAward.GrantAllocationAwardAmount = GrantAllocationAwardAmount;
            grantAllocationAward.GrantAllocationAwardExpirationDate = GrantAllocationAwardExpirationDate;

            grantAllocationAward.GrantAllocationID = GrantAllocationID;
            grantAllocationAward.FocusAreaID = FocusAreaID;

        }
    }

}