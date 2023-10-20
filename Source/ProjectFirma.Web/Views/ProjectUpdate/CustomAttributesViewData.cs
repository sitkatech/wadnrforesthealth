/*-----------------------------------------------------------------------
<copyright file="BasicsViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Views.ProjectCreate;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class CustomAttributesViewData : ProjectUpdateViewData
    {
        public bool HasThreeTierTaxonomy { get; private set; }
        public bool ShowProjectStageDropDown { get; }
        public IEnumerable<Models.ProjectCustomAttributeType> ProjectCustomAttributeTypes { get; private set; }
        public Models.ProjectUpdate ProjectUpdate { get; }
        public string RefreshUrl { get; }
        public string DiffUrl { get; }

        public readonly SectionCommentsViewData SectionCommentsViewData;

        public CustomAttributesViewData(Person currentPerson,
            Models.ProjectUpdate projectUpdate,
            UpdateStatus updateStatus,
            ProjectAttributeValidationResult projectAttributeValidationResult,
            IEnumerable<Models.ProjectCustomAttributeType> projectCustomAttributeTypes)
            : base(currentPerson, projectUpdate.ProjectUpdateBatch, updateStatus, projectAttributeValidationResult.GetWarningMessages(), "ProjectAttributes")// 10/20/2023 AM & TK : previous code called this, removed from lookup to skip section temporarily ProjectUpdateSection.ProjectAttributes.ProjectUpdateSectionDisplayName
        {
            ProjectUpdate = projectUpdate;
            DiffUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.DiffProjectAttributes(Project));
            RefreshUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.RefreshProjectAttributes(Project));
            ProjectCustomAttributeTypes = projectCustomAttributeTypes;
            SectionCommentsViewData =
                new SectionCommentsViewData(projectUpdate.ProjectUpdateBatch.ProjectAttributesComment, projectUpdate.ProjectUpdateBatch.IsReturned);
        }
    }
}
