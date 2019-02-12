//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectPriorityAreaUpdate]
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
    // Table [dbo].[ProjectPriorityAreaUpdate] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectPriorityAreaUpdate]")]
    public partial class ProjectPriorityAreaUpdate : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectPriorityAreaUpdate()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectPriorityAreaUpdate(int projectPriorityAreaUpdateID, int projectUpdateBatchID, int priorityAreaID) : this()
        {
            this.ProjectPriorityAreaUpdateID = projectPriorityAreaUpdateID;
            this.ProjectUpdateBatchID = projectUpdateBatchID;
            this.PriorityAreaID = priorityAreaID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectPriorityAreaUpdate(int projectUpdateBatchID, int priorityAreaID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectPriorityAreaUpdateID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectUpdateBatchID = projectUpdateBatchID;
            this.PriorityAreaID = priorityAreaID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectPriorityAreaUpdate(ProjectUpdateBatch projectUpdateBatch, PriorityArea priorityArea) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectPriorityAreaUpdateID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectUpdateBatchID = projectUpdateBatch.ProjectUpdateBatchID;
            this.ProjectUpdateBatch = projectUpdateBatch;
            projectUpdateBatch.ProjectPriorityAreaUpdates.Add(this);
            this.PriorityAreaID = priorityArea.PriorityAreaID;
            this.PriorityArea = priorityArea;
            priorityArea.ProjectPriorityAreaUpdates.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectPriorityAreaUpdate CreateNewBlank(ProjectUpdateBatch projectUpdateBatch, PriorityArea priorityArea)
        {
            return new ProjectPriorityAreaUpdate(projectUpdateBatch, priorityArea);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectPriorityAreaUpdate).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectPriorityAreaUpdates.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectPriorityAreaUpdateID { get; set; }
        public int ProjectUpdateBatchID { get; set; }
        public int PriorityAreaID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectPriorityAreaUpdateID; } set { ProjectPriorityAreaUpdateID = value; } }

        public virtual ProjectUpdateBatch ProjectUpdateBatch { get; set; }
        public virtual PriorityArea PriorityArea { get; set; }

        public static class FieldLengths
        {

        }
    }
}