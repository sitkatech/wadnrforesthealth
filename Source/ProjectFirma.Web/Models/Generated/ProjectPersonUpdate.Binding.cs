//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectPersonUpdate]
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
    [Table("[dbo].[ProjectPersonUpdate]")]
    public partial class ProjectPersonUpdate : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectPersonUpdate()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectPersonUpdate(int projectPersonUpdateID, int projectUpdateBatchID, int personID, int projectPersonRelationshipTypeID) : this()
        {
            this.ProjectPersonUpdateID = projectPersonUpdateID;
            this.ProjectUpdateBatchID = projectUpdateBatchID;
            this.PersonID = personID;
            this.ProjectPersonRelationshipTypeID = projectPersonRelationshipTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectPersonUpdate(int projectUpdateBatchID, int personID, int projectPersonRelationshipTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectPersonUpdateID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectUpdateBatchID = projectUpdateBatchID;
            this.PersonID = personID;
            this.ProjectPersonRelationshipTypeID = projectPersonRelationshipTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectPersonUpdate(ProjectUpdateBatch projectUpdateBatch, Person person, ProjectPersonRelationshipType projectPersonRelationshipType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectPersonUpdateID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectUpdateBatchID = projectUpdateBatch.ProjectUpdateBatchID;
            this.ProjectUpdateBatch = projectUpdateBatch;
            projectUpdateBatch.ProjectPersonUpdates.Add(this);
            this.PersonID = person.PersonID;
            this.Person = person;
            person.ProjectPersonUpdates.Add(this);
            this.ProjectPersonRelationshipTypeID = projectPersonRelationshipType.ProjectPersonRelationshipTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectPersonUpdate CreateNewBlank(ProjectUpdateBatch projectUpdateBatch, Person person, ProjectPersonRelationshipType projectPersonRelationshipType)
        {
            return new ProjectPersonUpdate(projectUpdateBatch, person, projectPersonRelationshipType);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectPersonUpdate).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            dbContext.AllProjectPersonUpdates.Remove(this);
        }

        [Key]
        public int ProjectPersonUpdateID { get; set; }
        public int TenantID { get; private set; }
        public int ProjectUpdateBatchID { get; set; }
        public int PersonID { get; set; }
        public int ProjectPersonRelationshipTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectPersonUpdateID; } set { ProjectPersonUpdateID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual ProjectUpdateBatch ProjectUpdateBatch { get; set; }
        public virtual Person Person { get; set; }
        public ProjectPersonRelationshipType ProjectPersonRelationshipType { get { return ProjectPersonRelationshipType.AllLookupDictionary[ProjectPersonRelationshipTypeID]; } }

        public static class FieldLengths
        {

        }
    }
}