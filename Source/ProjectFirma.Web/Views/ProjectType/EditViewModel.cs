/*-----------------------------------------------------------------------
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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.ProjectType
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        public int ProjectTypeID { get; set; }

        [Required]
        [StringLength(Models.ProjectType.FieldLengths.ProjectTypeName)]
        [DisplayName("Name")]
        public string ProjectTypeName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectTypeDescription)]
        [Required]
        public HtmlString ProjectTypeDescription { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.TaxonomyBranch)]
        public int TaxonomyBranchID { get; set; }

        public string ThemeColor { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.LimitVisibilityToAdmin)]
        public bool LimitVisibilityToAdmin { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.ProjectType projectType)
        {
            ProjectTypeID = projectType.ProjectTypeID;
            ProjectTypeName = projectType.ProjectTypeName;
            ProjectTypeDescription = projectType.ProjectTypeDescriptionHtmlString;
            TaxonomyBranchID = projectType.TaxonomyBranchID;
            ThemeColor = projectType.ThemeColor;
            LimitVisibilityToAdmin = projectType.LimitVisibilityToAdmin;
        }

        public void UpdateModel(Models.ProjectType projectType, Person currentPerson)
        {
            projectType.ProjectTypeName = ProjectTypeName;
            projectType.ProjectTypeDescriptionHtmlString = ProjectTypeDescription;
            projectType.TaxonomyBranchID = !MultiTenantHelpers.IsTaxonomyLevelLeaf()
                ? TaxonomyBranchID
                : HttpRequestStorage.DatabaseEntities.TaxonomyBranches.First().TaxonomyBranchID; // really should only be one
            ;
            projectType.ThemeColor = ThemeColor;
            projectType.LimitVisibilityToAdmin = LimitVisibilityToAdmin;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            var existingProjectTypes = HttpRequestStorage.DatabaseEntities.ProjectTypes.ToList();
            if (!Models.ProjectType.IsProjectTypeNameUnique(existingProjectTypes, ProjectTypeName, ProjectTypeID))
            {
                errors.Add(new SitkaValidationResult<EditViewModel, string>("Name already exists", x => x.ProjectTypeName));
            }

            return errors;
        }
    }
}
