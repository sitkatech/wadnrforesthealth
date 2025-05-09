﻿/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.ComponentModel.DataAnnotations;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.ProjectImageUpdate
{
    public class EditViewModel : FormViewModel
    {
        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.PhotoCaption)]
        [StringLength(Models.ProjectImageUpdate.FieldLengths.Caption)]
        public string Caption { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.PhotoCredit)]
        [StringLength(Models.ProjectImageUpdate.FieldLengths.Credit)]
        public string Credit { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.PhotoTiming)]
        public int? ProjectImageTimingID { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.ExcludeFromFactSheet)]
        public bool ExcludeFromFactSheet { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.ProjectImageUpdate projectImageUpdate)
        {
            Caption = projectImageUpdate.Caption;
            Credit = projectImageUpdate.Credit;
            ProjectImageTimingID = projectImageUpdate.ProjectImageTimingID;
            ExcludeFromFactSheet = projectImageUpdate.ExcludeFromFactSheet;
        }

        public virtual void UpdateModel(Models.ProjectImageUpdate projectImageUpdate, Person person)
        {
            projectImageUpdate.Caption = Caption;
            projectImageUpdate.Credit = Credit;
            projectImageUpdate.ProjectImageTimingID = ProjectImageTimingID;
            projectImageUpdate.ExcludeFromFactSheet = ExcludeFromFactSheet;
        }
    }
}
