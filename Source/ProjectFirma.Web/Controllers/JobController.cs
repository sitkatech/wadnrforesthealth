/*-----------------------------------------------------------------------
<copyright file="TagController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.ScheduledJobs;
using ProjectFirma.Web.Views.Job;
using ProjectFirma.Web.Views.JsonApiManagement;

namespace ProjectFirma.Web.Controllers
{
    public class JobController : FirmaBaseController
    {
        [JobManageFeature]
        public ViewResult JobIndex()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.TagList);
            var viewData = new JobIndexViewData(CurrentPerson, firmaPage);
            return RazorView<JobIndex, JobIndexViewData>(viewData);
        }

        [JobManageFeature]
        public GridJsonNetJObjectResult<vSocrataDataMartRawJsonImportIndex> JobIndexGridJsonData()
        {
            var hasJobDeletePermissions = new FirmaAdminFeature().HasPermissionByPerson(CurrentPerson);
            var gridSpec = new JobIndexGridSpec(hasJobDeletePermissions);
            var priorJobs = HttpRequestStorage.DatabaseEntities.vSocrataDataMartRawJsonImportIndices.OrderBy(x => x.CreateDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vSocrataDataMartRawJsonImportIndex>(priorJobs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [JobManageFeature]
        public ActionResult RunVendorImportJob()
        {
            var socrataJob = new SocrataDataMartUpdateBackgroundJob("Socrata Vendor Import (Button activated)");
            socrataJob.DownloadSocrataVendorTable();
            return RedirectToAction(new SitkaRoute<JobController>(x => x.JobIndex()));
        }

        [JobManageFeature]
        public ActionResult RunProgramIndexImportJob()
        {
            var socrataJob = new SocrataDataMartUpdateBackgroundJob("Socrata Program Index Import (Button activated)");
            socrataJob.DownloadSocrataProgramIndexTable();
            return RedirectToAction(new SitkaRoute<JobController>(x => x.JobIndex()));
        }

        [JobManageFeature]
        public ActionResult RunProjectCodeImportJob()
        {
            var socrataJob = new SocrataDataMartUpdateBackgroundJob("Socrata Project Code Import (Button activated)");
            socrataJob.DownloadSocrataProjectCodeTable();
            return RedirectToAction(new SitkaRoute<JobController>(x => x.JobIndex()));
        }

        [JobManageFeature]
        public ActionResult RunGrantExpendituresImportJob()
        {
            var socrataJob = new SocrataDataMartUpdateBackgroundJob("Grant Expenditures Import (Button activated)");
            socrataJob.DownloadGrantExpendituresTable();
            return RedirectToAction(new SitkaRoute<JobController>(x => x.JobIndex()));
        }


        /// <summary>
        /// Maybe this deserves its own controller, but it's here for now.
        /// Move if you feel the urge.
        /// </summary>
        /// <returns></returns>
        [ViewJsonApiLandingPageFeature]
        public ViewResult JsonApiLandingPage()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.TagList);
            var viewData = new Views.JsonApiManagement.JsonApiLandingPageViewData(CurrentPerson, firmaPage);
            return RazorView<JsonApiLandingPage, JsonApiLandingPageViewData>(viewData);
        }

    }
}
