/*-----------------------------------------------------------------------
<copyright file="ProjectController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Web.Mvc;
using LtInfo.Common.ExcelWorkbookUtilities;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Views.Grant;

namespace ProjectFirma.Web.Controllers
{
    public class GrantAllocationController : FirmaBaseController
    {
        //[HttpGet]
        //[GrantAllocationEditAsAdminFeature]
        //public PartialViewResult Edit(GrantAllocationPrimaryKey grantAllocationPrimaryKey)
        //{
        //    var grantAllocation = grantAllocationPrimaryKey.EntityObject;
        //    var viewModel = new EditGrantAllocationViewModel(grantAllocation);
        //    return ViewEdit(viewModel, grantAllocation, EditGrantType.ExistingGrant);
        //}

        //[HttpPost]
        //[GrantAllocationEditAsAdminFeature]
        //[AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        //public ActionResult Edit(GrantPrimaryKey grantPrimaryKey, EditGrantViewModel viewModel)
        //{
        //    var grant = grantPrimaryKey.EntityObject;
        //    if (!ModelState.IsValid)
        //    {
        //        return ViewEdit(viewModel, grant, EditGrantType.ExistingGrant);
        //    }
        //    viewModel.UpdateModel(grant, CurrentPerson);
        //    return new ModalDialogFormJsonResult();
        //}

        //private PartialViewResult ViewEdit(EditGrantViewModel viewModel, GrantAllocation grantAllocation, EditGrantType editGrantType)
        //{
        //    var organizations = HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations();

        //    var viewData = new EditGrantAllocationViewData(editGrantType,
        //        organizations
        //    );
        //    return RazorPartialView<EditGrantAllocation, EditGrantAllocationViewData, EditGrantAllocationViewModel>(viewData, viewModel);
        //}

        [GrantAllocationsViewFeature]
        public ViewResult GrantAllocationDetail(int grantAllocationID)
        {
            var grantAllocation = HttpRequestStorage.DatabaseEntities.GrantAllocations.Single(g => g.GrantAllocationID == grantAllocationID);
            var viewData = new Views.GrantAllocation.DetailViewData(CurrentPerson, grantAllocation);
            return RazorView<Views.GrantAllocation.Detail, Views.GrantAllocation.DetailViewData>(viewData);
        }

        //[GrantsViewFullListFeature]
        //public ViewResult Index()
        //{
        //    var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.FullProjectList);
        //    var viewData = new GrantIndexViewData(CurrentPerson, firmaPage);
        //    return RazorView<GrantIndex, GrantIndexViewData>(viewData);
        //}
    }
}
