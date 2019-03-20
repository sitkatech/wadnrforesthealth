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
    public class EditInteractionEventProjectsViewModel : FormViewModel, IValidatableObject
    {
        public List<InteractionEventProjectSimple> InteractionEventProjects { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditInteractionEventProjectsViewModel()
        {
        }

        public EditInteractionEventProjectsViewModel(IEnumerable<InteractionEventProject> interactionEventProjects)
        {
            InteractionEventProjects = interactionEventProjects.Select(x => new InteractionEventProjectSimple(){ InteractionEventID = x.InteractionEventID, ProjectID = x.ProjectID}).ToList();

        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }

        public void UpdateModel(Models.InteractionEvent interactionEvent, ICollection<InteractionEventProject> allInteractionEventProjects)
        {
            var interactionEventProjectsUpdated = InteractionEventProjects.Where(x => ModelObjectHelpers.IsRealPrimaryKeyValue(x.ProjectID)).Select(x =>
                new Models.InteractionEventProject(interactionEvent.InteractionEventID, x.ProjectID)).ToList();

            interactionEvent.InteractionEventProjects.Merge(interactionEventProjectsUpdated,
                allInteractionEventProjects,
                (x, y) => x.InteractionEventID == y.InteractionEventID && x.ProjectID == y.ProjectID);
        }
    }

    public class InteractionEventProjectSimple
    {
        public int InteractionEventID { get; set; }
        public int ProjectID { get; set; }
    }

}