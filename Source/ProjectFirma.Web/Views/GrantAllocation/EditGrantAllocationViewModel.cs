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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class EditGrantAllocationViewModel : FormViewModel, IValidatableObject
    {
        public int GrantAllocationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectName)]
        [Required]
        public string ProjectName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Organization)]
        [Required]
        public int OrganizationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantStatus)]
        [Required]
        public string GrantStatus { get; set; }

        public string GrantType { get; set; }
        public string GrantNumber { get; set; }
        public string ProgramIndex { get; set; }
        public string CFDA { get; set; }
        public string ProjectCode { get; set; }
        public string FederalFundCode { get; set; }
        public string Region { get; set; }
        public string AllocationAmount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
      
 /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationViewModel()
        {
        }

        public EditGrantAllocationViewModel(Models.GrantAllocation grantAllocation)
        {
            ProjectName = grantAllocation.ProjectName;
            OrganizationID = grantAllocation.Grant.OrganizationID;
            //TODO GET THESE WIRED TO THE FRONT SIDE EDIT DIALOG IN EditGrantAllocation.cshtml
            GrantStatus = grantAllocation.Grant.GrantStatus.GrantStatusName;
            GrantType = grantAllocation.Grant.GrantType.GrantTypeName;
            GrantNumber = grantAllocation.Grant.GrantNumber;
            ProgramIndex = grantAllocation.ProgramIndex.ProgramIndexAbbrev;
            CFDA = grantAllocation.Grant.CFDANumber;
            ProjectCode = grantAllocation.ProjectCodesString;
            FederalFundCode = grantAllocation.FederalFundCode.FederalFundCodeAbbrev;
            Region = grantAllocation.Region.RegionName;
            AllocationAmount = grantAllocation.AllocationAmount.ToStringCurrency();
            StartDate = grantAllocation.StartDateDisplay;
            EndDate = grantAllocation.EndDateDisplay;
        }

        public void UpdateModel(Models.GrantAllocation grantAllocation, Person currentPerson)
        {
            grantAllocation.ProjectName = ProjectName;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProjectName == "")
            {
                yield return new SitkaValidationResult<EditGrantAllocationViewModel, string>(
                    FirmaValidationMessages.OrganizationNameUnique, m => m.ProjectName);
            }
        }
    }
}