//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Organization]
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
    // Table [dbo].[Organization] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Organization]")]
    public partial class Organization : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Organization()
        {
            this.Agreements = new HashSet<Agreement>();
            this.FundSources = new HashSet<FundSource>();
            this.FundSourceAllocations = new HashSet<FundSourceAllocation>();
            this.GisUploadSourceOrganizationsWhereYouAreTheDefaultLeadImplementerOrganization = new HashSet<GisUploadSourceOrganization>();
            this.OrganizationBoundaryStagings = new HashSet<OrganizationBoundaryStaging>();
            this.People = new HashSet<Person>();
            this.PersonStewardOrganizations = new HashSet<PersonStewardOrganization>();
            this.Programs = new HashSet<Program>();
            this.ProjectOrganizations = new HashSet<ProjectOrganization>();
            this.ProjectOrganizationUpdates = new HashSet<ProjectOrganizationUpdate>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Organization(int organizationID, Guid? organizationGuid, string organizationName, string organizationShortName, int? primaryContactPersonID, bool isActive, string organizationUrl, int? logoFileResourceID, int organizationTypeID, DbGeometry organizationBoundary, int? vendorID, bool isEditable) : this()
        {
            this.OrganizationID = organizationID;
            this.OrganizationGuid = organizationGuid;
            this.OrganizationName = organizationName;
            this.OrganizationShortName = organizationShortName;
            this.PrimaryContactPersonID = primaryContactPersonID;
            this.IsActive = isActive;
            this.OrganizationUrl = organizationUrl;
            this.LogoFileResourceID = logoFileResourceID;
            this.OrganizationTypeID = organizationTypeID;
            this.OrganizationBoundary = organizationBoundary;
            this.VendorID = vendorID;
            this.IsEditable = isEditable;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Organization(string organizationName, string organizationShortName, bool isActive, int organizationTypeID, bool isEditable) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OrganizationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.OrganizationName = organizationName;
            this.OrganizationShortName = organizationShortName;
            this.IsActive = isActive;
            this.OrganizationTypeID = organizationTypeID;
            this.IsEditable = isEditable;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Organization(string organizationName, string organizationShortName, bool isActive, OrganizationType organizationType, bool isEditable) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OrganizationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.OrganizationName = organizationName;
            this.OrganizationShortName = organizationShortName;
            this.IsActive = isActive;
            this.OrganizationTypeID = organizationType.OrganizationTypeID;
            this.OrganizationType = organizationType;
            organizationType.Organizations.Add(this);
            this.IsEditable = isEditable;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Organization CreateNewBlank(OrganizationType organizationType)
        {
            return new Organization(default(string), default(string), default(bool), organizationType, default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return Agreements.Any() || FundSources.Any() || FundSourceAllocations.Any() || GisUploadSourceOrganizationsWhereYouAreTheDefaultLeadImplementerOrganization.Any() || OrganizationBoundaryStagings.Any() || People.Any() || PersonStewardOrganizations.Any() || Programs.Any() || ProjectOrganizations.Any() || ProjectOrganizationUpdates.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(Agreements.Any())
            {
                dependentObjects.Add(typeof(Agreement).Name);
            }

            if(FundSources.Any())
            {
                dependentObjects.Add(typeof(FundSource).Name);
            }

            if(FundSourceAllocations.Any())
            {
                dependentObjects.Add(typeof(FundSourceAllocation).Name);
            }

            if(GisUploadSourceOrganizationsWhereYouAreTheDefaultLeadImplementerOrganization.Any())
            {
                dependentObjects.Add(typeof(GisUploadSourceOrganization).Name);
            }

            if(OrganizationBoundaryStagings.Any())
            {
                dependentObjects.Add(typeof(OrganizationBoundaryStaging).Name);
            }

            if(People.Any())
            {
                dependentObjects.Add(typeof(Person).Name);
            }

            if(PersonStewardOrganizations.Any())
            {
                dependentObjects.Add(typeof(PersonStewardOrganization).Name);
            }

            if(Programs.Any())
            {
                dependentObjects.Add(typeof(Program).Name);
            }

            if(ProjectOrganizations.Any())
            {
                dependentObjects.Add(typeof(ProjectOrganization).Name);
            }

            if(ProjectOrganizationUpdates.Any())
            {
                dependentObjects.Add(typeof(ProjectOrganizationUpdate).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Organization).Name, typeof(Agreement).Name, typeof(FundSource).Name, typeof(FundSourceAllocation).Name, typeof(GisUploadSourceOrganization).Name, typeof(OrganizationBoundaryStaging).Name, typeof(Person).Name, typeof(PersonStewardOrganization).Name, typeof(Program).Name, typeof(ProjectOrganization).Name, typeof(ProjectOrganizationUpdate).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.Organizations.Remove(this);
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

            foreach(var x in Agreements.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in FundSources.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in FundSourceAllocations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GisUploadSourceOrganizationsWhereYouAreTheDefaultLeadImplementerOrganization.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in OrganizationBoundaryStagings.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in People.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in PersonStewardOrganizations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in Programs.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectOrganizations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectOrganizationUpdates.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int OrganizationID { get; set; }
        public Guid? OrganizationGuid { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationShortName { get; set; }
        public int? PrimaryContactPersonID { get; set; }
        public bool IsActive { get; set; }
        public string OrganizationUrl { get; set; }
        public int? LogoFileResourceID { get; set; }
        public int OrganizationTypeID { get; set; }
        public DbGeometry OrganizationBoundary { get; set; }
        public int? VendorID { get; set; }
        public bool IsEditable { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return OrganizationID; } set { OrganizationID = value; } }

        public virtual ICollection<Agreement> Agreements { get; set; }
        public virtual ICollection<FundSource> FundSources { get; set; }
        public virtual ICollection<FundSourceAllocation> FundSourceAllocations { get; set; }
        public virtual ICollection<GisUploadSourceOrganization> GisUploadSourceOrganizationsWhereYouAreTheDefaultLeadImplementerOrganization { get; set; }
        public virtual ICollection<OrganizationBoundaryStaging> OrganizationBoundaryStagings { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<PersonStewardOrganization> PersonStewardOrganizations { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
        public virtual ICollection<ProjectOrganization> ProjectOrganizations { get; set; }
        public virtual ICollection<ProjectOrganizationUpdate> ProjectOrganizationUpdates { get; set; }
        public virtual Person PrimaryContactPerson { get; set; }
        public virtual FileResource LogoFileResource { get; set; }
        public virtual OrganizationType OrganizationType { get; set; }
        public virtual Vendor Vendor { get; set; }

        public static class FieldLengths
        {
            public const int OrganizationName = 200;
            public const int OrganizationShortName = 50;
            public const int OrganizationUrl = 200;
        }
    }
}