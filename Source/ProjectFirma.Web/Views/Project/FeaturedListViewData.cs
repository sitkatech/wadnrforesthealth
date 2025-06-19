/*-----------------------------------------------------------------------
<copyright file="FeaturedListViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Views.Project
{
    public class FeaturedListViewData : FirmaViewData
    {
        public readonly FeaturesListProjectGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;

        public FeaturedListViewData(Person currentPerson, Models.FirmaPage firmaPage) : base(currentPerson, firmaPage)
        {
            PageTitle = $"Featured {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}";
            GridName = "featuredListGrid";
            GridSpec = new FeaturesListProjectGridSpec(currentPerson)
            {
                ObjectNameSingular = $"Featured {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"Featured {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true,
                CreateEntityModalDialogForm = new ModalDialogForm(SitkaRoute<ProjectController>.BuildUrlFromExpression(tc => tc.EditFeaturedProjects()), $"Edit Featured {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}"),
                CreateEntityActionPhrase = $"Add/Remove Featured {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}"
            };
            GridDataUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(tc => tc.FeaturedListGridJsonData());
        }
    }

    public class FeaturesListProjectGridSpec : BasicProjectInfoGridSpec
    {
        public FeaturesListProjectGridSpec(Person currentPerson)
            : base(currentPerson, true)
        {
            Add("# of Photos", x => x.ProjectImages.Count, 100);
        }
    }
}
