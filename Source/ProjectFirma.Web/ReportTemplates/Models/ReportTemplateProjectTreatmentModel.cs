﻿using ProjectFirma.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFirma.Web.ReportTemplates.Models
{
    public class ReportTemplateProjectTreatmentModel : ReportTemplateBaseModel
    {
        private Project Project { get; set; }
        private Treatment ProjectTreatment { get; set; }

        //public string TreatmentCode { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public DateTime? EndDate { get; set; }
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string FootprintAcres { get; set; }
        public string TreatedAcres { get; set; }
        public decimal? CostPerAcre { get; set; }

        public decimal? TotalCostFootprint
        {
            get
            {
                if (CostPerAcre.HasValue)
                    return ProjectTreatment.TreatmentFootprintAcres * CostPerAcre.Value;
                
                return null;
            }
        }

        public decimal? TotalCostTreated
        {
            get
            {
                if (CostPerAcre.HasValue)
                    return ProjectTreatment.TreatmentTreatedAcres * CostPerAcre.Value;
                return null;
            }
        }

        public ReportTemplateProjectTreatmentModel(Treatment projectTreatment)
        {
            Project = projectTreatment.Project;
            ProjectTreatment = projectTreatment;

            //TreatmentCode = ProjectTreatment.TreatmentCode.TreatmentCodeDisplayName;
            Name = ProjectTreatment.TreatmentDetailedActivityType.TreatmentDetailedActivityTypeDisplayName;
            StartDate = ProjectTreatment.TreatmentStartDate;
            EndDate = ProjectTreatment.TreatmentEndDate;
            FootprintAcres = ProjectTreatment.TreatmentFootprintAcres.ToString();
            TreatedAcres = ProjectTreatment.TreatmentTreatedAcres.HasValue ? ProjectTreatment.TreatmentTreatedAcres.Value.ToString() : string.Empty;
            CostPerAcre = ProjectTreatment.CostPerAcre;
        }
    }
}