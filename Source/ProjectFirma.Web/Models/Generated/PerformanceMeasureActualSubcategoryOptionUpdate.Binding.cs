//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PerformanceMeasureActualSubcategoryOptionUpdate]
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
    // Table [dbo].[PerformanceMeasureActualSubcategoryOptionUpdate] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[PerformanceMeasureActualSubcategoryOptionUpdate]")]
    public partial class PerformanceMeasureActualSubcategoryOptionUpdate : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected PerformanceMeasureActualSubcategoryOptionUpdate()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public PerformanceMeasureActualSubcategoryOptionUpdate(int performanceMeasureActualSubcategoryOptionUpdateID, int performanceMeasureActualUpdateID, int performanceMeasureSubcategoryOptionID, int performanceMeasureID, int performanceMeasureSubcategoryID) : this()
        {
            this.PerformanceMeasureActualSubcategoryOptionUpdateID = performanceMeasureActualSubcategoryOptionUpdateID;
            this.PerformanceMeasureActualUpdateID = performanceMeasureActualUpdateID;
            this.PerformanceMeasureSubcategoryOptionID = performanceMeasureSubcategoryOptionID;
            this.PerformanceMeasureID = performanceMeasureID;
            this.PerformanceMeasureSubcategoryID = performanceMeasureSubcategoryID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public PerformanceMeasureActualSubcategoryOptionUpdate(int performanceMeasureActualUpdateID, int performanceMeasureSubcategoryOptionID, int performanceMeasureID, int performanceMeasureSubcategoryID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PerformanceMeasureActualSubcategoryOptionUpdateID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PerformanceMeasureActualUpdateID = performanceMeasureActualUpdateID;
            this.PerformanceMeasureSubcategoryOptionID = performanceMeasureSubcategoryOptionID;
            this.PerformanceMeasureID = performanceMeasureID;
            this.PerformanceMeasureSubcategoryID = performanceMeasureSubcategoryID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public PerformanceMeasureActualSubcategoryOptionUpdate(PerformanceMeasureActualUpdate performanceMeasureActualUpdate, PerformanceMeasureSubcategoryOption performanceMeasureSubcategoryOption, PerformanceMeasure performanceMeasure, PerformanceMeasureSubcategory performanceMeasureSubcategory) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PerformanceMeasureActualSubcategoryOptionUpdateID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.PerformanceMeasureActualUpdateID = performanceMeasureActualUpdate.PerformanceMeasureActualUpdateID;
            this.PerformanceMeasureActualUpdate = performanceMeasureActualUpdate;
            performanceMeasureActualUpdate.PerformanceMeasureActualSubcategoryOptionUpdates.Add(this);
            this.PerformanceMeasureSubcategoryOptionID = performanceMeasureSubcategoryOption.PerformanceMeasureSubcategoryOptionID;
            this.PerformanceMeasureSubcategoryOption = performanceMeasureSubcategoryOption;
            performanceMeasureSubcategoryOption.PerformanceMeasureActualSubcategoryOptionUpdates.Add(this);
            this.PerformanceMeasureID = performanceMeasure.PerformanceMeasureID;
            this.PerformanceMeasure = performanceMeasure;
            performanceMeasure.PerformanceMeasureActualSubcategoryOptionUpdates.Add(this);
            this.PerformanceMeasureSubcategoryID = performanceMeasureSubcategory.PerformanceMeasureSubcategoryID;
            this.PerformanceMeasureSubcategory = performanceMeasureSubcategory;
            performanceMeasureSubcategory.PerformanceMeasureActualSubcategoryOptionUpdates.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static PerformanceMeasureActualSubcategoryOptionUpdate CreateNewBlank(PerformanceMeasureActualUpdate performanceMeasureActualUpdate, PerformanceMeasureSubcategoryOption performanceMeasureSubcategoryOption, PerformanceMeasure performanceMeasure, PerformanceMeasureSubcategory performanceMeasureSubcategory)
        {
            return new PerformanceMeasureActualSubcategoryOptionUpdate(performanceMeasureActualUpdate, performanceMeasureSubcategoryOption, performanceMeasure, performanceMeasureSubcategory);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(PerformanceMeasureActualSubcategoryOptionUpdate).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.PerformanceMeasureActualSubcategoryOptionUpdates.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int PerformanceMeasureActualSubcategoryOptionUpdateID { get; set; }
        public int PerformanceMeasureActualUpdateID { get; set; }
        public int PerformanceMeasureSubcategoryOptionID { get; set; }
        public int PerformanceMeasureID { get; set; }
        public int PerformanceMeasureSubcategoryID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return PerformanceMeasureActualSubcategoryOptionUpdateID; } set { PerformanceMeasureActualSubcategoryOptionUpdateID = value; } }

        public virtual PerformanceMeasureActualUpdate PerformanceMeasureActualUpdate { get; set; }
        public virtual PerformanceMeasureSubcategoryOption PerformanceMeasureSubcategoryOption { get; set; }
        public virtual PerformanceMeasure PerformanceMeasure { get; set; }
        public virtual PerformanceMeasureSubcategory PerformanceMeasureSubcategory { get; set; }

        public static class FieldLengths
        {

        }
    }
}