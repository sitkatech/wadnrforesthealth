//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PriorityArea]
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
    // Table [dbo].[PriorityArea] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[PriorityArea]")]
    public partial class PriorityArea : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected PriorityArea()
        {
            this.ProjectPriorityAreas = new HashSet<ProjectPriorityArea>();
            this.ProjectPriorityAreaUpdates = new HashSet<ProjectPriorityAreaUpdate>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public PriorityArea(int priorityAreaID, string priorityAreaName, DbGeometry priorityAreaLocation) : this()
        {
            this.PriorityAreaID = priorityAreaID;
            this.PriorityAreaName = priorityAreaName;
            this.PriorityAreaLocation = priorityAreaLocation;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public PriorityArea(string priorityAreaName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PriorityAreaID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PriorityAreaName = priorityAreaName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static PriorityArea CreateNewBlank()
        {
            return new PriorityArea(default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return ProjectPriorityAreas.Any() || ProjectPriorityAreaUpdates.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(ProjectPriorityAreas.Any())
            {
                dependentObjects.Add(typeof(ProjectPriorityArea).Name);
            }

            if(ProjectPriorityAreaUpdates.Any())
            {
                dependentObjects.Add(typeof(ProjectPriorityAreaUpdate).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(PriorityArea).Name, typeof(ProjectPriorityArea).Name, typeof(ProjectPriorityAreaUpdate).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.PriorityAreas.Remove(this);
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

            foreach(var x in ProjectPriorityAreas.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectPriorityAreaUpdates.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int PriorityAreaID { get; set; }
        public string PriorityAreaName { get; set; }
        public DbGeometry PriorityAreaLocation { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return PriorityAreaID; } set { PriorityAreaID = value; } }

        public virtual ICollection<ProjectPriorityArea> ProjectPriorityAreas { get; set; }
        public virtual ICollection<ProjectPriorityAreaUpdate> ProjectPriorityAreaUpdates { get; set; }

        public static class FieldLengths
        {
            public const int PriorityAreaName = 100;
        }
    }
}