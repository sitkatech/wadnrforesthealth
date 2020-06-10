/*-----------------------------------------------------------------------
<copyright file="BasicsViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Views.GisProjectBulkUpdate
{
    public class GisMetadataViewData : GisImportViewData
    {

        public readonly string GridName;
        public readonly string GridDataUrl;

        public GisRecordGridSpec GisRecordGridSpec { get; set; }

        public GisMetadataViewData(Person currentPerson,
            GisUploadAttempt gisUploadAttempt
            , GisImportSectionStatus gisImportSectionStatus
            , GisRecordGridSpec gisRecordGridSpec)
            : base(currentPerson, gisUploadAttempt, GisUploadAttemptWorkflowSection.ValidateMetadata.GisUploadAttemptWorkflowSectionDisplayName, gisImportSectionStatus)
        {
            GisRecordGridSpec = gisRecordGridSpec;
            GridDataUrl = SitkaRoute<GisProjectBulkUpdateController>.BuildUrlFromExpression(tc => tc.GisRecordGridJsonData(gisUploadAttempt.GisUploadAttemptID));
            GridName = "GisRecordGrid";
        }


    }
}
