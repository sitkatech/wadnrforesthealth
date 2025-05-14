/*-----------------------------------------------------------------------
<copyright file="FirmaPageType.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class FirmaPageType
    {
        public abstract string GetViewUrl();
    }

    public partial class FirmaPageTypeHomePage
    {
        public override string GetViewUrl() => SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.Index());
    }

    public partial class FirmaPageTypeDemoScript
    {
        public override string GetViewUrl() => SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.DemoScript());
    }

    public partial class FirmaPageTypeInternalSetupNotes
    {
        public override string GetViewUrl() => SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.InternalSetupNotes());
    }

    public partial class FirmaPageTypeHomeAdditionalInfo
    {
        public override string GetViewUrl() => SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.Index());
    }

    public partial class FirmaPageTypeHomeMapInfo
    {
        public override string GetViewUrl() => SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.Index());
    }

    public partial class FirmaPageTypeFullProjectList
    {
        public override string GetViewUrl() => SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.Index());
    }

    public partial class FirmaPageTypeProjectTypeList
    {
        public override string GetViewUrl() => SitkaRoute<ProjectTypeController>.BuildUrlFromExpression(x => x.Index());
    }

    public partial class FirmaPageTypeTaxonomyTrunkList
    {
        public override string GetViewUrl() => SitkaRoute<TaxonomyTrunkController>.BuildUrlFromExpression(x => x.Index());
    }

    public partial class FirmaPageTypeOrganizationsList
    {
        public override string GetViewUrl() => SitkaRoute<OrganizationController>.BuildUrlFromExpression(x => x.Index());
    }

    public partial class FirmaPageTypeProgramsList
    {
        public override string GetViewUrl() => SitkaRoute<ProgramController>.BuildUrlFromExpression(x => x.Index());
    }

    public partial class FirmaPageTypeUploadLoaTabularDataExcel 
    {
        public override string GetViewUrl() => SitkaRoute<ExcelUploadController>.BuildUrlFromExpression(x => x.ManageExcelUploadsAndEtl());
    }


    public partial class FirmaPageTypeTaxonomyBranchList
    {
        public override string GetViewUrl() => SitkaRoute<TaxonomyBranchController>.BuildUrlFromExpression(x => x.Index());
    }

    public partial class FirmaPageTypeMyProjects
    {
        public override string GetViewUrl() => SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.MyProjectsRequiringAnUpdate());
    }

    public partial class FirmaPageTypeProjectMap
    {
        public override string GetViewUrl() => SitkaRoute<ResultsController>.BuildUrlFromExpression(x => x.ProjectMap());
    }

    public partial class FirmaPageTypeTaxonomy
    {
        public override string GetViewUrl() => SitkaRoute<ProgramInfoController>.BuildUrlFromExpression(x => x.Taxonomy());
    }

    public partial class FirmaPageTypeFeaturedProjectList
    {
        public override string GetViewUrl() => SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.FeaturedList());
    }

    public partial class FirmaPageTypeManageUpdateNotifications
    {
        public override string GetViewUrl() => SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.Manage());
    }

    public partial class FirmaPageTypeFullProjectListSimple
    {
        public override string GetViewUrl() => SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.FullProjectListSimple());
    }

    public partial class FirmaPageTypeTagList
    {
        public override string GetViewUrl() => SitkaRoute<TagController>.BuildUrlFromExpression(x => x.Index());
    }

    public partial class FirmaPageTypeProposals
    {
        public override string GetViewUrl() => SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.Proposed());
    }

    public partial class FirmaPageTypePendingProjects
    {
        public override string GetViewUrl() => SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.Pending());
    }

    public partial class FirmaPageTypeProposeProjectInstructions
    {
        public override string GetViewUrl() => SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.InstructionsProposal(null));
    }
    public partial class FirmaPageTypeEnterHistoricProjectInstructions
    {
        public override string GetViewUrl() => SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.InstructionsEnterHistoric(null));
    }

    public partial class FirmaPageTypeProjectStewardOrganizationList
    {
        public override string GetViewUrl() => SitkaRoute<ProjectStewardOrganizationController>.BuildUrlFromExpression(x => x.Index());
    }

    public partial class FirmaPageTypeTraining
    {
        public override string GetViewUrl() => SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.Training());
    }

    public partial class FirmaPageTypeCustomFooter
    {
        public override string GetViewUrl() => string.Empty;
    }


    public partial class FirmaPageTypeFactSheetCustomText
    {
        public override string GetViewUrl() => SitkaRoute<ProjectController>.BuildUrlFromExpression(c => c.Index());
    }

    public partial class FirmaPageTypeFocusAreasList
    {
        public override string GetViewUrl() => SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.Index());
    }
    public partial class FirmaPageTypeFullGrantList
    {
        public override string GetViewUrl() => SitkaRoute<GrantController>.BuildUrlFromExpression(c => c.Index());
    }

    public partial class FirmaPageTypeFullGrantAllocationList
    {
        public override string GetViewUrl() => SitkaRoute<GrantController>.BuildUrlFromExpression(c => c.Index());
    }

    public partial class FirmaPageTypePriorityLandscapesList
    {
        public override string GetViewUrl() => SitkaRoute<PriorityLandscapeController>.BuildUrlFromExpression(c => c.Index());
    }

    public partial class FirmaPageTypeRegionsList
    {
        public override string GetViewUrl() => SitkaRoute<DNRUplandRegionController>.BuildUrlFromExpression(c => c.Index());
    }

    public partial class FirmaPageTypeFullAgreementList
    {
        public override string GetViewUrl() => SitkaRoute<GrantController>.BuildUrlFromExpression(c => c.Index());
    }



    public partial class FirmaPageTypeInteractionEventList
    {
        public override string GetViewUrl() => SitkaRoute<InteractionEventController>.BuildUrlFromExpression(c => c.Index());
    }


    public partial class FirmaPageTypeGisUploadAttemptInstructions
    {
        public override string GetViewUrl() => string.Empty;
    }

    public partial class FirmaPageTypeDNRCostShareTreatments
    {
        public override string GetViewUrl() => string.Empty;
    }

    public partial class FirmaPageTypeManageFindYourForester
    {
        public override string GetViewUrl() => SitkaRoute<FindYourForesterController>.BuildUrlFromExpression(c => c.Manage(null));
    }
    public partial class FirmaPageTypeFindYourForester
    {
        public override string GetViewUrl() => SitkaRoute<FindYourForesterController>.BuildUrlFromExpression(c => c.Index());
    }
    public partial class FirmaPageTypeExternalMapLayers
    {
        public override string GetViewUrl() => SitkaRoute<MapLayerController>.BuildUrlFromExpression(c => c.Index());
    }

    public partial class FirmaPageTypeReports
    {
        public override string GetViewUrl() => SitkaRoute<ReportsController>.BuildUrlFromExpression(c => c.Index());
    }
    public partial class FirmaPageTypeReportProjects
    {
        public override string GetViewUrl() => SitkaRoute<ReportsController>.BuildUrlFromExpression(c => c.Projects());
    }
    public partial class FirmaPageTypeReportAddReport
    {
        public override string GetViewUrl() => SitkaRoute<ReportsController>.BuildUrlFromExpression(c => c.New());
    }

    public partial class FirmaPageTypeCounty
    {
        public override string GetViewUrl() => SitkaRoute<CountyController>.BuildUrlFromExpression(c => c.Index());
    }

    public partial class FirmaPageTypeVendor
    {
        public override string GetViewUrl() => SitkaRoute<VendorController>.BuildUrlFromExpression(c => c.Index());
    }
}
