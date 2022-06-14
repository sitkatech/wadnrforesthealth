//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PriorityLandscapeCategory]
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
    // Table [dbo].[PriorityLandscapeCategory] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[PriorityLandscapeCategory]")]
    public partial class PriorityLandscapeCategory : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected PriorityLandscapeCategory()
        {
            this.PriorityLandscapes = new HashSet<PriorityLandscape>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public PriorityLandscapeCategory(int priorityLandscapeCategoryID, string priorityLandscapeCategoryName, string priorityLandscapeCategoryDisplayName, string priorityLandscapeCategoryMapLayerColor) : this()
        {
            this.PriorityLandscapeCategoryID = priorityLandscapeCategoryID;
            this.PriorityLandscapeCategoryName = priorityLandscapeCategoryName;
            this.PriorityLandscapeCategoryDisplayName = priorityLandscapeCategoryDisplayName;
            this.PriorityLandscapeCategoryMapLayerColor = priorityLandscapeCategoryMapLayerColor;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public PriorityLandscapeCategory(string priorityLandscapeCategoryName, string priorityLandscapeCategoryDisplayName, string priorityLandscapeCategoryMapLayerColor) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PriorityLandscapeCategoryID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PriorityLandscapeCategoryName = priorityLandscapeCategoryName;
            this.PriorityLandscapeCategoryDisplayName = priorityLandscapeCategoryDisplayName;
            this.PriorityLandscapeCategoryMapLayerColor = priorityLandscapeCategoryMapLayerColor;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static PriorityLandscapeCategory CreateNewBlank()
        {
            return new PriorityLandscapeCategory(default(string), default(string), default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(PriorityLandscapeCategory).Name, typeof(PriorityLandscape).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.PriorityLandscapeCategories.Remove(this);
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
        public int PriorityLandscapeCategoryID { get; set; }
        public string PriorityLandscapeCategoryName { get; set; }
        public string PriorityLandscapeCategoryDisplayName { get; set; }
        public string PriorityLandscapeCategoryMapLayerColor { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return PriorityLandscapeCategoryID; } set { PriorityLandscapeCategoryID = value; } }

        public virtual ICollection<PriorityLandscape> PriorityLandscapes { get; set; }

        public static class FieldLengths
        {
            public const int PriorityLandscapeCategoryName = 100;
            public const int PriorityLandscapeCategoryDisplayName = 100;
            public const int PriorityLandscapeCategoryMapLayerColor = 20;
        }
    }
}