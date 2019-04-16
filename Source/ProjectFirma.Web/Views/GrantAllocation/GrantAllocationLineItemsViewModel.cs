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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Views.ProgramIndex;
using ProjectFirma.Web.Views.ProjectCode;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class GrantAllocationLineItemsViewModel : FormViewModel, IValidatableObject
    {
        public int GrantAllocationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationName)]
        [Required]
        public string GrantAllocationName { get; set; }

     
        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public GrantAllocationLineItemsViewModel()
        {
        }

        public GrantAllocationLineItemsViewModel(Models.GrantAllocation grantAllocation)
        {
            GrantAllocationID = grantAllocation.GrantAllocationID;
            GrantAllocationName = grantAllocation.GrantAllocationName;
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (GrantAllocationName == "")
            {
                yield return new SitkaValidationResult<EditGrantAllocationViewModel, string>(
                    FirmaValidationMessages.OrganizationNameUnique, m => m.GrantAllocationName);
            }
        }

        public void UpdateModel(Models.GrantAllocation grantAllocation, Person currentPerson)
        {
            grantAllocation.GrantAllocationName = GrantAllocationName;
        }
    }
}