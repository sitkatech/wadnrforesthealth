/*-----------------------------------------------------------------------
<copyright file="TagController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Data;
using System.Data.SqlClient;
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
        public GridJsonNetJObjectResult<vArcOnlineRawJsonImportIndex> JobIndexGridJsonData()
        {
            var hasJobDeletePermissions = new FirmaAdminFeature().HasPermissionByPerson(CurrentPerson);
            var gridSpec = new JobIndexGridSpec(hasJobDeletePermissions);
            var priorJobs = HttpRequestStorage.DatabaseEntities.vArcOnlineRawJsonImportIndices.OrderBy(x => x.CreateDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vArcOnlineRawJsonImportIndex>(priorJobs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpPost]
        [JobManageFeature]
        public ActionResult ClearPriorDataMartRawJsonImports()
        {
            int priorJobCount = HttpRequestStorage.DatabaseEntities.ArcOnlineFinanceApiRawJsonImports.Count();

            ArcOnlineFinanceApiUpdateBackgroundJob.ClearArcOnlineFinanceApiRawJsonImportsTable();
            var message = $"{priorJobCount} Prior ArcOnline Json Imports cleared";
            SetMessageForDisplay(message);

            HttpRequestStorage.DatabaseEntities.SaveChanges();
            return RedirectToAction(new SitkaRoute<JobController>(x => x.JobIndex()));
        }

        [HttpPost]
        [JobManageFeature]
        public ActionResult RunVendorImportJob()
        {
            lock (ScheduledBackgroundJobBase.ScheduledBackgroundGlobalJobLock)
            {
                var vendorImportJob = new VendorImportHangfireBackgroundJob();
                vendorImportJob.DownloadArcOnlineVendorTable();
            }

            var message = $"Vendors Imported";
            SetMessageForDisplay(message);

            return RedirectToAction(new SitkaRoute<JobController>(x => x.JobIndex()));
        }

        [HttpPost]
        [JobManageFeature]
        public ActionResult RunProgramIndexImportJob()
        {
            lock (ScheduledBackgroundJobBase.ScheduledBackgroundGlobalJobLock)
            {
                var programIndexJob = new ProgramIndexImportHangfireBackgroundJob();
                programIndexJob.DownloadArcOnlineProgramIndexTable();
            }

            var message = $"Program Indices Imported";
            SetMessageForDisplay(message);

            return RedirectToAction(new SitkaRoute<JobController>(x => x.JobIndex()));
        }

        [HttpPost]
        [JobManageFeature]
        public ActionResult RunProjectCodeImportJob()
        {
            lock (ScheduledBackgroundJobBase.ScheduledBackgroundGlobalJobLock)
            {
                var projectCodeJob = new ProjectCodeImportHangfireBackgroundJob();
                projectCodeJob.DownloadArcOnlineFinanceApiProjectCodeTable();
            }

            var message = $"ArcOnline Finance API Project Codes Imported";
            SetMessageForDisplay(message);

            return RedirectToAction(new SitkaRoute<JobController>(x => x.JobIndex()));
        }

        [HttpPost]
        [JobManageFeature]
        public ActionResult RunFundSourceExpendituresImportJob()
        {
            lock (ScheduledBackgroundJobBase.ScheduledBackgroundGlobalJobLock)
            {
                var fundSourceExpenditureJob = new FundSourceExpenditureImportHangfireBackgroundJob();
                fundSourceExpenditureJob.DownloadFundSourceExpendituresTableForAllFiscalYears();
            }

            var message = $"FundSource Expenditures Imported";
            SetMessageForDisplay(message);

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
            var viewData = new JsonApiLandingPageViewData(CurrentPerson, firmaPage);
            return RazorView<JsonApiLandingPage, JsonApiLandingPageViewData>(viewData);
        }

    }
}
