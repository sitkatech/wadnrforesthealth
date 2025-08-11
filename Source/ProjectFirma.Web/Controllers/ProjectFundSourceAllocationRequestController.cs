/*-----------------------------------------------------------------------
<copyright file="ProjectFundSourceAllocationRequestController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Views.ProjectFundSourceAllocationRequest;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectFundSourceAllocationRequestController : FirmaBaseController
    {
        [HttpGet]
        [ProjectEditAsAdminFeature]
        public PartialViewResult EditProjectFundSourceAllocationRequestsForProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var currentProjectFundSourceAllocationRequests = project.ProjectFundSourceAllocationRequests.ToList();
            Money projectEstimatedTotalCost = project.EstimatedTotalCost.GetValueOrDefault();

            var viewModel = new EditProjectFundSourceAllocationRequestsViewModel(project.ProjectID,currentProjectFundSourceAllocationRequests, true, projectEstimatedTotalCost, project.ProjectFundingSourceNotes, project.ProjectFundingSources.ToList());
            return ViewEditProjectFundSourceAllocationRequests(project, viewModel);
        }

        [HttpPost]
        [ProjectEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditProjectFundSourceAllocationRequestsForProject(ProjectPrimaryKey projectPrimaryKey, EditProjectFundSourceAllocationRequestsViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            var currentProjectFundSourceAllocationRequests = project.ProjectFundSourceAllocationRequests.ToList();
            if (!ModelState.IsValid)
            {
                return ViewEditProjectFundSourceAllocationRequests(project, viewModel);
            }
            return UpdateProjectFundSourceAllocationRequests(viewModel, currentProjectFundSourceAllocationRequests, project);
        }


        private static ActionResult UpdateProjectFundSourceAllocationRequests(EditProjectFundSourceAllocationRequestsViewModel viewModel,
                                                                         List<ProjectFundSourceAllocationRequest> currentProjectFundSourceAllocationRequests, 
                                                                         Project project)
        {
            HttpRequestStorage.DatabaseEntities.ProjectFundSourceAllocationRequests.Load();
            var allProjectFundSourceAllocationRequests = HttpRequestStorage.DatabaseEntities.ProjectFundSourceAllocationRequests.Local;

            HttpRequestStorage.DatabaseEntities.ProjectFundingSources.Load();
            var allProjectFundingSources = HttpRequestStorage.DatabaseEntities.ProjectFundingSources.Local;
            var currentProjectFundingSources = project.ProjectFundingSources.ToList();

            viewModel.UpdateModel(currentProjectFundSourceAllocationRequests, allProjectFundSourceAllocationRequests, project, currentProjectFundingSources, allProjectFundingSources);
            
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult ViewEditProjectFundSourceAllocationRequests(Project project, EditProjectFundSourceAllocationRequestsViewModel viewModel)
        {
            var allFundSourceAllocations = HttpRequestStorage.DatabaseEntities.FundSourceAllocations.ToList().Select(x => new FundSourceAllocationSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditProjectFundSourceAllocationRequestsViewData(new ProjectSimple(project), allFundSourceAllocations);
            return RazorPartialView<EditProjectFundSourceAllocationRequests, EditProjectFundSourceAllocationRequestsViewData, EditProjectFundSourceAllocationRequestsViewModel>(viewData, viewModel);
        }
    }
}