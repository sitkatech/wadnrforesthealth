//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ForesterWorkUnit]
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
    // Table [dbo].[ForesterWorkUnit] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ForesterWorkUnit]")]
    public partial class ForesterWorkUnit : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ForesterWorkUnit()
        {
            this.ForesterWorkUnitPeople = new HashSet<ForesterWorkUnitPerson>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ForesterWorkUnit(int foresterWorkUnitID, int foresterRoleID, string foresterWorkUnitName, string regionName, DbGeometry foresterWorkUnitLocation) : this()
        {
            this.ForesterWorkUnitID = foresterWorkUnitID;
            this.ForesterRoleID = foresterRoleID;
            this.ForesterWorkUnitName = foresterWorkUnitName;
            this.RegionName = regionName;
            this.ForesterWorkUnitLocation = foresterWorkUnitLocation;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ForesterWorkUnit(int foresterRoleID, string foresterWorkUnitName, DbGeometry foresterWorkUnitLocation) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ForesterWorkUnitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ForesterRoleID = foresterRoleID;
            this.ForesterWorkUnitName = foresterWorkUnitName;
            this.ForesterWorkUnitLocation = foresterWorkUnitLocation;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ForesterWorkUnit(ForesterRole foresterRole, string foresterWorkUnitName, DbGeometry foresterWorkUnitLocation) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ForesterWorkUnitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ForesterRoleID = foresterRole.ForesterRoleID;
            this.ForesterRole = foresterRole;
            foresterRole.ForesterWorkUnits.Add(this);
            this.ForesterWorkUnitName = foresterWorkUnitName;
            this.ForesterWorkUnitLocation = foresterWorkUnitLocation;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ForesterWorkUnit CreateNewBlank(ForesterRole foresterRole)
        {
            return new ForesterWorkUnit(foresterRole, default(string), default(DbGeometry));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return ForesterWorkUnitPeople.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(ForesterWorkUnitPeople.Any())
            {
                dependentObjects.Add(typeof(ForesterWorkUnitPerson).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ForesterWorkUnit).Name, typeof(ForesterWorkUnitPerson).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ForesterWorkUnits.Remove(this);
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

            foreach(var x in ForesterWorkUnitPeople.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int ForesterWorkUnitID { get; set; }
        public int ForesterRoleID { get; set; }
        public string ForesterWorkUnitName { get; set; }
        public string RegionName { get; set; }
        public DbGeometry ForesterWorkUnitLocation { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ForesterWorkUnitID; } set { ForesterWorkUnitID = value; } }

        public virtual ICollection<ForesterWorkUnitPerson> ForesterWorkUnitPeople { get; set; }
        public virtual ForesterRole ForesterRole { get; set; }

        public static class FieldLengths
        {
            public const int ForesterWorkUnitName = 100;
            public const int RegionName = 100;
        }
    }
}