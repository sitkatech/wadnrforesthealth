/*-----------------------------------------------------------------------
<copyright file="FirmaViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.User;

namespace ProjectFirma.Web.Views
{
    public abstract class FirmaViewData
    {
        public List<LtInfoMenuItem> TopLevelLtInfoMenuItems { get; set; }
        public string FullProjectListUrl { get; }
        public string ProjectSearchUrl { get; }
        public string ProjectFindUrl { get; }
        public string PageTitle { get; set; }
        public string HtmlPageTitle { get; set; }
        public string BreadCrumbTitle { get; set; }
        public string EntityName { get; set; }
        public Models.FirmaPage FirmaPage { get; }
        public Person CurrentPerson { get; }
        public string FirmaHomeUrl { get; }
        public string LogInUrl { get; }
        public string LogOutUrl { get; }
        public string RequestSupportUrl { get; }
        public ViewPageContentViewData ViewPageContentViewData { get; }
        public LtInfoMenuItem HelpMenu { get; private set; }
        public ViewPageContentViewData CustomFooterViewData { get; }
        public string TenantName { get; }
        public string TenantDisplayName { get; }
        public string TenantBannerLogoUrl { get; }

        /// <summary>
        /// Call for page without associated FirmaPage
        /// </summary>
        protected FirmaViewData(Person currentPerson) : this(currentPerson, null)
        {
        }
     
        /// <summary>
        /// Call for page with associated FirmaPage
        /// </summary>
        protected FirmaViewData(Person currentPerson, Models.FirmaPage firmaPage)
        {
            FirmaPage = firmaPage;

            CurrentPerson = currentPerson;
            FirmaHomeUrl = SitkaRoute<HomeController>.BuildUrlFromExpression(c => c.Index());

            LogInUrl = FirmaHelpers.GenerateLogInUrlWithReturnUrl();
            LogOutUrl = FirmaHelpers.GenerateLogOutUrlWithReturnUrl();

            RequestSupportUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(c => c.Support());

            MakeFirmaMenu(currentPerson);

            FullProjectListUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(c => c.Index());
            ProjectSearchUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(c => c.Search(UrlTemplate.Parameter1String));
            ProjectFindUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(c => c.Find(string.Empty));

            var currentPersonCanManage = new FirmaPageManageFeature().HasPermissionByPerson(currentPerson);
            ViewPageContentViewData = firmaPage != null ? new ViewPageContentViewData(firmaPage, currentPersonCanManage) : null;
            CustomFooterViewData =
                new ViewPageContentViewData(Models.FirmaPage.GetFirmaPageByPageType(FirmaPageType.CustomFooter),
                    currentPersonCanManage);
            TenantName = MultiTenantHelpers.GetTenantName();
            TenantDisplayName = MultiTenantHelpers.GetTenantDisplayName();
            TenantBannerLogoUrl = MultiTenantHelpers.GetTenantBannerLogoUrl();
        }


        private void MakeFirmaMenu(Person currentPerson)
        {
            TopLevelLtInfoMenuItems = new List<LtInfoMenuItem>
            {
                BuildAboutMenu(currentPerson),
                BuildProjectsMenu(currentPerson),
                BuildFinancialsMenuItem(currentPerson),
                BuildProgramInfoMenu(currentPerson)
            };
            TopLevelLtInfoMenuItems.Add(BuildManageMenu(currentPerson));
            TopLevelLtInfoMenuItems.Add(BuildReportsMenu(currentPerson));

            TopLevelLtInfoMenuItems.ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-root-item" });
            TopLevelLtInfoMenuItems.SelectMany(x => x.ChildMenus).ToList().ForEach(x => x.ExtraTopLevelMenuCssClasses = new List<string> { "navigation-dropdown-item" });
            
            HelpMenu = new LtInfoMenuItem("Help");
            HelpMenu.AddMenuItem(LtInfoMenuItem.MakeItem("Request Support",
                ModalDialogFormHelper.ModalDialogFormLink("Request Support", RequestSupportUrl, "Request Support", 800,
                    "Submit Request", "Cancel", new List<string>(), null, null).ToString(), "ToolHelp"));
            //5/24/2022 TK & AM Commenting out until new training videos are added
            //HelpMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c=>c.Training()), currentPerson, "Training", "ToolHelp"));
            HelpMenu.AddMenuItem(LtInfoMenuItem.MakeItem("About ProjectFirma",@"<a href='http://www.sitkatech.com/products/ProjectFirma/projectfirma-faqs/' target='_blank'>About ProjectFirma <span class='glyphicon glyphicon-new-window'></span></a>", "ExternalHelp"));

            
        }

        private static LtInfoMenuItem BuildAboutMenu(Person currentPerson)
        {
            var aboutMenu = new LtInfoMenuItem("About");

            MultiTenantHelpers.GetCustomPagesByNavigationSection(CustomPageNavigationSectionEnum.About).ForEach(x =>
            {
                var isVisible = x.CustomPageDisplayType == CustomPageDisplayType.Public ||
                                (!currentPerson.IsAnonymousUser &&
                                 x.CustomPageDisplayType == CustomPageDisplayType.Protected);
                if (isVisible)
                {
                    aboutMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<CustomPageController>(c => c.About(x.CustomPageVanityUrl)), currentPerson, x.CustomPageDisplayName, "Group1"));
                }
                
            });
            return aboutMenu;
        }


        /// <summary>
        /// Financials Top-Level menu
        /// </summary>
        private static LtInfoMenuItem BuildFinancialsMenuItem(Person currentPerson)
        {
            var financialsMenu = new LtInfoMenuItem("Financials");

            financialsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<GrantController>(c => c.Index()), currentPerson, 
              $"Full {Models.FieldDefinition.Grant.GetFieldDefinitionLabel()} List", "Group1"));

            financialsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<AgreementController>(c => c.Index()), currentPerson, $"Full { Models.FieldDefinition.Agreement.GetFieldDefinitionLabel()} List", "Group2"));


            MultiTenantHelpers.GetCustomPagesByNavigationSection(CustomPageNavigationSectionEnum.Financials).ForEach(x =>
            {
                var isVisible = x.CustomPageDisplayType == CustomPageDisplayType.Public ||
                                (!currentPerson.IsAnonymousUser &&
                                 x.CustomPageDisplayType == CustomPageDisplayType.Protected);
                if (isVisible)
                {
                    financialsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<CustomPageController>(c => c.About(x.CustomPageVanityUrl)), currentPerson, x.CustomPageDisplayName, "Group4"));
                }

            });

            return financialsMenu;
        }

        private static LtInfoMenuItem BuildProgramInfoMenu(Person currentPerson)
        {
            var programInfoMenu = new LtInfoMenuItem("Program Info");

            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FindYourForesterController>(c => c.Index()), currentPerson, "Find Your Forester", "Group1"));
            if (new ProgramViewFeature().HasPermissionByPerson(currentPerson))
            {
                programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ProgramController>(c => c.Index()), currentPerson, $"{Models.FieldDefinition.Program.GetFieldDefinitionLabelPluralized()}", "Group1"));
            }

            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<PriorityLandscapeController>(c => c.Index()), currentPerson, "Priority Landscapes", "Group2"));
            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<DNRUplandRegionController>(c => c.Index()), currentPerson, Models.FieldDefinition.DNRUplandRegion.GetFieldDefinitionLabelPluralized(), "Group2"));
            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FocusAreaController>(c => c.Index()), currentPerson, Models.FieldDefinition.FocusArea.GetFieldDefinitionLabelPluralized(), "Group2"));
            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<CountyController>(c => c.Index()), currentPerson, $"{Models.FieldDefinition.County.GetFieldDefinitionLabelPluralized()}", "Group2"));

            if (MultiTenantHelpers.HasCanStewardProjectsOrganizationRelationship())
            {
                programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ProjectStewardOrganizationController>(c => c.Index()), currentPerson, $"{Models.FieldDefinition.ProjectStewardOrganizationDisplayName.GetFieldDefinitionLabelPluralized()}", "Group3"));
            }
            programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<OrganizationController>(c => c.Index()), currentPerson, $"{Models.FieldDefinition.Organization.GetFieldDefinitionLabelPluralized()}", "Group3"));

            if (new VendorViewFeature().HasPermissionByPerson(currentPerson))
            {
                programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<VendorController>(c => c.Index()), currentPerson, $"{Models.FieldDefinition.Vendor.GetFieldDefinitionLabelPluralized()}", "Group3"));
            }

            MultiTenantHelpers.GetCustomPagesByNavigationSection(CustomPageNavigationSectionEnum.ProgramInfo).ForEach(x =>
            {
                var isVisible = x.CustomPageDisplayType == CustomPageDisplayType.Public ||
                                (!currentPerson.IsAnonymousUser &&
                                 x.CustomPageDisplayType == CustomPageDisplayType.Protected);
                if (isVisible)
                {
                    programInfoMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<CustomPageController>(c => c.About(x.CustomPageVanityUrl)), currentPerson, x.CustomPageDisplayName, "Group4"));
                }

            });

            return programInfoMenu;
        }


        private static LtInfoMenuItem BuildManageMenu(Person currentPerson)
        {
            var manageMenu = new LtInfoMenuItem("Manage");

            // Group 1 - Project Classifications Stuff (taxonomies, classification systems, PMs)
            if (MultiTenantHelpers.IsTaxonomyLevelTrunk())
            {
                manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TaxonomyTrunkController>(c => c.Manage()), currentPerson, Models.FieldDefinition.TaxonomyTrunk.GetFieldDefinitionLabelPluralized(), "Group1"));
            }

            if (!MultiTenantHelpers.IsTaxonomyLevelLeaf())
            {
                manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TaxonomyBranchController>(c => c.Manage()), currentPerson, Models.FieldDefinition.TaxonomyBranch.GetFieldDefinitionLabelPluralized(), "Group1"));
            }
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ProjectTypeController>(c => c.Manage()), currentPerson, Models.FieldDefinition.ProjectType.GetFieldDefinitionLabelPluralized(), "Group1"));
            MultiTenantHelpers.GetClassificationSystems().ForEach(x =>
            {
                manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ClassificationController>(c => c.Index(x.ClassificationSystemID)), currentPerson, x.ClassificationSystemNamePluralized, "Group1"));
            });
            

            // Group 2 - System Config stuff
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<UserController>(c => c.Index((int)IndexGridSpec.UsersStatusFilterTypeEnum.AllActiveUsersAndContacts)), currentPerson, "Users and Contacts", "Group2"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ProjectController>(c => c.FeaturedList()), currentPerson, $"Featured {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", "Group2"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<TagController>(c => c.Index()), currentPerson, $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Tags", "Group2"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ProjectUpdateController>(c => c.Manage()), currentPerson, $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Updates", "Group2"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.ManageHomePageImages()), currentPerson, "Homepage Configuration", "Group2"));

            // Group 3 - Content Editing stuff
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FirmaPageController>(c => c.Index()), currentPerson, "Manage Page Content", "Group3"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FieldDefinitionController>(c => c.Index()), currentPerson, "Custom Labels & Definitions", "Group3"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<CustomPageController>(c => c.Index()), currentPerson, "Custom Pages", "Group3"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ProjectCustomAttributeTypeController>(c => c.Manage()), currentPerson, "Custom Attributes", "Group3"));

            // Group 4 - Other
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.InternalSetupNotes()), currentPerson, "Internal Setup Notes", "Group4"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<HomeController>(c => c.StyleGuide()), currentPerson, "Style Guide", "Group4"));
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ExcelUploadController>(c => c.ManageExcelUploadsAndEtl()), currentPerson, $"Upload Excel Files / ETL", "Group4"));

            // Group 5 - Organizations
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<OrganizationAndRelationshipTypeController>(c => c.Index()), currentPerson, Models.FieldDefinition.OrganizationType.GetFieldDefinitionLabelPluralized(), "Group5"));

            if (new FindYourForesterManageFeature().HasPermissionByPerson(currentPerson))
            {
                manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<FindYourForesterController>(c => c.Manage(null)), currentPerson, "Manage Find Your Forester", "Group5"));
            }

            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<MapLayerController>(c => c.Index()), currentPerson, "Map Layers", "Group5"));

            // Group 6 - Jobs
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<JobController>(c => c.JobIndex()), currentPerson, Models.FieldDefinition.Job.GetFieldDefinitionLabelPluralized(), "Group6"));

            // Group 7 - JSON APIs
            manageMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<JsonApiManagementController>(c => c.JsonApiLandingPage()), currentPerson, "JSON APIs", "Group7"));

            // Group 8 - Hangfire menu
            if (currentPerson.IsAdministrator())
            {
                LtInfoMenuItem hangfireMenuItem = new LtInfoMenuItem("/hangfire","Hangfire", true, false, "Group8");
                manageMenu.AddMenuItem(hangfireMenuItem);
            }

            return manageMenu;
        }


        private static LtInfoMenuItem BuildProjectsMenu(Person currentPerson)
        {
            var projectsMenu = new LtInfoMenuItem($"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}");
            projectsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ResultsController>(c => c.ProjectMap()), currentPerson, $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Map", "Group1"));
            projectsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ProjectController>(c => c.Index()), currentPerson, $"Full {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} List", "Group2"));
            projectsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ProgramInfoController>(c => c.Taxonomy()), currentPerson, MultiTenantHelpers.GetTaxonomySystemName(), "Group2"));
            MultiTenantHelpers.GetClassificationSystems().ForEach(x =>
            {
                projectsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ProgramInfoController>(c => c.ClassificationSystem(x.ClassificationSystemID)), currentPerson, x.ClassificationSystemDefinition, "Group2"));
            });
            projectsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ProjectController>(c => c.Proposed()), currentPerson, $"{Models.FieldDefinition.Application.GetFieldDefinitionLabelPluralized()}", "Group3"));
            projectsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ProjectController>(c => c.Pending()), currentPerson, $"Pending {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", "Group3"));

            if (new GrantAllocationAwardLandownerCostShareLineItemViewFeature().HasPermissionByPerson(currentPerson))
            {
                projectsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<GrantAllocationAwardLandownerCostShareLineItemController>(tac => tac.Index()), currentPerson, "DNR Cost Share Treatments", "Group4"));
            }

            projectsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<InteractionEventController>(iec => iec.Index()), currentPerson, $"Full {Models.FieldDefinition.InteractionEvent.GetFieldDefinitionLabelPluralized()} List", "Group5"));

            MultiTenantHelpers.GetCustomPagesByNavigationSection(CustomPageNavigationSectionEnum.Projects).ForEach(x =>
            {
                var isVisible = x.CustomPageDisplayType == CustomPageDisplayType.Public ||
                                (!currentPerson.IsAnonymousUser &&
                                 x.CustomPageDisplayType == CustomPageDisplayType.Protected);
                if (isVisible)
                {
                    projectsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<CustomPageController>(c => c.About(x.CustomPageVanityUrl)), currentPerson, x.CustomPageDisplayName, "Group6"));
                }

            });


            return projectsMenu;
        }

        private static LtInfoMenuItem BuildReportsMenu(Person currentPerson)
        {
            var reportsMenu = new LtInfoMenuItem("Reports");

            reportsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ReportsController>(c => c.Projects()), currentPerson, $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", "Group1"));

            if (new FirmaAdminFeature().HasPermissionByPerson(currentPerson))
            {
                reportsMenu.AddMenuItem(LtInfoMenuItem.MakeItem(new SitkaRoute<ReportsController>(c => c.Index()), currentPerson, "Manage Report Templates", "Group2"));
            }

            return reportsMenu;
        }

        public string GetBreadCrumbTitle()
        {
            if (!string.IsNullOrWhiteSpace(BreadCrumbTitle))
            {
                return $" | {BreadCrumbTitle}";
            }
            else if (!string.IsNullOrWhiteSpace(PageTitle))
            {
                return $" | {PageTitle}";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
