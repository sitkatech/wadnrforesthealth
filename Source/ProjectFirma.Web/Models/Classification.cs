﻿/*-----------------------------------------------------------------------
<copyright file="Classification.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class Classification : IAuditableEntity, IHaveASortOrder
    {
        public List<Project> GetAssociatedProjects(Person currentPerson)
        {
            return ProjectClassifications.Select(ptc => ptc.Project).ToList().GetActiveProjectsVisibleToUser(currentPerson).ToList();
        }

        public string KeyImageUrlLarge => KeyImageFileResource != null ? KeyImageFileResource.FileResourceUrlScaledForPrint : "http://placehold.it/280x210";

        public string GetDeleteUrl()
        {
            return SitkaRoute<ClassificationController>.BuildUrlFromExpression(c => c.DeleteClassification(ClassificationID));
        }

        public string KeyImageScaledForThumbnail
        {
            get { return SitkaRoute<FileResourceController>.BuildUrlFromExpression(x => x.GetFileResourceResized(KeyImageFileResource.FileResourceGUIDAsString, 287, 180)); }
        }

        public static bool IsDisplayNameUnique(IEnumerable<Classification> classifications, string displayName, int currentClassificationID)
        {
            var classification = classifications.SingleOrDefault(x => x.ClassificationID != currentClassificationID && String.Equals(x.DisplayName, displayName, StringComparison.InvariantCultureIgnoreCase));
            return classification == null;
        }

        public string AuditDescriptionString => DisplayName;

        public int? SortOrder
        {
            get => ClassificationSortOrder;
            set => ClassificationSortOrder = value;
        }

        public int ID => ClassificationID;
    }
}
