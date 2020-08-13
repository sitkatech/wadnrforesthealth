//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentDetailedActivityType]
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
    // Table [dbo].[TreatmentDetailedActivityType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TreatmentDetailedActivityType]")]
    public partial class TreatmentDetailedActivityType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentDetailedActivityType()
        {
            this.Treatments = new HashSet<Treatment>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentDetailedActivityType(int treatmentDetailedActivityTypeID, string treatmentDetailedActivityTypeName, string treatmentDetailedActivityTypeDisplayName) : this()
        {
            this.TreatmentDetailedActivityTypeID = treatmentDetailedActivityTypeID;
            this.TreatmentDetailedActivityTypeName = treatmentDetailedActivityTypeName;
            this.TreatmentDetailedActivityTypeDisplayName = treatmentDetailedActivityTypeDisplayName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentDetailedActivityType(string treatmentDetailedActivityTypeName, string treatmentDetailedActivityTypeDisplayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentDetailedActivityTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentDetailedActivityTypeName = treatmentDetailedActivityTypeName;
            this.TreatmentDetailedActivityTypeDisplayName = treatmentDetailedActivityTypeDisplayName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentDetailedActivityType CreateNewBlank()
        {
            return new TreatmentDetailedActivityType(default(string), default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return Treatments.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(Treatments.Any())
            {
                dependentObjects.Add(typeof(Treatment).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentDetailedActivityType).Name, typeof(Treatment).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.TreatmentDetailedActivityTypes.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in Treatments.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int TreatmentDetailedActivityTypeID { get; set; }
        public string TreatmentDetailedActivityTypeName { get; set; }
        public string TreatmentDetailedActivityTypeDisplayName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentDetailedActivityTypeID; } set { TreatmentDetailedActivityTypeID = value; } }

        public virtual ICollection<Treatment> Treatments { get; set; }

        public static class FieldLengths
        {
            public const int TreatmentDetailedActivityTypeName = 50;
            public const int TreatmentDetailedActivityTypeDisplayName = 50;
        }
    }
}