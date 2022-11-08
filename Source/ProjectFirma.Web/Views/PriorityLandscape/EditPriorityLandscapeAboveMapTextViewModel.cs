/*-----------------------------------------------------------------------
<copyright file="EditPriorityLandscapeAboveMapTextViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
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
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.PriorityLandscape
{
    public class EditPriorityLandscapeAboveMapTextViewModel : FormViewModel, IValidatableObject
    {
        public int PriorityLandscapeID { get; set; }

        [DisplayName("Above Map Text")]
        [StringLength(Models.PriorityLandscape.FieldLengths.PriorityLandscapeAboveMapText)]
        public string PriorityLandscapeAboveMapText { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditPriorityLandscapeAboveMapTextViewModel()
        {
        }

        public EditPriorityLandscapeAboveMapTextViewModel(Models.PriorityLandscape priorityLandscape)
        {
            PriorityLandscapeAboveMapText = priorityLandscape.PriorityLandscapeAboveMapText;
        }

        public void UpdateModel(Models.PriorityLandscape priorityLandscape)
        {
            priorityLandscape.PriorityLandscapeAboveMapText = PriorityLandscapeAboveMapText;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}