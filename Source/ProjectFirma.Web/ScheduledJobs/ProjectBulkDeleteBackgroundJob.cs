using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Hangfire;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.GisProjectBulkUpdate;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class ProjectBulkDeleteBackgroundJob : ScheduledBackgroundJobBase
    {
        public static ProjectBulkDeleteBackgroundJob Instance;


        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        static ProjectBulkDeleteBackgroundJob()
        {
            Instance = new ProjectBulkDeleteBackgroundJob();
        }

        public ProjectBulkDeleteBackgroundJob() : base("Project Bulk Delete Background Job", ConcurrencySetting.RunJobByItself)
        {
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            
        }

        private void BulkDeleteProjectsImplementation()
        {

        }


    }
}
