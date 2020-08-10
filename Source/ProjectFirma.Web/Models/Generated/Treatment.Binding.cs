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
        public Treatment(int treatmentID, int projectID, int? grantAllocationAwardLandownerCostShareLineItemID, DateTime? treatmentStartDate, DateTime? treatmentEndDate, decimal treatmentFootprintAcres, string treatmentNotes, int treatmentTypeID, int? treatmentAreaID, decimal? treatmentTreatedAcres) : this()
        {
            this.TreatmentID = treatmentID;
            this.ProjectID = projectID;
            this.GrantAllocationAwardLandownerCostShareLineItemID = grantAllocationAwardLandownerCostShareLineItemID;
            this.TreatmentStartDate = treatmentStartDate;
            this.TreatmentEndDate = treatmentEndDate;
            this.TreatmentFootprintAcres = treatmentFootprintAcres;
            this.TreatmentNotes = treatmentNotes;
            this.TreatmentTypeID = treatmentTypeID;
            this.TreatmentAreaID = treatmentAreaID;
            this.TreatmentTreatedAcres = treatmentTreatedAcres;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Treatment(int projectID, decimal treatmentFootprintAcres, int treatmentTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.TreatmentFootprintAcres = treatmentFootprintAcres;
            this.TreatmentTypeID = treatmentTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Treatment(Project project, decimal treatmentFootprintAcres, TreatmentType treatmentType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.Treatments.Add(this);
            this.TreatmentFootprintAcres = treatmentFootprintAcres;
            this.TreatmentTypeID = treatmentType.TreatmentTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Treatment CreateNewBlank(Project project, TreatmentType treatmentType)
        {
            return new Treatment(project, default(decimal), treatmentType);
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
        public int? GrantAllocationAwardLandownerCostShareLineItemID { get; set; }
        public DateTime? TreatmentStartDate { get; set; }
        public DateTime? TreatmentEndDate { get; set; }
        public decimal TreatmentFootprintAcres { get; set; }
        public string TreatmentNotes { get; set; }
        public int TreatmentTypeID { get; set; }
        public int? TreatmentAreaID { get; set; }
        public decimal? TreatmentTreatedAcres { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentID; } set { TreatmentID = value; } }

        public virtual Project Project { get; set; }
        public virtual GrantAllocationAwardLandownerCostShareLineItem GrantAllocationAwardLandownerCostShareLineItem { get; set; }
        public TreatmentType TreatmentType { get { return TreatmentType.AllLookupDictionary[TreatmentTypeID]; } }
        public virtual TreatmentArea TreatmentArea { get; set; }

        public static class FieldLengths
        {
            public const int TreatmentNotes = 2000;
        }
    }
}