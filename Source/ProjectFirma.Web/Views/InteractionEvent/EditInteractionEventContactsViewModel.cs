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
    public class EditInteractionEventContactsViewModel : FormViewModel, IValidatableObject
    {
        public List<InteractionEventContact> InteractionEventContacts { get; }


        

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditInteractionEventContactsViewModel()
        {
        }

        public EditInteractionEventContactsViewModel(Models.InteractionEvent interactionEvent, IEnumerable<InteractionEventContact> interactionEventContacts)
        {
            InteractionEventContacts = interactionEventContacts.ToList();

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }

        public void UpdateModel(Models.InteractionEvent interactionEvent, Person currentPerson)
        {

        }
    }
}