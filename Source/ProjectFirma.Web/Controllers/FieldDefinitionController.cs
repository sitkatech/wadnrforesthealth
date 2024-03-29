﻿/*-----------------------------------------------------------------------
<copyright file="FieldDefinitionController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web;
using System.Web.Mvc;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.FieldDefinition;
using ProjectFirma.Web.Views.Shared;
using LtInfo.Common;
using LtInfo.Common.MvcResults;

namespace ProjectFirma.Web.Controllers
{
    public class FieldDefinitionController : FirmaBaseController
    {
        [FieldDefinitionViewListFeature]
        [CrossAreaRoute]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [FieldDefinitionViewListFeature]
        [CrossAreaRoute]
        public GridJsonNetJObjectResult<FieldDefinition> IndexGridJsonData()
        {
            FieldDefinitionGridSpec gridSpec;
            var actions = GetFieldDefinitionsAndGridSpec(out gridSpec, CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FieldDefinition>(actions, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private static List<FieldDefinition> GetFieldDefinitionsAndGridSpec(out FieldDefinitionGridSpec gridSpec, Person currentPerson)
        {
            gridSpec = new FieldDefinitionGridSpec(new FieldDefinitionViewListFeature().HasPermissionByPerson(currentPerson));
            var hasPermission = new FieldDefinitionManageFeature().HasPermissionByPerson(currentPerson);
            return FieldDefinition.All.Where(x => hasPermission).OrderBy(x => x.GetFieldDefinitionLabel()).ToList();
        }

        [HttpGet]
        [FieldDefinitionManageFeature]
        [CrossAreaRoute]
        public ViewResult Edit(FieldDefinitionPrimaryKey fieldDefinitionPrimaryKey)
        {
            var fieldDefinitionData = HttpRequestStorage.DatabaseEntities.FieldDefinitionDatas.GetFieldDefinitionDataByFieldDefinition(fieldDefinitionPrimaryKey);            
            var viewModel = new EditViewModel(fieldDefinitionData);
            return ViewEdit(fieldDefinitionPrimaryKey, viewModel);
        }

        [HttpPost]
        [FieldDefinitionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        [CrossAreaRoute]
        public ActionResult Edit(FieldDefinitionPrimaryKey fieldDefinitionPrimaryKey, EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(fieldDefinitionPrimaryKey, viewModel);
            }
            var fieldDefinitionData = HttpRequestStorage.DatabaseEntities.FieldDefinitionDatas.GetFieldDefinitionDataByFieldDefinition(fieldDefinitionPrimaryKey);
            if (fieldDefinitionData == null)
            {
                fieldDefinitionData = new FieldDefinitionData(fieldDefinitionPrimaryKey.EntityObject);
                HttpRequestStorage.DatabaseEntities.FieldDefinitionDatas.Add(fieldDefinitionData);
            }

            viewModel.UpdateModel(fieldDefinitionData);
            SetMessageForDisplay("Field Definition successfully saved.");
            return RedirectToAction(new SitkaRoute<FieldDefinitionController>(x => x.Edit(fieldDefinitionData.FieldDefinition)));
        }

        private ViewResult ViewEdit(FieldDefinitionPrimaryKey fieldDefinitionPrimaryKey, EditViewModel viewModel)
        {
            var viewData = new EditViewData(CurrentPerson, fieldDefinitionPrimaryKey.EntityObject);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FieldDefinitionViewFeature]
        [CrossAreaRoute]
        public PartialViewResult FieldDefinitionDetails(int fieldDefinitionID)
        {
            var fieldDefinition = FieldDefinition.AllLookupDictionary[fieldDefinitionID];
            var fieldDefinitionData = HttpRequestStorage.DatabaseEntities.FieldDefinitionDatas.SingleOrDefault(x => x.FieldDefinitionID == fieldDefinitionID);
            var showEditLink = new FieldDefinitionManageFeature().HasPermissionByPerson(CurrentPerson);
            var editUrl = SitkaRoute<FieldDefinitionController>.BuildUrlFromExpression(t => t.Edit(fieldDefinition));
            var viewData = new FieldDefinitionDetailsViewData(fieldDefinitionData, showEditLink, editUrl, fieldDefinition.DefaultDefinitionHtmlString, fieldDefinition.GetFieldDefinitionLabel());
            return RazorPartialView<FieldDefinitionDetails, FieldDefinitionDetailsViewData>(viewData);
        }

        [HttpGet]
        [FieldDefinitionViewFeature]
        [CrossAreaRoute]
        public PartialViewResult FieldDefinitionDetailsForClassificationSystem(ClassificationSystemPrimaryKey classificationSystemPrimaryKey)
        {
            var classificationSystem = classificationSystemPrimaryKey.EntityObject;
            var viewData = new FieldDefinitionDetailsViewData(classificationSystem, (bool)false, (string)string.Empty, new HtmlString("<p>A logical system to group projects according to overarching program themes or goals.</p>"), classificationSystem.ClassificationSystemName);
            return RazorPartialView<FieldDefinitionDetails, FieldDefinitionDetailsViewData>(viewData);
        }
    }
}
