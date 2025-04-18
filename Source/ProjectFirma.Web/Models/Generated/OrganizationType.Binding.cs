//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OrganizationType]
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
    // Table [dbo].[OrganizationType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[OrganizationType]")]
    public partial class OrganizationType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected OrganizationType()
        {
            this.Organizations = new HashSet<Organization>();
            this.OrganizationTypeRelationshipTypes = new HashSet<OrganizationTypeRelationshipType>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public OrganizationType(int organizationTypeID, string organizationTypeName, string organizationTypeAbbreviation, string legendColor, bool showOnProjectMaps, bool isDefaultOrganizationType, bool isFundingType) : this()
        {
            this.OrganizationTypeID = organizationTypeID;
            this.OrganizationTypeName = organizationTypeName;
            this.OrganizationTypeAbbreviation = organizationTypeAbbreviation;
            this.LegendColor = legendColor;
            this.ShowOnProjectMaps = showOnProjectMaps;
            this.IsDefaultOrganizationType = isDefaultOrganizationType;
            this.IsFundingType = isFundingType;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public OrganizationType(string organizationTypeName, string organizationTypeAbbreviation, string legendColor, bool showOnProjectMaps, bool isDefaultOrganizationType, bool isFundingType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OrganizationTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.OrganizationTypeName = organizationTypeName;
            this.OrganizationTypeAbbreviation = organizationTypeAbbreviation;
            this.LegendColor = legendColor;
            this.ShowOnProjectMaps = showOnProjectMaps;
            this.IsDefaultOrganizationType = isDefaultOrganizationType;
            this.IsFundingType = isFundingType;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static OrganizationType CreateNewBlank()
        {
            return new OrganizationType(default(string), default(string), default(string), default(bool), default(bool), default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return Organizations.Any() || OrganizationTypeRelationshipTypes.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(Organizations.Any())
            {
                dependentObjects.Add(typeof(Organization).Name);
            }

            if(OrganizationTypeRelationshipTypes.Any())
            {
                dependentObjects.Add(typeof(OrganizationTypeRelationshipType).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(OrganizationType).Name, typeof(Organization).Name, typeof(OrganizationTypeRelationshipType).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.OrganizationTypes.Remove(this);
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

            foreach(var x in Organizations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in OrganizationTypeRelationshipTypes.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int OrganizationTypeID { get; set; }
        public string OrganizationTypeName { get; set; }
        public string OrganizationTypeAbbreviation { get; set; }
        public string LegendColor { get; set; }
        public bool ShowOnProjectMaps { get; set; }
        public bool IsDefaultOrganizationType { get; set; }
        public bool IsFundingType { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return OrganizationTypeID; } set { OrganizationTypeID = value; } }

        public virtual ICollection<Organization> Organizations { get; set; }
        public virtual ICollection<OrganizationTypeRelationshipType> OrganizationTypeRelationshipTypes { get; set; }

        public static class FieldLengths
        {
            public const int OrganizationTypeName = 200;
            public const int OrganizationTypeAbbreviation = 100;
            public const int LegendColor = 10;
        }
    }
}