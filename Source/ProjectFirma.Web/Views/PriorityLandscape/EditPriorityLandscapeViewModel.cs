/*-----------------------------------------------------------------------
<copyright file="EditPriorityLandscapeViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.PriorityLandscape
{
    public class EditPriorityLandscapeViewModel : FormViewModel, IValidatableObject
    {
        public int PriorityLandscapeID { get; set; }

        //9/18/20 TK - We do not want them to edit the Name because these are imported from another source
        [DisplayName("Name")]
        public string PriorityLandscapeName { get; }

        [DisplayName("Description")]
        public HtmlString PriorityLandscapeDescription { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditPriorityLandscapeViewModel()
        {
        }


        public EditPriorityLandscapeViewModel(Models.PriorityLandscape priorityLandscape)
        {
            PriorityLandscapeName = priorityLandscape.PriorityLandscapeName;
            PriorityLandscapeDescription = priorityLandscape.PriorityLandscapeDescriptionHtmlString;
            PriorityLandscapeID = priorityLandscape.PriorityLandscapeID;
        }

        public void UpdateModel(Models.PriorityLandscape priorityLandscape)
        {
            priorityLandscape.PriorityLandscapeDescriptionHtmlString = PriorityLandscapeDescription;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}