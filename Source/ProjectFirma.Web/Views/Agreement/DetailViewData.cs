/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using JetBrains.Annotations;
using LtInfo.Common;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Agreement
{
    public class DetailViewData : FirmaViewData
    {
        public AgreementPersonGridSpec AgreementPersonGridSpec { get; }
        public string AgreementPersonGridName { get; }
        public string AgreementPersonGridDataUrl { get; }
        public bool UserHasEditAgreementPermissions { get; set; }
        public bool ShowDownload { get; }
        public string EditAgreementFundSourceAllocationRelationshipsUrl { get; }
        [NotNull]
        public List<Models.ProjectCode> ProjectCodes { get; }
        [NotNull]
        public List<Models.ProgramIndex> ProgramIndices { get; }
        public Models.Agreement Agreement { get; }
        public string EditAgreementUrl { get; set; }
        public string BackToAgreementsText { get; set; }
        public string AgreementsListUrl { get; set; }
        public List<AgreementFundSourceAllocation> CurrentAgreementFundSourceAllocationsInSortedOrder { get; }
        public List<AgreementProject> AgreementProjects { get; }
        public string EditAgreementProjectsUrl { get; }



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
                EditAgreementFundSourceAllocationRelationshipsUrl = SitkaRoute<AgreementController>.BuildUrlFromExpression(t => t.EditAgreementFundSourceAllocationRelationships(agreement.AgreementID));
            }

            AgreementPersonGridName = "agreementPersonGrid";
            AgreementPersonGridDataUrl = SitkaRoute<AgreementController>.BuildUrlFromExpression(ac => ac.AgreementPersonGridJsonData(agreement.AgreementID));

            List<AgreementFundSourceAllocation> agreementFundSourceAllocationsList = agreement.AgreementFundSourceAllocations.ToList();
            CurrentAgreementFundSourceAllocationsInSortedOrder = AgreementFundSourceAllocation.OrderAgreementFundSourceAllocationsByYearPrefixedFundSourceNumbersThenEverythingElse(agreementFundSourceAllocationsList);

            AgreementProjects = agreement.AgreementProjects.ToList();
            EditAgreementProjectsUrl = SitkaRoute<AgreementController>.BuildUrlFromExpression(t => t.EditAgreementProjects(agreement.AgreementID));
        }
    }
}
