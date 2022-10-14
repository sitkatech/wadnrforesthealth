using ProjectFirma.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFirma.Web.ReportTemplates.Models
{
    public class ReportTemplateProjectCountyModel : ReportTemplateBaseModel
    {
        public Project Project { get; set; }
        public ProjectCounty ProjectCounty { get; set; }
        public County County { get; set; }

        public string CountyName { get; set; }

        public ReportTemplateProjectCountyModel(ProjectCounty projectCounty)
        {
            Project = projectCounty.Project;
            ProjectCounty = projectCounty;
            County = projectCounty.County;

            CountyName = County.CountyName;
        }
    }
}