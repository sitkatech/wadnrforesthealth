//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectRegionUpdate]
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
    // Table [dbo].[ProjectRegionUpdate] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectRegionUpdate]")]
    public partial class ProjectRegionUpdate : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectRegionUpdate()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectRegionUpdate(int projectRegionUpdateID, int projectUpdateBatchID, int dNRUplandRegionID) : this()
        {
            this.ProjectRegionUpdateID = projectRegionUpdateID;
            this.ProjectUpdateBatchID = projectUpdateBatchID;
            this.DNRUplandRegionID = dNRUplandRegionID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectRegionUpdate(int projectUpdateBatchID, int dNRUplandRegionID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectRegionUpdateID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectUpdateBatchID = projectUpdateBatchID;
            this.DNRUplandRegionID = dNRUplandRegionID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectRegionUpdate(ProjectUpdateBatch projectUpdateBatch, DNRUplandRegion dNRUplandRegion) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectRegionUpdateID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectUpdateBatchID = projectUpdateBatch.ProjectUpdateBatchID;
            this.ProjectUpdateBatch = projectUpdateBatch;
            projectUpdateBatch.ProjectRegionUpdates.Add(this);
            this.DNRUplandRegionID = dNRUplandRegion.DNRUplandRegionID;
            this.DNRUplandRegion = dNRUplandRegion;
            dNRUplandRegion.ProjectRegionUpdates.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectRegionUpdate CreateNewBlank(ProjectUpdateBatch projectUpdateBatch, DNRUplandRegion dNRUplandRegion)
        {
            return new ProjectRegionUpdate(projectUpdateBatch, dNRUplandRegion);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectRegionUpdate).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectRegionUpdates.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectRegionUpdateID { get; set; }
        public int ProjectUpdateBatchID { get; set; }
        public int DNRUplandRegionID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectRegionUpdateID; } set { ProjectRegionUpdateID = value; } }

        public virtual ProjectUpdateBatch ProjectUpdateBatch { get; set; }
        public virtual DNRUplandRegion DNRUplandRegion { get; set; }

        public static class FieldLengths
        {

        }
    }
}