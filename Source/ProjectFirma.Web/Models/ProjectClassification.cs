﻿/*-----------------------------------------------------------------------
<copyright file="ProjectClassification.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Common;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectClassification : IEntityClassification, IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var projectDeleted = HttpRequestStorage.DatabaseEntities.Projects.Find(ProjectID);
                var classificationDeleted = HttpRequestStorage.DatabaseEntities.Classifications.Find(ClassificationID);
                var projectName = projectDeleted != null ? projectDeleted.AuditDescriptionString : ViewUtilities.NotFoundString;
                var classificationName = classificationDeleted != null ? classificationDeleted.AuditDescriptionString : ViewUtilities.NotFoundString;
                return string.Format("Project: {0}, Classification: {1}", projectName, classificationName);
            }
        }

        public ProjectClassification(int projectID, int classificationID, string projectClassificationNotes)
            : this(projectID, classificationID)
        {                        
            ProjectClassificationNotes = projectClassificationNotes;
        }
        
    }
}
