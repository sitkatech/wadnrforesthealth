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

namespace ProjectFirma.Web.Views.GisProjectBulkUpdate
{
    public class UploadGisFileViewData : GisImportViewData
    {
        public string UploadUrl { get; set; }
        public string CheckInfoUrl { get; set; }

        public string ProgramName { get; set; }
        public string ProgramUrl { get; set; }
        public Models.Program Program { get; set; }
        public bool ShowExampleGdbDownload { get; set; }

		public UploadGisFileViewData(Person currentPerson,
            GisUploadAttempt gisUploadAttempt
            , GisImportSectionStatus gisImportSectionStatus
            , string uploadUrl)
            : base(currentPerson, gisUploadAttempt, GisUploadAttemptWorkflowSection.UploadGisFile.GisUploadAttemptWorkflowSectionDisplayName, gisImportSectionStatus)
        {
            UploadUrl = uploadUrl;
            CheckInfoUrl = SitkaRoute<GisProjectBulkUpdateController>.BuildUrlFromExpression(c => c.CheckStatusOfGisUploadAttempt(gisUploadAttempt.GisUploadAttemptID));
            ProgramUrl = gisUploadAttempt?.GisUploadSourceOrganization?.Program?.GetDetailUrl();
            ProgramName = gisUploadAttempt?.GisUploadSourceOrganization?.Program?.DisplayName;
            ShowExampleGdbDownload = gisUploadAttempt?.GisUploadSourceOrganization?.Program
                ?.ProgramExampleGeospatialUploadFileResourceID.HasValue ?? false;
            Program = gisUploadAttempt?.GisUploadSourceOrganization?.Program;
        }


    }
}
