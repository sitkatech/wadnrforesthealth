//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Treatment]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    // Table [dbo].[Treatment] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Treatment]")]
    public partial class Treatment : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Treatment()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Treatment(int treatmentID, int projectID, DbGeometry treatmentFeature, int? grantAllocationAwardLandownerCostShareLineItemID, DateTime? treatmentStartDate, DateTime? treatmentEndDate, decimal treatmentFootprintAcres, decimal treatmentChippingAcres, decimal treatmentPruningAcres, decimal treatmentThinningAcres, decimal treatmentMasticationAcres, decimal treatmentGrazingAcres, decimal treatmentLopAndScatterAcres, decimal treatmentBiomassRemovalAcres, decimal treatmentHandPileAcres, decimal treatmentBroadcastBurnAcres, decimal treatmentHandPileBurnAcres, decimal treatmentMachinePileBurnAcres, decimal treatmentOtherTreatmentAcres, decimal treatmentSlashAcres, string treatmentNotes, int treatmentTypeID) : this()
        {
            this.TreatmentID = treatmentID;
            this.ProjectID = projectID;
            this.TreatmentFeature = treatmentFeature;
            this.GrantAllocationAwardLandownerCostShareLineItemID = grantAllocationAwardLandownerCostShareLineItemID;
            this.TreatmentStartDate = treatmentStartDate;
            this.TreatmentEndDate = treatmentEndDate;
            this.TreatmentFootprintAcres = treatmentFootprintAcres;
            this.TreatmentChippingAcres = treatmentChippingAcres;
            this.TreatmentPruningAcres = treatmentPruningAcres;
            this.TreatmentThinningAcres = treatmentThinningAcres;
            this.TreatmentMasticationAcres = treatmentMasticationAcres;
            this.TreatmentGrazingAcres = treatmentGrazingAcres;
            this.TreatmentLopAndScatterAcres = treatmentLopAndScatterAcres;
            this.TreatmentBiomassRemovalAcres = treatmentBiomassRemovalAcres;
            this.TreatmentHandPileAcres = treatmentHandPileAcres;
            this.TreatmentBroadcastBurnAcres = treatmentBroadcastBurnAcres;
            this.TreatmentHandPileBurnAcres = treatmentHandPileBurnAcres;
            this.TreatmentMachinePileBurnAcres = treatmentMachinePileBurnAcres;
            this.TreatmentOtherTreatmentAcres = treatmentOtherTreatmentAcres;
            this.TreatmentSlashAcres = treatmentSlashAcres;
            this.TreatmentNotes = treatmentNotes;
            this.TreatmentTypeID = treatmentTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Treatment(int projectID, decimal treatmentFootprintAcres, decimal treatmentChippingAcres, decimal treatmentPruningAcres, decimal treatmentThinningAcres, decimal treatmentMasticationAcres, decimal treatmentGrazingAcres, decimal treatmentLopAndScatterAcres, decimal treatmentBiomassRemovalAcres, decimal treatmentHandPileAcres, decimal treatmentBroadcastBurnAcres, decimal treatmentHandPileBurnAcres, decimal treatmentMachinePileBurnAcres, decimal treatmentOtherTreatmentAcres, decimal treatmentSlashAcres, int treatmentTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.TreatmentFootprintAcres = treatmentFootprintAcres;
            this.TreatmentChippingAcres = treatmentChippingAcres;
            this.TreatmentPruningAcres = treatmentPruningAcres;
            this.TreatmentThinningAcres = treatmentThinningAcres;
            this.TreatmentMasticationAcres = treatmentMasticationAcres;
            this.TreatmentGrazingAcres = treatmentGrazingAcres;
            this.TreatmentLopAndScatterAcres = treatmentLopAndScatterAcres;
            this.TreatmentBiomassRemovalAcres = treatmentBiomassRemovalAcres;
            this.TreatmentHandPileAcres = treatmentHandPileAcres;
            this.TreatmentBroadcastBurnAcres = treatmentBroadcastBurnAcres;
            this.TreatmentHandPileBurnAcres = treatmentHandPileBurnAcres;
            this.TreatmentMachinePileBurnAcres = treatmentMachinePileBurnAcres;
            this.TreatmentOtherTreatmentAcres = treatmentOtherTreatmentAcres;
            this.TreatmentSlashAcres = treatmentSlashAcres;
            this.TreatmentTypeID = treatmentTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Treatment(Project project, decimal treatmentFootprintAcres, decimal treatmentChippingAcres, decimal treatmentPruningAcres, decimal treatmentThinningAcres, decimal treatmentMasticationAcres, decimal treatmentGrazingAcres, decimal treatmentLopAndScatterAcres, decimal treatmentBiomassRemovalAcres, decimal treatmentHandPileAcres, decimal treatmentBroadcastBurnAcres, decimal treatmentHandPileBurnAcres, decimal treatmentMachinePileBurnAcres, decimal treatmentOtherTreatmentAcres, decimal treatmentSlashAcres, TreatmentType treatmentType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.Treatments.Add(this);
            this.TreatmentFootprintAcres = treatmentFootprintAcres;
            this.TreatmentChippingAcres = treatmentChippingAcres;
            this.TreatmentPruningAcres = treatmentPruningAcres;
            this.TreatmentThinningAcres = treatmentThinningAcres;
            this.TreatmentMasticationAcres = treatmentMasticationAcres;
            this.TreatmentGrazingAcres = treatmentGrazingAcres;
            this.TreatmentLopAndScatterAcres = treatmentLopAndScatterAcres;
            this.TreatmentBiomassRemovalAcres = treatmentBiomassRemovalAcres;
            this.TreatmentHandPileAcres = treatmentHandPileAcres;
            this.TreatmentBroadcastBurnAcres = treatmentBroadcastBurnAcres;
            this.TreatmentHandPileBurnAcres = treatmentHandPileBurnAcres;
            this.TreatmentMachinePileBurnAcres = treatmentMachinePileBurnAcres;
            this.TreatmentOtherTreatmentAcres = treatmentOtherTreatmentAcres;
            this.TreatmentSlashAcres = treatmentSlashAcres;
            this.TreatmentTypeID = treatmentType.TreatmentTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Treatment CreateNewBlank(Project project, TreatmentType treatmentType)
        {
            return new Treatment(project, default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), treatmentType);
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
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Treatment).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.Treatments.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int TreatmentID { get; set; }
        public int ProjectID { get; set; }
        public DbGeometry TreatmentFeature { get; set; }
        public int? GrantAllocationAwardLandownerCostShareLineItemID { get; set; }
        public DateTime? TreatmentStartDate { get; set; }
        public DateTime? TreatmentEndDate { get; set; }
        public decimal TreatmentFootprintAcres { get; set; }
        public decimal TreatmentChippingAcres { get; set; }
        public decimal TreatmentPruningAcres { get; set; }
        public decimal TreatmentThinningAcres { get; set; }
        public decimal TreatmentMasticationAcres { get; set; }
        public decimal TreatmentGrazingAcres { get; set; }
        public decimal TreatmentLopAndScatterAcres { get; set; }
        public decimal TreatmentBiomassRemovalAcres { get; set; }
        public decimal TreatmentHandPileAcres { get; set; }
        public decimal TreatmentBroadcastBurnAcres { get; set; }
        public decimal TreatmentHandPileBurnAcres { get; set; }
        public decimal TreatmentMachinePileBurnAcres { get; set; }
        public decimal TreatmentOtherTreatmentAcres { get; set; }
        public decimal TreatmentSlashAcres { get; set; }
        public string TreatmentNotes { get; set; }
        public int TreatmentTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentID; } set { TreatmentID = value; } }

        public virtual Project Project { get; set; }
        public virtual GrantAllocationAwardLandownerCostShareLineItem GrantAllocationAwardLandownerCostShareLineItem { get; set; }
        public TreatmentType TreatmentType { get { return TreatmentType.AllLookupDictionary[TreatmentTypeID]; } }

        public static class FieldLengths
        {
            public const int TreatmentNotes = 2000;
        }
    }
}