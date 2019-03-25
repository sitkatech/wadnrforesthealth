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

        //[FieldDefinitionDisplay(FieldDefinitionEnum.InteractionEvent)]
        [Required]
        public int InteractionEventTypeID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int DNRStaffPersonID { get; set; }

        public List<InteractionEventProjectSimple> InteractionEventProjects { get; set; }



        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditInteractionEventViewModel()
        {
            Date = DateTime.Now;
            InteractionEventProjects = new List<InteractionEventProjectSimple>();
        }

        public EditInteractionEventViewModel(Models.InteractionEvent interactionEvent)
        {
            InteractionEventTypeID = interactionEvent.InteractionEventTypeID;
            Date = interactionEvent.InteractionEventDate;
            Title = interactionEvent.InteractionEventTitle;
            Description = interactionEvent.InteractionEventDescription;
            DNRStaffPersonID = interactionEvent.StaffPersonID;
            InteractionEventProjects = interactionEvent.InteractionEventProjects.Select(x => new InteractionEventProjectSimple() { InteractionEventID = x.InteractionEventID, ProjectID = x.ProjectID }).ToList();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == "")
            {
                yield return new SitkaValidationResult<EditInteractionEventViewModel, string>(
                    FirmaValidationMessages.InteractionEventMustHaveTitle, m => m.Title);
            }
        }

        public void UpdateModel(Models.InteractionEvent interactionEvent, Person currentPerson, ICollection<InteractionEventProject> allInteractionEventProjects)
        {
            interactionEvent.InteractionEventTypeID = InteractionEventTypeID;
            interactionEvent.InteractionEventDate = Date;
            interactionEvent.InteractionEventTitle = Title;
            interactionEvent.InteractionEventDescription = Description;
            interactionEvent.StaffPersonID = DNRStaffPersonID;
            if (InteractionEventProjects != null)
            {
                var interactionEventProjectsUpdated = InteractionEventProjects
                    .Where(x => ModelObjectHelpers.IsRealPrimaryKeyValue(x.ProjectID)).Select(x =>
                        new Models.InteractionEventProject(interactionEvent.InteractionEventID, x.ProjectID)).ToList();

                interactionEvent.InteractionEventProjects.Merge(interactionEventProjectsUpdated,
                    allInteractionEventProjects,
                    (x, y) => x.InteractionEventID == y.InteractionEventID && x.ProjectID == y.ProjectID);
            }
        }
    }

    public class InteractionEventProjectSimple
    {
        public int InteractionEventID { get; set; }
        public int ProjectID { get; set; }
    }
}