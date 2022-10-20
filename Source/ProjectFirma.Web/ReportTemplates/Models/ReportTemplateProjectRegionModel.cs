using ProjectFirma.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFirma.Web.ReportTemplates.Models
{
    public class ReportTemplateProjectRegionModel : ReportTemplateBaseModel
    {
        private Project Project { get; set; }
        private ProjectRegion ProjectRegion { get; set; }
        private DNRUplandRegion DNRUplandRegion { get; set; }

        public string Abbreviation { get; set; }
        public string Name { get; set; }

        public ReportTemplateProjectRegionModel(ProjectRegion projectRegion)
        {
            Project = projectRegion.Project;
            ProjectRegion = projectRegion;
            DNRUplandRegion = projectRegion.DNRUplandRegion;

            Abbreviation = projectRegion.DNRUplandRegion.DNRUplandRegionAbbrev;
            Name = projectRegion.DNRUplandRegion.DisplayName;
        }
    }
}