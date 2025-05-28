/*-----------------------------------------------------------------------
<copyright file="ProjectCreateViewData.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Models;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.ProjectCreate;

namespace ProjectFirma.Web.Views.GisProjectBulkUpdate
{
    public abstract class GisImportViewData : FirmaViewData
    {
        public Models.GisUploadAttempt GisUploadAttempt { get; }
        public string CurrentSectionDisplayName { get; }
        public List<ProjectCreateSection> ProjectCreateSections { get; }
        public string InitialUploadUrl { get; }
        public string GisMetadataUrl { get; }
        public string GeospatialValidationUrl { get; }
        public string ProvideFeedbackUrl { get; }
        public string SubmitUrl { get; }
        public string ApproveUrl { get; }
        public string WithdrawUrl { get; }
        public bool CurrentPersonIsSubmitter { get; }
        public bool CurrentPersonIsApprover { get; }
        public GisImportSectionStatus GisImportSectionStatus { get; }
        public bool CanAdvanceStage { get; }
        public bool GisUploadAttemptStateIsValidInWizard { get; }
        public bool CurrentPersonCanWithdraw { get; set; }
        public bool IsInstructionsPage { get; }
        public string InstructionsPageUrl { get;  }
        public List<GisUploadAttemptWorkflowSectionGrouping> GisUploadAttemptWorkflowSectionGroupings { get; }
        public ProjectStage ProjectStage { get; set; }

        protected GisImportViewData(Person currentPerson,
            Models.GisUploadAttempt gisUploadAttempt,
            string currentSectionDisplayName,
            GisImportSectionStatus gisImportSectionStatus) : this(gisUploadAttempt, currentPerson, currentSectionDisplayName)
        {
            IsInstructionsPage = currentSectionDisplayName.Equals("Instructions", StringComparison.InvariantCultureIgnoreCase);
            Check.Assert(gisUploadAttempt != null, "Project should be created in database by this point so it cannot be null.");


            CurrentPersonCanWithdraw = new GisUploadAttemptCreateFeature().HasPermission(currentPerson, gisUploadAttempt).HasPermission;

            GisUploadAttempt = gisUploadAttempt;
            GisImportSectionStatus = gisImportSectionStatus;
            CanAdvanceStage = GisImportSectionStatus.AreAllSectionsValidForGisUploadAttempt(gisUploadAttempt);
            // ReSharper disable PossibleNullReferenceException
            GisUploadAttemptStateIsValidInWizard = true;

            InstructionsPageUrl = SitkaRoute<GisProjectBulkUpdateController>.BuildUrlFromExpression(x =>
                    x.InstructionsGisImport(gisUploadAttempt.GisUploadAttemptID));



            PageTitle = $"GIS Bulk Project Upload";

            GeospatialValidationUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.Detail(gisUploadAttempt.GisUploadAttemptID));
            InitialUploadUrl = SitkaRoute<GisProjectBulkUpdateController>.BuildUrlFromExpression(x => x.UploadGisFile(gisUploadAttempt.GisUploadAttemptID));
            GisMetadataUrl = SitkaRoute<GisProjectBulkUpdateController>.BuildUrlFromExpression(x => x.GisMetadata(gisUploadAttempt.GisUploadAttemptID));
            
            ApproveUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Approve(gisUploadAttempt.GisUploadAttemptID));
            WithdrawUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Withdraw(gisUploadAttempt.GisUploadAttemptID));
        }



        private GisImportViewData(Models.GisUploadAttempt gisUploadAttempt, Person currentPerson, string currentSectionDisplayName) : base(currentPerson)
        {
            EntityName = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()}";
            CurrentPersonIsSubmitter = gisUploadAttempt.GisUploadAttemptCreatePersonID == currentPerson.PersonID;
            CurrentPersonIsApprover = gisUploadAttempt.GisUploadAttemptCreatePersonID == currentPerson.PersonID;
            GisUploadAttemptWorkflowSectionGroupings = GisUploadAttemptWorkflowSectionGrouping.All.OrderBy(x => x.SortOrder).ToList();
            CurrentSectionDisplayName = currentSectionDisplayName;
        }
    }
}
