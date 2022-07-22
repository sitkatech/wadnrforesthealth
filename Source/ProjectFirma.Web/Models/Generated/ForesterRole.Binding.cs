//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ForesterRole]
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
    // Table [dbo].[ForesterRole] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ForesterRole]")]
    public partial class ForesterRole : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ForesterRole()
        {
            this.ForesterWorkUnits = new HashSet<ForesterWorkUnit>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ForesterRole(int foresterRoleID, string foresterRoleDisplayName, string foresterRoleName) : this()
        {
            this.ForesterRoleID = foresterRoleID;
            this.ForesterRoleDisplayName = foresterRoleDisplayName;
            this.ForesterRoleName = foresterRoleName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ForesterRole(string foresterRoleDisplayName, string foresterRoleName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ForesterRoleID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ForesterRoleDisplayName = foresterRoleDisplayName;
            this.ForesterRoleName = foresterRoleName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ForesterRole CreateNewBlank()
        {
            return new ForesterRole(default(string), default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return ForesterWorkUnits.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(ForesterWorkUnits.Any())
            {
                dependentObjects.Add(typeof(ForesterWorkUnit).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ForesterRole).Name, typeof(ForesterWorkUnit).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ForesterRoles.Remove(this);
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

            foreach(var x in ForesterWorkUnits.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int ForesterRoleID { get; set; }
        public string ForesterRoleDisplayName { get; set; }
        public string ForesterRoleName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ForesterRoleID; } set { ForesterRoleID = value; } }

        public virtual ICollection<ForesterWorkUnit> ForesterWorkUnits { get; set; }

        public static class FieldLengths
        {
            public const int ForesterRoleDisplayName = 100;
            public const int ForesterRoleName = 100;
        }
    }
}