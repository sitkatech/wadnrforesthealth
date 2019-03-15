/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Linq;
using JetBrains.Annotations;
using LtInfo.Common;
using LtInfo.Common.ModalDialog;
using MoreLinq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.ProjectFunding;
using ProjectFirma.Web.Views.ProjectUpdate;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.ExpenditureAndBudgetControls;
using ProjectFirma.Web.Views.Shared.PerformanceMeasureControls;
using ProjectFirma.Web.Views.Shared.ProjectControls;
using ProjectFirma.Web.Views.Shared.ProjectDocument;
using ProjectFirma.Web.Views.Shared.ProjectLocationControls;
using ProjectFirma.Web.Views.Shared.ProjectOrganization;
using ProjectFirma.Web.Views.Shared.ProjectPerson;
using ProjectFirma.Web.Views.Shared.TextControls;
using ProjectFirma.Web.Views.TreatmentActivity;

namespace ProjectFirma.Web.Views.Agreement
{
    public class DetailViewData : FirmaViewData
    {
        public AgreementPersonGridSpec AgreementPersonGridSpec { get; }
        public string AgreementPersonGridName { get; }
        public string AgreementPersonGridDataUrl { get; }
        public bool UserHasEditAgreementPermissions { get; set; }
        public bool ShowDownload { get; }
        public string EditAgreementGrantAllocationRelationshipsUrl { get; }
        [NotNull]
        public List<Models.ProjectCode> ProjectCodes { get; }
        [NotNull]
        public List<ProgramIndex> ProgramIndices { get; }
        public Models.Agreement Agreement { get; }
        public string EditAgreementUrl { get; set; }
        public string BackToAgreementsText { get; set; }
        public string AgreementsListUrl { get; set; }


        public DetailViewData(Person currentPerson, Models.Agreement agreement, bool userHasEditAgreementPermissions)
            : base(currentPerson, null)
        {
            Agreement = agreement;
            HtmlPageTitle = agreement.AgreementTitle;
            EntityName = $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabel()}";
            EditAgreementUrl = agreement.GetEditUrl();
            BackToAgreementsText = "Back to all Agreements";
            AgreementsListUrl = SitkaRoute<AgreementController>.BuildUrlFromExpression(c => c.Index());

            PageTitle = agreement.AgreementTitle.ToEllipsifiedStringClean(110);
            BreadCrumbTitle = $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabel()} Detail";
            UserHasEditAgreementPermissions = userHasEditAgreementPermissions;
            // Used for creating file download link, if file available
            ShowDownload = agreement.AgreementFileResource != null;

            ProgramIndices = agreement.ProgramIndices;
            ProjectCodes = agreement.ProjectCodes;

            AgreementPersonGridSpec = new AgreementPersonGridSpec(currentPerson) { ObjectNameSingular = "Agreement Contact", ObjectNamePlural = "Agreement Contacts", SaveFiltersInCookie = true };

            if (UserHasEditAgreementPermissions)
            {
                var contentUrl = SitkaRoute<AgreementController>.BuildUrlFromExpression(t => t.NewAgreementPerson(agreement.AgreementID));
                AgreementPersonGridSpec.CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, "Create a new Agreement Contact");
                EditAgreementGrantAllocationRelationshipsUrl = SitkaRoute<AgreementController>.BuildUrlFromExpression(t => t.EditAgreementGrantAllocationRelationships(agreement.AgreementID));
            }

            AgreementPersonGridName = "agreementPersonGrid";
            AgreementPersonGridDataUrl = SitkaRoute<AgreementController>.BuildUrlFromExpression(ac => ac.AgreementPersonGridJsonData(agreement.AgreementID));
        }
    }
}
