﻿/*-----------------------------------------------------------------------
<copyright file="MapLayerController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.MapLayer;
using ProjectFirma.Web.Views.Shared;
using System.Linq;
using System.Web.Mvc;



namespace ProjectFirma.Web.Controllers
{
    public class MapLayerController : FirmaBaseController
    {

        [FirmaAdminFeature]
        public ViewResult Index()
        {
            var externalMapLayersFirmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.ExternalMapLayers);
            var externalMapLayerGridDataUrl = SitkaRoute<MapLayerController>.BuildUrlFromExpression(x => x.ExternalMapLayerGridJsonData());
            var userCanManage = new FirmaAdminFeature().HasPermissionByPerson(CurrentPerson);

            var viewData = new IndexViewData(CurrentPerson, externalMapLayersFirmaPage, externalMapLayerGridDataUrl, userCanManage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [FirmaAdminFeature]
        public GridJsonNetJObjectResult<ExternalMapLayer> ExternalMapLayerGridJsonData()
        {
            var userCanManage = new FirmaAdminFeature().HasPermissionByPerson(CurrentPerson);
            var gridSpec = new ExternalMapLayerGridSpec(userCanManage);
            var externalMapLayers = HttpRequestStorage.DatabaseEntities.ExternalMapLayers.OrderBy(x => x.DisplayName).ToList();

            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ExternalMapLayer>(externalMapLayers, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult New()
        {
            var externalMapLayer = new ExternalMapLayer(string.Empty, string.Empty, true, true, true, true, false);
            var viewModel = new EditExternalMapLayerViewModel(externalMapLayer);
            return ViewEditExternalMapLayer(viewModel);
        }

        [HttpPost]
        [FirmaAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditExternalMapLayerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditExternalMapLayer(viewModel);
            }
            var externalMapLayer = ExternalMapLayer.CreateNewBlank();
            viewModel.UpdateModel(externalMapLayer);
            HttpRequestStorage.DatabaseEntities.ExternalMapLayers.Add(externalMapLayer);
            SetMessageForDisplay($"External map layer {externalMapLayer.DisplayName} successfully created.");
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult EditExternalMapLayer(ExternalMapLayerPrimaryKey externalMapLayerPrimaryKey)
        {
            var externalMapLayer = externalMapLayerPrimaryKey.EntityObject;
            var viewModel = new EditExternalMapLayerViewModel(externalMapLayer);
            return ViewEditExternalMapLayer(viewModel);
        }

        [HttpPost]
        [FirmaAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditExternalMapLayer(ExternalMapLayerPrimaryKey externalMapLayerPrimaryKey, EditExternalMapLayerViewModel viewModel)
        {
            var externalMapLayer = externalMapLayerPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditExternalMapLayer(viewModel);
            }
            viewModel.UpdateModel(externalMapLayer);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditExternalMapLayer(EditExternalMapLayerViewModel viewModel)
        {
            var viewData = new EditExternalMapLayerViewData();
            return RazorPartialView<EditExternalMapLayer, EditExternalMapLayerViewData, EditExternalMapLayerViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult Delete(ExternalMapLayerPrimaryKey externalMapLayerPrimaryKey)
        {
            var externalMapLayer = externalMapLayerPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(externalMapLayer.ExternalMapLayerID);
            return ViewDelete(externalMapLayer, viewModel);
        }

        private PartialViewResult ViewDelete(ExternalMapLayer externalMapLayer, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete external map layer {externalMapLayer.DisplayName}?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FirmaAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(ExternalMapLayerPrimaryKey externalMapLayerPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var externalMapLayer = externalMapLayerPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(externalMapLayer, viewModel);
            }
            var message = $"External Map Layer \"{externalMapLayer.DisplayName}\" successfully deleted.";
            externalMapLayer.Delete(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);

            return new ModalDialogFormJsonResult();
        }

    }
}
