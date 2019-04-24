/*-----------------------------------------------------------------------
<copyright file="ProjectGrantAllocationRequestController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Views.ProjectGrantAllocationRequest;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectGrantAllocationRequestController : FirmaBaseController
    {
        [HttpGet]
        [ProjectEditAsAdminFeature]
        public PartialViewResult EditProjectGrantAllocationRequestsForProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var currentProjectGrantAllocationRequests = project.ProjectGrantAllocationRequests.ToList();
            Money projectEstimatedTotalCost = project.EstimatedTotalCost.GetValueOrDefault();

            var viewModel = new EditProjectGrantAllocationRequestsViewModel(currentProjectGrantAllocationRequests, true, projectEstimatedTotalCost);
            return ViewEditProjectGrantAllocationRequests(project, viewModel);
        }

        [HttpPost]
        [ProjectEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditProjectGrantAllocationRequestsForProject(ProjectPrimaryKey projectPrimaryKey, EditProjectGrantAllocationRequestsViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            var currentProjectGrantAllocationRequests = project.ProjectGrantAllocationRequests.ToList();
            if (!ModelState.IsValid)
            {
                return ViewEditProjectGrantAllocationRequests(project, viewModel);
            }
            return UpdateProjectGrantAllocationRequests(viewModel, currentProjectGrantAllocationRequests, project);
        }


        private static ActionResult UpdateProjectGrantAllocationRequests(EditProjectGrantAllocationRequestsViewModel viewModel,
                                                                       List<ProjectGrantAllocationRequest> currentProjectGrantAllocationRequests, 
                                                                       Project project)
        {
            HttpRequestStorage.DatabaseEntities.ProjectGrantAllocationRequests.Load();
            var allProjectGrantAllocationRequests = HttpRequestStorage.DatabaseEntities.ProjectGrantAllocationRequests.Local;
            viewModel.UpdateModel(currentProjectGrantAllocationRequests, allProjectGrantAllocationRequests, project);
            
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditProjectGrantAllocationRequests(GrantAllocation grantAllocation,
                                                                       EditProjectGrantAllocationRequestsViewModel viewModel)
        {
            var allProjects = HttpRequestStorage.DatabaseEntities.Projects.ToList().GetActiveProjects().Select(x => new ProjectSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditProjectGrantAllocationRequestsViewData(new GrantAllocationSimple(grantAllocation), allProjects);
            return RazorPartialView<EditProjectGrantAllocationRequests, EditProjectGrantAllocationRequestsViewData, EditProjectGrantAllocationRequestsViewModel>(viewData, viewModel);
        }

        private PartialViewResult ViewEditProjectGrantAllocationRequests(Project project, EditProjectGrantAllocationRequestsViewModel viewModel)
        {
            var allGrantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.ToList().Select(x => new GrantAllocationSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditProjectGrantAllocationRequestsViewData(new ProjectSimple(project), allGrantAllocations);
            return RazorPartialView<EditProjectGrantAllocationRequests, EditProjectGrantAllocationRequestsViewData, EditProjectGrantAllocationRequestsViewModel>(viewData, viewModel);
        }
    }
}