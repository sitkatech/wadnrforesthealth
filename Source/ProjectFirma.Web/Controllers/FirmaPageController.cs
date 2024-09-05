﻿/*-----------------------------------------------------------------------
<copyright file="FirmaPageController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.FirmaPage;
using LtInfo.Common;
using LtInfo.Common.MvcResults;

namespace ProjectFirma.Web.Controllers
{
    public class FirmaPageController : FirmaBaseController
    {
        [FirmaPageViewListFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [FirmaPageViewListFeature]
        public GridJsonNetJObjectResult<FirmaPage> IndexGridJsonData()
        {
            var hasPermission = new FirmaPageManageFeature().HasPermissionByPerson(CurrentPerson);
            var gridSpec = new FirmaPageGridSpec(new FirmaPageViewListFeature().HasPermissionByPerson(CurrentPerson));
            var firmaPages = HttpRequestStorage.DatabaseEntities.FirmaPages.ToList()
                .Where(x => hasPermission)
                .OrderBy(x => x.FirmaPageType.FirmaPageTypeDisplayName)
                .ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FirmaPage>(firmaPages, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [FirmaPageManageFeature]
        public PartialViewResult EditInDialog(FirmaPagePrimaryKey firmaPagePrimaryKey)
        {
            var firmaPage = firmaPagePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(firmaPage);
            return ViewEditInDialog(viewModel, firmaPage);
        }

        [HttpPost]
        [FirmaPageManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditInDialog(FirmaPagePrimaryKey firmaPagePrimaryKey, EditViewModel viewModel)
        {
            var firmaPage = firmaPagePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditInDialog(viewModel, firmaPage);
            }
            viewModel.UpdateModel(firmaPage);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditInDialog(EditViewModel viewModel, FirmaPage firmaPage)
        {
            var editorToolbar = firmaPage.FirmaPageType.FirmaPageRenderType == FirmaPageRenderType.PageContent ? TinyMCEExtension.TinyMCEToolbarStyle.AllOnOneRowNoMaximize : TinyMCEExtension.TinyMCEToolbarStyle.All;
            var viewData = new EditViewData(editorToolbar);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FirmaPageManageFeature]
        public PartialViewResult FirmaPageDetails(FirmaPagePrimaryKey firmaPagePrimaryKey)
        {
            var firmaPage = firmaPagePrimaryKey.EntityObject;
            var firmaPageContentHtmlString = firmaPage.FirmaPageContentHtmlString;
            if (!firmaPage.HasPageContent)
            {
                firmaPageContentHtmlString = new HtmlString(string.Format("No page content for Page \"{0}\".", firmaPage.FirmaPageType.FirmaPageTypeDisplayName));
            }
            var viewData = new FirmaPageDetailsViewData(firmaPageContentHtmlString);
            return RazorPartialView<FirmaPageDetails, FirmaPageDetailsViewData>(viewData);
        }
    }
}
