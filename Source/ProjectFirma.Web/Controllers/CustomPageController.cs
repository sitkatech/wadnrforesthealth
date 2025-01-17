﻿/*-----------------------------------------------------------------------
<copyright file="CustomPageController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.CustomPage;
using LtInfo.Common;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using MoreLinq;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Controllers
{
    public class CustomPageController : FirmaBaseController
    {
        [AnonymousUnclassifiedFeature]
        [Route("About/{vanityUrl}")]
        public ActionResult About(string vanityUrl)
        {
            var customPage = MultiTenantHelpers.GetAllCustomPages()
                .SingleOrDefault(x => x.CustomPageVanityUrl == vanityUrl);
            new CustomPageViewFeature().DemandPermission(CurrentPerson, customPage);
            var hasPermission = new CustomPageManageFeature().HasPermissionByPerson(CurrentPerson);
            var viewData = new DisplayPageContentViewData(CurrentPerson, customPage, hasPermission);
            return RazorView<DisplayPageContent, DisplayPageContentViewData>(viewData);
        }


        [FirmaPageViewListFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [FirmaPageViewListFeature]
        public GridJsonNetJObjectResult<CustomPage> IndexGridJsonData()
        {
            var gridSpec = new CustomPageGridSpec(new FirmaPageViewListFeature().HasPermissionByPerson(CurrentPerson));
            var hasPermissionByPerson = new CustomPageManageFeature().HasPermissionByPerson(CurrentPerson);
            var customPages = HttpRequestStorage.DatabaseEntities.CustomPages.ToList()
                .Where(x => hasPermissionByPerson)
                .OrderBy(x => x.CustomPageDisplayName)
                .ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<CustomPage>(customPages, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [CustomPageManageFeature]
        public PartialViewResult EditInDialog(CustomPagePrimaryKey customPagePrimaryKey)
        {
            var customPage = customPagePrimaryKey.EntityObject;
            var viewModel = new EditHtmlContentInDialogViewModel(customPage);
            return ViewEditInDialog(viewModel, customPage);
        }

        [HttpPost]
        [CustomPageManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditInDialog(CustomPagePrimaryKey customPagePrimaryKey, EditHtmlContentInDialogViewModel viewModel)
        {
            var customPage = customPagePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditInDialog(viewModel, customPage);
            }
            viewModel.UpdateModel(customPage);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditInDialog(EditHtmlContentInDialogViewModel viewModel, CustomPage customPage)
        {
            var editorToolbar = TinyMCEExtension.TinyMCEToolbarStyle.All;
            var viewData = new EditHtmlContentInDialogViewData(editorToolbar);
            return RazorPartialView<EditHtmlContentInDialog, EditHtmlContentInDialogViewData, EditHtmlContentInDialogViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [CustomPageManageFeature]
        public PartialViewResult CustomPageDetails(CustomPagePrimaryKey customPagePrimaryKey)
        {
            var customPage = customPagePrimaryKey.EntityObject;
            var customPageContentHtmlString = customPage.CustomPageContentHtmlString;
            if (!customPage.HasPageContent)
            {
                customPageContentHtmlString = new HtmlString(string.Format("No page content for Page \"{0}\".", customPage.CustomPageDisplayName));
            }
            var viewData = new CustomPageDetailsViewData(customPageContentHtmlString);
            return RazorPartialView<CustomPageDetails, CustomPageDetailsViewData>(viewData);
        }


        [HttpGet]
        [FirmaPageViewListFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditViewModel();
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [FirmaPageViewListFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            var customPage = new CustomPage(string.Empty, string.Empty, CustomPageDisplayType.Disabled, CustomPageNavigationSection.About);
            viewModel.UpdateModel(customPage, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.CustomPages.Add(customPage);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"Custom About Page '{customPage.CustomPageDisplayName}' successfully created.");

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [CustomPageManageFeature]
        public PartialViewResult Edit(CustomPagePrimaryKey customPagePrimaryKey)
        {
            var customPage = customPagePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(customPage);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [CustomPageManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(CustomPagePrimaryKey customPagePrimaryKey, EditViewModel viewModel)
        {
            var customPage = customPagePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            viewModel.UpdateModel(customPage, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var customPageTypesAsSelectListItems = CustomPageDisplayType.All.OrderBy(x => x.CustomPageDisplayTypeDisplayName)
                .ToSelectListWithEmptyFirstRow(x => x.CustomPageDisplayTypeID.ToString(CultureInfo.InvariantCulture),
                    x => x.CustomPageDisplayTypeDisplayName);
            var customPageNavigationSections = CustomPageNavigationSection.All.ToSelectListWithEmptyFirstRow(
                x => x.CustomPageNavigationSectionID.ToString(CultureInfo.InvariantCulture),
                y => y.CustomPageNavigationSectionName);
                      
            var viewData = new EditViewData(customPageTypesAsSelectListItems, customPageNavigationSections);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [CustomPageManageFeature]
        public PartialViewResult DeleteCustomPage(CustomPagePrimaryKey customPagePrimaryKey)
        {
            var customPage = customPagePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(customPage.CustomPageID);
            return ViewDeleteCustomPage(customPage, viewModel);
        }

        private PartialViewResult ViewDeleteCustomPage(CustomPage customPage, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = true;
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete the Custom About Page '{customPage.CustomPageDisplayName}'?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("About Page", SitkaRoute<CustomPageController>.BuildLinkFromExpression(x => x.About(customPage.CustomPageVanityUrl), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [CustomPageManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteCustomPage(CustomPagePrimaryKey customPagePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var customPage = customPagePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteCustomPage(customPage, viewModel);
            }
            SetMessageForDisplay($"Custom About Page '{customPage.CustomPageDisplayName}' successfully removed.");

            customPage.DeleteFull(HttpRequestStorage.DatabaseEntities);
            return new ModalDialogFormJsonResult();
        }
    }
}
