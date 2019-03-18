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
using ApprovalUtilities.Utilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class EditInteractionEventViewModel : FormViewModel, IValidatableObject
    {
        public int InteractionEventID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.InteractionEvent)]
        [Required]
        public string InteractionEventType { get; set; }

        

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditInteractionEventViewModel()
        {
        }

        public EditInteractionEventViewModel(Models.InteractionEvent interactionEvent)
        {
            InteractionEventType = interactionEvent.InteractionEventType.InteractionEventTypeDisplayName;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (InteractionEventType == "")
            {
                yield return new SitkaValidationResult<EditInteractionEventViewModel, string>(
                    FirmaValidationMessages.OrganizationNameUnique, m => m.InteractionEventType);
            }
        }

        public void UpdateModel(Models.InteractionEvent interactionEvent, Person currentPerson)
        {
            interactionEvent.InteractionEventType.InteractionEventTypeDisplayName = InteractionEventType;
        }
    }
}