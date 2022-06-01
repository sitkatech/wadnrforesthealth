//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PriorityLandscapeType]
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
    // Table [dbo].[PriorityLandscapeType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[PriorityLandscapeType]")]
    public partial class PriorityLandscapeType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected PriorityLandscapeType()
        {
            this.PriorityLandscapes = new HashSet<PriorityLandscape>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public PriorityLandscapeType(int priorityLandscapeTypeID, string priorityLandscapeTypeName, string priorityLandscapeTypeDisplayName) : this()
        {
            this.PriorityLandscapeTypeID = priorityLandscapeTypeID;
            this.PriorityLandscapeTypeName = priorityLandscapeTypeName;
            this.PriorityLandscapeTypeDisplayName = priorityLandscapeTypeDisplayName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public PriorityLandscapeType(string priorityLandscapeTypeName, string priorityLandscapeTypeDisplayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PriorityLandscapeTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PriorityLandscapeTypeName = priorityLandscapeTypeName;
            this.PriorityLandscapeTypeDisplayName = priorityLandscapeTypeDisplayName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static PriorityLandscapeType CreateNewBlank()
        {
            return new PriorityLandscapeType(default(string), default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return PriorityLandscapes.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(PriorityLandscapes.Any())
            {
                dependentObjects.Add(typeof(PriorityLandscape).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(PriorityLandscapeType).Name, typeof(PriorityLandscape).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.PriorityLandscapeTypes.Remove(this);
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

            foreach(var x in PriorityLandscapes.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int PriorityLandscapeTypeID { get; set; }
        public string PriorityLandscapeTypeName { get; set; }
        public string PriorityLandscapeTypeDisplayName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return PriorityLandscapeTypeID; } set { PriorityLandscapeTypeID = value; } }

        public virtual ICollection<PriorityLandscape> PriorityLandscapes { get; set; }

        public static class FieldLengths
        {
            public const int PriorityLandscapeTypeName = 100;
            public const int PriorityLandscapeTypeDisplayName = 100;
        }
    }
}