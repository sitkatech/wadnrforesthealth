﻿/*-----------------------------------------------------------------------
<copyright file="TaxonomyTierPerformanceMeasureController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.TaxonomyTierPerformanceMeasure;

namespace ProjectFirma.Web.Controllers
{
    public class TaxonomyTierPerformanceMeasureController : FirmaBaseController
    {
        [HttpGet]
        [TaxonomyTierPerformanceMeasureManageFeature]
        public PartialViewResult Edit(PerformanceMeasurePrimaryKey performanceMeasurePrimaryKey)
        {
            var performanceMeasure = performanceMeasurePrimaryKey.EntityObject;
            var taxonomyTierPerformanceMeasureSimples = performanceMeasure.GetTaxonomyTiers().Select(x =>
                    new TaxonomyTierPerformanceMeasureSimple(x.Key.TaxonomyTierID, x.First().PerformanceMeasureID,
                        x.First().IsPrimaryProjectType)).ToList();
            var primaryTaxonomyTierID = performanceMeasure.GetPrimaryTaxonomyTier()?.TaxonomyTierID;
            var viewModel = new EditViewModel(taxonomyTierPerformanceMeasureSimples, primaryTaxonomyTierID);
            return ViewEdit(viewModel, performanceMeasure);
        }

        [HttpPost]
        [TaxonomyTierPerformanceMeasureManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(PerformanceMeasurePrimaryKey performanceMeasurePrimaryKey, EditViewModel viewModel)
        {
            var performanceMeasure = performanceMeasurePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, performanceMeasure);
            }
            HttpRequestStorage.DatabaseEntities.ProjectTypePerformanceMeasures.Load();
            viewModel.UpdateModel(performanceMeasure.ProjectTypePerformanceMeasures.ToList(), HttpRequestStorage.DatabaseEntities.ProjectTypePerformanceMeasures.Local);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel, PerformanceMeasure performanceMeasure)
        {
            var associatePerformanceMeasureTaxonomyLevel = MultiTenantHelpers.GetAssociatePerformanceMeasureTaxonomyLevel();
            var taxonomyBranchSimples = associatePerformanceMeasureTaxonomyLevel.GetTaxonomyTiers().OrderBy(p => p.DisplayName).ToList().Select(x => new TaxonomyTier(x)).ToList();
            var viewData = new EditViewData(new PerformanceMeasureSimple(performanceMeasure), taxonomyBranchSimples, associatePerformanceMeasureTaxonomyLevel);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }
    }
}
