//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentActivity]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    [Table("[dbo].[TreatmentActivity]")]
    public partial class TreatmentActivity : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentActivity()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentActivity(int treatmentActivityID, int projectID, int fundingSourceID, decimal treatmentActivityFootprintAcres, decimal treatmentActivityBrushControlAcres, decimal treatmentActivityThinningAcres, decimal treatmentActivityPruningAcres, decimal treatmentActivitySlashAcres, decimal treatmentActivityPrescribedBurnAcres, decimal treatmentActivityAllocatedAmount, decimal treatmentActivityTotalCost, decimal treatmentActivityGrantCost, DateTime treatmentActivityStartDate, DateTime? treatmentActivityEndDate, string treatmentActivityNotes) : this()
        {
            this.TreatmentActivityID = treatmentActivityID;
            this.ProjectID = projectID;
            this.FundingSourceID = fundingSourceID;
            this.TreatmentActivityFootprintAcres = treatmentActivityFootprintAcres;
            this.TreatmentActivityBrushControlAcres = treatmentActivityBrushControlAcres;
            this.TreatmentActivityThinningAcres = treatmentActivityThinningAcres;
            this.TreatmentActivityPruningAcres = treatmentActivityPruningAcres;
            this.TreatmentActivitySlashAcres = treatmentActivitySlashAcres;
            this.TreatmentActivityPrescribedBurnAcres = treatmentActivityPrescribedBurnAcres;
            this.TreatmentActivityAllocatedAmount = treatmentActivityAllocatedAmount;
            this.TreatmentActivityTotalCost = treatmentActivityTotalCost;
            this.TreatmentActivityGrantCost = treatmentActivityGrantCost;
            this.TreatmentActivityStartDate = treatmentActivityStartDate;
            this.TreatmentActivityEndDate = treatmentActivityEndDate;
            this.TreatmentActivityNotes = treatmentActivityNotes;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentActivity(int projectID, int fundingSourceID, decimal treatmentActivityFootprintAcres, decimal treatmentActivityBrushControlAcres, decimal treatmentActivityThinningAcres, decimal treatmentActivityPruningAcres, decimal treatmentActivitySlashAcres, decimal treatmentActivityPrescribedBurnAcres, decimal treatmentActivityAllocatedAmount, decimal treatmentActivityTotalCost, decimal treatmentActivityGrantCost, DateTime treatmentActivityStartDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentActivityID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.FundingSourceID = fundingSourceID;
            this.TreatmentActivityFootprintAcres = treatmentActivityFootprintAcres;
            this.TreatmentActivityBrushControlAcres = treatmentActivityBrushControlAcres;
            this.TreatmentActivityThinningAcres = treatmentActivityThinningAcres;
            this.TreatmentActivityPruningAcres = treatmentActivityPruningAcres;
            this.TreatmentActivitySlashAcres = treatmentActivitySlashAcres;
            this.TreatmentActivityPrescribedBurnAcres = treatmentActivityPrescribedBurnAcres;
            this.TreatmentActivityAllocatedAmount = treatmentActivityAllocatedAmount;
            this.TreatmentActivityTotalCost = treatmentActivityTotalCost;
            this.TreatmentActivityGrantCost = treatmentActivityGrantCost;
            this.TreatmentActivityStartDate = treatmentActivityStartDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentActivity(Project project, FundingSource fundingSource, decimal treatmentActivityFootprintAcres, decimal treatmentActivityBrushControlAcres, decimal treatmentActivityThinningAcres, decimal treatmentActivityPruningAcres, decimal treatmentActivitySlashAcres, decimal treatmentActivityPrescribedBurnAcres, decimal treatmentActivityAllocatedAmount, decimal treatmentActivityTotalCost, decimal treatmentActivityGrantCost, DateTime treatmentActivityStartDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentActivityID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.TreatmentActivities.Add(this);
            this.FundingSourceID = fundingSource.FundingSourceID;
            this.FundingSource = fundingSource;
            fundingSource.TreatmentActivities.Add(this);
            this.TreatmentActivityFootprintAcres = treatmentActivityFootprintAcres;
            this.TreatmentActivityBrushControlAcres = treatmentActivityBrushControlAcres;
            this.TreatmentActivityThinningAcres = treatmentActivityThinningAcres;
            this.TreatmentActivityPruningAcres = treatmentActivityPruningAcres;
            this.TreatmentActivitySlashAcres = treatmentActivitySlashAcres;
            this.TreatmentActivityPrescribedBurnAcres = treatmentActivityPrescribedBurnAcres;
            this.TreatmentActivityAllocatedAmount = treatmentActivityAllocatedAmount;
            this.TreatmentActivityTotalCost = treatmentActivityTotalCost;
            this.TreatmentActivityGrantCost = treatmentActivityGrantCost;
            this.TreatmentActivityStartDate = treatmentActivityStartDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentActivity CreateNewBlank(Project project, FundingSource fundingSource)
        {
            return new TreatmentActivity(project, fundingSource, default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(DateTime));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return false;
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentActivity).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(HttpRequestStorage.DatabaseEntities);
            dbContext.AllTreatmentActivities.Remove(this);
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

        }

        [Key]
        public int TreatmentActivityID { get; set; }
        public int TenantID { get; private set; }
        public int ProjectID { get; set; }
        public int FundingSourceID { get; set; }
        public decimal TreatmentActivityFootprintAcres { get; set; }
        public decimal TreatmentActivityBrushControlAcres { get; set; }
        public decimal TreatmentActivityThinningAcres { get; set; }
        public decimal TreatmentActivityPruningAcres { get; set; }
        public decimal TreatmentActivitySlashAcres { get; set; }
        public decimal TreatmentActivityPrescribedBurnAcres { get; set; }
        public decimal TreatmentActivityAllocatedAmount { get; set; }
        public decimal TreatmentActivityTotalCost { get; set; }
        public decimal TreatmentActivityGrantCost { get; set; }
        public DateTime TreatmentActivityStartDate { get; set; }
        public DateTime? TreatmentActivityEndDate { get; set; }
        public string TreatmentActivityNotes { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentActivityID; } set { TreatmentActivityID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual Project Project { get; set; }
        public virtual FundingSource FundingSource { get; set; }

        public static class FieldLengths
        {

        }
    }
}