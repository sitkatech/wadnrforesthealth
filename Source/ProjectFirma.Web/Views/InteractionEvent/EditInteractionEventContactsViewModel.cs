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
using Antlr.Runtime.Misc;
using ApprovalUtilities.Utilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class EditInteractionEventContactsViewModel : FormViewModel, IValidatableObject
    {
        public List<InteractionEventContactSimple> InteractionEventContacts { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditInteractionEventContactsViewModel()
        {
        }

        public EditInteractionEventContactsViewModel(IEnumerable<InteractionEventContact> interactionEventContacts)
        {
            InteractionEventContacts = interactionEventContacts.Select(x => new InteractionEventContactSimple(){ InteractionEventID = x.InteractionEventID, PersonID = x.PersonID}).ToList();

        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }

        public void UpdateModel(Models.InteractionEvent interactionEvent, ICollection<InteractionEventContact> allInteractionEventContacts)
        {
            var interactionEventContactsUpdated = InteractionEventContacts.Where(x => ModelObjectHelpers.IsRealPrimaryKeyValue(x.PersonID)).Select(x =>
                new Models.InteractionEventContact(interactionEvent.InteractionEventID, x.PersonID)).ToList();

            interactionEvent.InteractionEventContacts.Merge(interactionEventContactsUpdated,
                allInteractionEventContacts,
                (x, y) => x.InteractionEventID == y.InteractionEventID && x.PersonID == y.PersonID);
        }
    }

    public class InteractionEventContactSimple
    {
        public int InteractionEventID { get; set; }
        public int PersonID { get; set; }
    }

}