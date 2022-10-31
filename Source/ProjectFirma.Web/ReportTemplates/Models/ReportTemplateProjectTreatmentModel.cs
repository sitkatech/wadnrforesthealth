using ProjectFirma.Web.Models;
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

        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public DateTime? EndDate { get; set; }
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public decimal FootprintAcres { get; set; }
        public string FootprintAcresDisplay(int decimalPlaces = 3) => ProjectTreatment.TreatmentFootprintAcres.ToString($"N{decimalPlaces}");
        public decimal? TreatedAcres { get; set; }
        public string TreatedAcresDisplay(int decimalPlaces = 3) => ProjectTreatment.TreatmentTreatedAcres.HasValue ? ProjectTreatment.TreatmentTreatedAcres.Value.ToString($"N{decimalPlaces}") : string.Empty;
        public decimal? CostPerAcre { get; set; }
        public string CostPerAcreDisplay(int decimalPlaces = 2) => CostPerAcre.HasValue ? CostPerAcre.Value.ToString($"C{decimalPlaces}") : string.Empty;

        public decimal? TotalCostFootprint
        {
            get
            {
                if (CostPerAcre.HasValue)
                    return ProjectTreatment.TreatmentFootprintAcres * CostPerAcre.Value;
                
                return null;
            }
        }
        public string TotalCostFootprintDisplay(int decimalPlaces = 2) => TotalCostFootprint.HasValue ? TotalCostFootprint.Value.ToString($"C{decimalPlaces}") : string.Empty;

        public decimal? TotalCostTreated
        {
            get
            {
                if (CostPerAcre.HasValue)
                    return ProjectTreatment.TreatmentTreatedAcres * CostPerAcre.Value;
                return null;
            }
        }
        public string TotalCostTreatedDisplay(int decimalPlaces = 2) => TotalCostTreated.HasValue ? TotalCostTreated.Value.ToString($"C{decimalPlaces}") : string.Empty;

        public ReportTemplateProjectTreatmentModel(Treatment projectTreatment)
        {
            Project = projectTreatment.Project;
            ProjectTreatment = projectTreatment;

            Code = ProjectTreatment.TreatmentCode?.TreatmentCodeDisplayName;
            Name = ProjectTreatment.TreatmentDetailedActivityType.TreatmentDetailedActivityTypeDisplayName;
            StartDate = ProjectTreatment.TreatmentStartDate;
            EndDate = ProjectTreatment.TreatmentEndDate;
            FootprintAcres = ProjectTreatment.TreatmentFootprintAcres;
            TreatedAcres = ProjectTreatment.TreatmentTreatedAcres;
            CostPerAcre = ProjectTreatment.CostPerAcre;
        }
    }
}