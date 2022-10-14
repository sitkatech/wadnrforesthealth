using ProjectFirma.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFirma.Web.ReportTemplates.Models
{
    public class ReportTemplateProjectRegionModel : ReportTemplateBaseModel
    {
        public Project Project { get; set; }
        public ProjectRegion ProjectRegion { get; set; }
        public DNRUplandRegion DNRUplandRegion { get; set; }

        public string DNRUplandRegionName { get; set; }

        public ReportTemplateProjectRegionModel(ProjectRegion projectRegion)
        {
            Project = projectRegion.Project;
            ProjectRegion = projectRegion;
            DNRUplandRegion = projectRegion.DNRUplandRegion;

            DNRUplandRegionName = projectRegion.DNRUplandRegion.DisplayName;
        }
    }
}