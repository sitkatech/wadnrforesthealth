/*-----------------------------------------------------------------------
<copyright file="ProjectFundingSourceRequestController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.ProjectFundingSourceRequest;
using LtInfo.Common.MvcResults;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectFundingSourceRequestController : FirmaBaseController
    {
        [HttpGet]
        [ProjectEditAsAdminFeature]
        public PartialViewResult EditProjectFundingSourceRequestsForProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var currentProjectFundingSourceRequests = project.ProjectFundingSourceRequests.ToList();
            Money projectEstimatedTotalCost = project.EstimatedTotalCost.GetValueOrDefault();
            Money projectEstimatedIndirectCost = project.EstimatedIndirectCost.GetValueOrDefault();
            Money projectEstimatedPersonnelAndBenefitsCost = project.EstimatedPersonnelAndBenefitsCost.GetValueOrDefault();
            Money projectEstimatedSuppliesCost = project.EstimatedSuppliesCost.GetValueOrDefault();
            Money projectEstimatedTravelCost = project.EstimatedTravelCost.GetValueOrDefault();

            var viewModel = new EditProjectFundingSourceRequestsViewModel(currentProjectFundingSourceRequests, true, projectEstimatedTotalCost, projectEstimatedIndirectCost, projectEstimatedPersonnelAndBenefitsCost, projectEstimatedSuppliesCost, projectEstimatedTravelCost);
            return ViewEditProjectFundingSourceRequests(project, viewModel);
        }

        [HttpPost]
        [ProjectEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditProjectFundingSourceRequestsForProject(ProjectPrimaryKey projectPrimaryKey, EditProjectFundingSourceRequestsViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            var currentProjectFundingSourceRequests = project.ProjectFundingSourceRequests.ToList();
            if (!ModelState.IsValid)
            {
                return ViewEditProjectFundingSourceRequests(project, viewModel);
            }
            return UpdateProjectFundingSourceRequests(viewModel, currentProjectFundingSourceRequests, project);
        }


        private static ActionResult UpdateProjectFundingSourceRequests(
            EditProjectFundingSourceRequestsViewModel viewModel,
            List<ProjectFundingSourceRequest> currentProjectFundingSourceRequests, Project project)
        {
            HttpRequestStorage.DatabaseEntities.ProjectFundingSourceRequests.Load();
            var allProjectFundingSourceRequests = HttpRequestStorage.DatabaseEntities.ProjectFundingSourceRequests.Local;
            viewModel.UpdateModel(currentProjectFundingSourceRequests, allProjectFundingSourceRequests, project);

            
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditProjectFundingSourceRequests(GrantAllocation grantAllocation,
                                                                       EditProjectFundingSourceRequestsViewModel viewModel)
        {
            var allProjects = HttpRequestStorage.DatabaseEntities.Projects.ToList().GetActiveProjects().Select(x => new ProjectSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditProjectFundingSourceRequestsViewData(new FundingSourceSimple(grantAllocation), allProjects);
            return RazorPartialView<EditProjectFundingSourceRequests, EditProjectFundingSourceRequestsViewData, EditProjectFundingSourceRequestsViewModel>(viewData, viewModel);
        }

        private PartialViewResult ViewEditProjectFundingSourceRequests(Project project, EditProjectFundingSourceRequestsViewModel viewModel)
        {
            var allGrantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.ToList().Select(x => new FundingSourceSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditProjectFundingSourceRequestsViewData(new ProjectSimple(project), allGrantAllocations);
            return RazorPartialView<EditProjectFundingSourceRequests, EditProjectFundingSourceRequestsViewData, EditProjectFundingSourceRequestsViewModel>(viewData, viewModel);
        }
    }
}