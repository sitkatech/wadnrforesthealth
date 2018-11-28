using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Models
{
    public class TreatmentActivitySimple
    {
        public int? TreatmentActivityID { get; set; }

        [Required(ErrorMessage="Project is required.")]
        public int? ProjectID { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DisplayName("Start Date")]
        public DateTime? TreatmentActivityStartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime? TreatmentActivityEndDate { get; set; }

        [Required(ErrorMessage = "Grant is required.")]
        public int? FundingSourceID { get; set; }

        public string TreatmentActivityNotes { get; set; }

        [Required(ErrorMessage = "Grant Cost is required.")]
        [Range(0d, 50000000)]
        public decimal? TreatmentActivityGrantCost { get; set; }

        [Required(ErrorMessage = "Total Cost is required.")]
        [Range(0d, 50000000)]
        public decimal? TreatmentActivityTotalCost { get; set; }

        [Required(ErrorMessage = "Allocated Amount is required.")]
        [Range(0d, 50000000)]
        public decimal? TreatmentActivityAllocatedAmount { get; set; }

        [Required(ErrorMessage = "Prescribed Burn Acres is required.")]
        [Range(0d, 50000000)]
        public decimal? TreatmentActivityPrescribedBurnAcres { get; set; }

        [Required(ErrorMessage = "Slash Acres is required.")]
        [Range(0d, 50000000)]
        public decimal? TreatmentActivitySlashAcres { get; set; }

        [Required(ErrorMessage = "Pruning Acres is required.")]
        [Range(0d, 50000000)]
        public decimal? TreatmentActivityPruningAcres { get; set; }

        [Required(ErrorMessage = "Thinning Acres is required.")]
        [Range(0d, 50000000)]
        public decimal? TreatmentActivityThinningAcres { get; set; }

        [Required(ErrorMessage = "Brush Control Acres is required.")]
        [Range(0d, 50000000)]
        public decimal? TreatmentActivityBrushControlAcres { get; set; }

        [Required(ErrorMessage = "Footprint Acres is required.")]
        [Range(0d, 50000000)]
        public decimal? TreatmentActivityFootprintAcres { get; set; }

        // Needed by ModelBinder
        public TreatmentActivitySimple()
        {

        }

        public TreatmentActivitySimple(TreatmentActivity y)
        {
            TreatmentActivityID = y.TreatmentActivityID;
            TreatmentActivityEndDate = y.TreatmentActivityEndDate;
            TreatmentActivityNotes = y.TreatmentActivityNotes;
            TreatmentActivityStartDate = y.TreatmentActivityStartDate;
            TreatmentActivityFootprintAcres = y.TreatmentActivityFootprintAcres;
            TreatmentActivityBrushControlAcres = y.TreatmentActivityBrushControlAcres;
            TreatmentActivityThinningAcres = y.TreatmentActivityThinningAcres;
            TreatmentActivityPruningAcres = y.TreatmentActivityPruningAcres;
            TreatmentActivitySlashAcres = y.TreatmentActivitySlashAcres;
            TreatmentActivityPrescribedBurnAcres = y.TreatmentActivityPrescribedBurnAcres;
            TreatmentActivityAllocatedAmount = y.TreatmentActivityAllocatedAmount;
            TreatmentActivityTotalCost = y.TreatmentActivityTotalCost;
            TreatmentActivityGrantCost = y.TreatmentActivityGrantCost;
            FundingSourceID = y.FundingSourceID;
            ProjectID = y.ProjectID;
        }

        public TreatmentActivity ToTreatmentActivity()
        {
            // None of the nullables will ever default, thanks to RequiredAttribute
            return new TreatmentActivity(TreatmentActivityID ?? ModelObjectHelpers.NotYetAssignedID,
                ProjectID.GetValueOrDefault(),
                FundingSourceID.GetValueOrDefault(),
                TreatmentActivityFootprintAcres.GetValueOrDefault(),
                TreatmentActivityBrushControlAcres.GetValueOrDefault(),
                TreatmentActivityThinningAcres.GetValueOrDefault(),
                TreatmentActivityPruningAcres.GetValueOrDefault(),
                TreatmentActivitySlashAcres.GetValueOrDefault(),
                TreatmentActivityPrescribedBurnAcres.GetValueOrDefault(),
                TreatmentActivityAllocatedAmount.GetValueOrDefault(),
                TreatmentActivityTotalCost.GetValueOrDefault(),
                TreatmentActivityGrantCost.GetValueOrDefault(),
                TreatmentActivityStartDate.GetValueOrDefault(),
                TreatmentActivityEndDate,
                TreatmentActivityNotes);
        }
    }
}
