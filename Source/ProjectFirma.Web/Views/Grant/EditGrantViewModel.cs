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

namespace ProjectFirma.Web.Views.Grant
{
    public class EditGrantViewModel : FormViewModel, IValidatableObject
    {
        public int GrantID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Organization)]
        [Required]
        public int OrganizationID { get; set; }

      


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantViewModel()
        {
        }

        public EditGrantViewModel(Models.Grant grant)
        {
            OrganizationID = grant.OrganizationID;
        }

        public void UpdateModel(Models.Grant grant, Person currentPerson)
        {
            grant.OrganizationID = OrganizationID;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OrganizationID == 0)
            {
                yield return new SitkaValidationResult<EditGrantViewModel, int>(
                    FirmaValidationMessages.OrganizationNameUnique, m => m.OrganizationID);
            }
        }
    }
}