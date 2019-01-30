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
    // Table [dbo].[TreatmentActivity] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TreatmentActivity]")]
    public partial class TreatmentActivity : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentActivity()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentActivity(int treatmentActivityID, int projectID, DateTime? treatmentActivityStartDate, DateTime? treatmentActivityEndDate, int treatmentActivityStatusID, int? treatmentActivityContactID, decimal treatmentActivityFootprintAcres, decimal treatmentActivityChippingAcres, decimal treatmentActivityPruningAcres, decimal treatmentActivityThinningAcres, decimal treatmentActivityMasticationAcres, decimal treatmentActivityGrazingAcres, decimal treatmentActivityLopAndScatterAcres, decimal treatmentActivityBiomassRemovalAcres, decimal treatmentActivityHandPileAcres, decimal treatmentActivityBroadcastBurnAcres, decimal treatmentActivityHandPileBurnAcres, decimal treatmentActivityMachinePileBurnAcres, decimal treatmentActivityOtherTreatmentAcres, string treatmentActivityNotes, int? programIndexID, int? projectCodeID, decimal treatmentActivitySlashAcres) : this()
        {
            this.TreatmentActivityID = treatmentActivityID;
            this.ProjectID = projectID;
            this.TreatmentActivityStartDate = treatmentActivityStartDate;
            this.TreatmentActivityEndDate = treatmentActivityEndDate;
            this.TreatmentActivityStatusID = treatmentActivityStatusID;
            this.TreatmentActivityContactID = treatmentActivityContactID;
            this.TreatmentActivityFootprintAcres = treatmentActivityFootprintAcres;
            this.TreatmentActivityChippingAcres = treatmentActivityChippingAcres;
            this.TreatmentActivityPruningAcres = treatmentActivityPruningAcres;
            this.TreatmentActivityThinningAcres = treatmentActivityThinningAcres;
            this.TreatmentActivityMasticationAcres = treatmentActivityMasticationAcres;
            this.TreatmentActivityGrazingAcres = treatmentActivityGrazingAcres;
            this.TreatmentActivityLopAndScatterAcres = treatmentActivityLopAndScatterAcres;
            this.TreatmentActivityBiomassRemovalAcres = treatmentActivityBiomassRemovalAcres;
            this.TreatmentActivityHandPileAcres = treatmentActivityHandPileAcres;
            this.TreatmentActivityBroadcastBurnAcres = treatmentActivityBroadcastBurnAcres;
            this.TreatmentActivityHandPileBurnAcres = treatmentActivityHandPileBurnAcres;
            this.TreatmentActivityMachinePileBurnAcres = treatmentActivityMachinePileBurnAcres;
            this.TreatmentActivityOtherTreatmentAcres = treatmentActivityOtherTreatmentAcres;
            this.TreatmentActivityNotes = treatmentActivityNotes;
            this.ProgramIndexID = programIndexID;
            this.ProjectCodeID = projectCodeID;
            this.TreatmentActivitySlashAcres = treatmentActivitySlashAcres;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentActivity(int projectID, int treatmentActivityStatusID, decimal treatmentActivityFootprintAcres, decimal treatmentActivityChippingAcres, decimal treatmentActivityPruningAcres, decimal treatmentActivityThinningAcres, decimal treatmentActivityMasticationAcres, decimal treatmentActivityGrazingAcres, decimal treatmentActivityLopAndScatterAcres, decimal treatmentActivityBiomassRemovalAcres, decimal treatmentActivityHandPileAcres, decimal treatmentActivityBroadcastBurnAcres, decimal treatmentActivityHandPileBurnAcres, decimal treatmentActivityMachinePileBurnAcres, decimal treatmentActivityOtherTreatmentAcres, decimal treatmentActivitySlashAcres) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentActivityID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.TreatmentActivityStatusID = treatmentActivityStatusID;
            this.TreatmentActivityFootprintAcres = treatmentActivityFootprintAcres;
            this.TreatmentActivityChippingAcres = treatmentActivityChippingAcres;
            this.TreatmentActivityPruningAcres = treatmentActivityPruningAcres;
            this.TreatmentActivityThinningAcres = treatmentActivityThinningAcres;
            this.TreatmentActivityMasticationAcres = treatmentActivityMasticationAcres;
            this.TreatmentActivityGrazingAcres = treatmentActivityGrazingAcres;
            this.TreatmentActivityLopAndScatterAcres = treatmentActivityLopAndScatterAcres;
            this.TreatmentActivityBiomassRemovalAcres = treatmentActivityBiomassRemovalAcres;
            this.TreatmentActivityHandPileAcres = treatmentActivityHandPileAcres;
            this.TreatmentActivityBroadcastBurnAcres = treatmentActivityBroadcastBurnAcres;
            this.TreatmentActivityHandPileBurnAcres = treatmentActivityHandPileBurnAcres;
            this.TreatmentActivityMachinePileBurnAcres = treatmentActivityMachinePileBurnAcres;
            this.TreatmentActivityOtherTreatmentAcres = treatmentActivityOtherTreatmentAcres;
            this.TreatmentActivitySlashAcres = treatmentActivitySlashAcres;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentActivity(Project project, TreatmentActivityStatus treatmentActivityStatus, decimal treatmentActivityFootprintAcres, decimal treatmentActivityChippingAcres, decimal treatmentActivityPruningAcres, decimal treatmentActivityThinningAcres, decimal treatmentActivityMasticationAcres, decimal treatmentActivityGrazingAcres, decimal treatmentActivityLopAndScatterAcres, decimal treatmentActivityBiomassRemovalAcres, decimal treatmentActivityHandPileAcres, decimal treatmentActivityBroadcastBurnAcres, decimal treatmentActivityHandPileBurnAcres, decimal treatmentActivityMachinePileBurnAcres, decimal treatmentActivityOtherTreatmentAcres, decimal treatmentActivitySlashAcres) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentActivityID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.TreatmentActivities.Add(this);
            this.TreatmentActivityStatusID = treatmentActivityStatus.TreatmentActivityStatusID;
            this.TreatmentActivityFootprintAcres = treatmentActivityFootprintAcres;
            this.TreatmentActivityChippingAcres = treatmentActivityChippingAcres;
            this.TreatmentActivityPruningAcres = treatmentActivityPruningAcres;
            this.TreatmentActivityThinningAcres = treatmentActivityThinningAcres;
            this.TreatmentActivityMasticationAcres = treatmentActivityMasticationAcres;
            this.TreatmentActivityGrazingAcres = treatmentActivityGrazingAcres;
            this.TreatmentActivityLopAndScatterAcres = treatmentActivityLopAndScatterAcres;
            this.TreatmentActivityBiomassRemovalAcres = treatmentActivityBiomassRemovalAcres;
            this.TreatmentActivityHandPileAcres = treatmentActivityHandPileAcres;
            this.TreatmentActivityBroadcastBurnAcres = treatmentActivityBroadcastBurnAcres;
            this.TreatmentActivityHandPileBurnAcres = treatmentActivityHandPileBurnAcres;
            this.TreatmentActivityMachinePileBurnAcres = treatmentActivityMachinePileBurnAcres;
            this.TreatmentActivityOtherTreatmentAcres = treatmentActivityOtherTreatmentAcres;
            this.TreatmentActivitySlashAcres = treatmentActivitySlashAcres;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentActivity CreateNewBlank(Project project, TreatmentActivityStatus treatmentActivityStatus)
        {
            return new TreatmentActivity(project, treatmentActivityStatus, default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal));
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
            
            dbContext.TreatmentActivities.Remove(this);
        }

        [Key]
        public int TreatmentActivityID { get; set; }
        public int ProjectID { get; set; }
        public DateTime? TreatmentActivityStartDate { get; set; }
        public DateTime? TreatmentActivityEndDate { get; set; }
        public int TreatmentActivityStatusID { get; set; }
        public int? TreatmentActivityContactID { get; set; }
        public decimal TreatmentActivityFootprintAcres { get; set; }
        public decimal TreatmentActivityChippingAcres { get; set; }
        public decimal TreatmentActivityPruningAcres { get; set; }
        public decimal TreatmentActivityThinningAcres { get; set; }
        public decimal TreatmentActivityMasticationAcres { get; set; }
        public decimal TreatmentActivityGrazingAcres { get; set; }
        public decimal TreatmentActivityLopAndScatterAcres { get; set; }
        public decimal TreatmentActivityBiomassRemovalAcres { get; set; }
        public decimal TreatmentActivityHandPileAcres { get; set; }
        public decimal TreatmentActivityBroadcastBurnAcres { get; set; }
        public decimal TreatmentActivityHandPileBurnAcres { get; set; }
        public decimal TreatmentActivityMachinePileBurnAcres { get; set; }
        public decimal TreatmentActivityOtherTreatmentAcres { get; set; }
        public string TreatmentActivityNotes { get; set; }
        public int? ProgramIndexID { get; set; }
        public int? ProjectCodeID { get; set; }
        public decimal TreatmentActivitySlashAcres { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentActivityID; } set { TreatmentActivityID = value; } }

        public virtual Project Project { get; set; }
        public TreatmentActivityStatus TreatmentActivityStatus { get { return TreatmentActivityStatus.AllLookupDictionary[TreatmentActivityStatusID]; } }
        public virtual Person TreatmentActivityContact { get; set; }
        public virtual ProgramIndex ProgramIndex { get; set; }
        public virtual ProjectCode ProjectCode { get; set; }

        public static class FieldLengths
        {

        }
    }
}