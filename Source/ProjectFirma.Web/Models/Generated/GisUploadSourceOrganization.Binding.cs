//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GisUploadSourceOrganization]
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
    // Table [dbo].[GisUploadSourceOrganization] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GisUploadSourceOrganization]")]
    public partial class GisUploadSourceOrganization : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GisUploadSourceOrganization()
        {
            this.GisCrossWalkDefaults = new HashSet<GisCrossWalkDefault>();
            this.GisDefaultMappings = new HashSet<GisDefaultMapping>();
            this.GisUploadAttempts = new HashSet<GisUploadAttempt>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GisUploadSourceOrganization(int gisUploadSourceOrganizationID, string gisUploadSourceOrganizationName, string projectTypeDefaultName, string treatmentTypeDefaultName, bool? importIsFlattened, bool requireCompletionDate, bool adjustProjectTypeBasedOnTreatmentTypes, int projectStageDefaultID, bool dataDeriveProjectStage, int defaultLeadImplementerOrganizationID, int relationshipTypeForDefaultOrganizationID, bool importAsDetailedLocationInsteadOfTreatments, string projectDescriptionDefaultText) : this()
        {
            this.GisUploadSourceOrganizationID = gisUploadSourceOrganizationID;
            this.GisUploadSourceOrganizationName = gisUploadSourceOrganizationName;
            this.ProjectTypeDefaultName = projectTypeDefaultName;
            this.TreatmentTypeDefaultName = treatmentTypeDefaultName;
            this.ImportIsFlattened = importIsFlattened;
            this.RequireCompletionDate = requireCompletionDate;
            this.AdjustProjectTypeBasedOnTreatmentTypes = adjustProjectTypeBasedOnTreatmentTypes;
            this.ProjectStageDefaultID = projectStageDefaultID;
            this.DataDeriveProjectStage = dataDeriveProjectStage;
            this.DefaultLeadImplementerOrganizationID = defaultLeadImplementerOrganizationID;
            this.RelationshipTypeForDefaultOrganizationID = relationshipTypeForDefaultOrganizationID;
            this.ImportAsDetailedLocationInsteadOfTreatments = importAsDetailedLocationInsteadOfTreatments;
            this.ProjectDescriptionDefaultText = projectDescriptionDefaultText;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GisUploadSourceOrganization(string gisUploadSourceOrganizationName, bool requireCompletionDate, bool adjustProjectTypeBasedOnTreatmentTypes, int projectStageDefaultID, bool dataDeriveProjectStage, int defaultLeadImplementerOrganizationID, int relationshipTypeForDefaultOrganizationID, bool importAsDetailedLocationInsteadOfTreatments) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GisUploadSourceOrganizationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GisUploadSourceOrganizationName = gisUploadSourceOrganizationName;
            this.RequireCompletionDate = requireCompletionDate;
            this.AdjustProjectTypeBasedOnTreatmentTypes = adjustProjectTypeBasedOnTreatmentTypes;
            this.ProjectStageDefaultID = projectStageDefaultID;
            this.DataDeriveProjectStage = dataDeriveProjectStage;
            this.DefaultLeadImplementerOrganizationID = defaultLeadImplementerOrganizationID;
            this.RelationshipTypeForDefaultOrganizationID = relationshipTypeForDefaultOrganizationID;
            this.ImportAsDetailedLocationInsteadOfTreatments = importAsDetailedLocationInsteadOfTreatments;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GisUploadSourceOrganization(string gisUploadSourceOrganizationName, bool requireCompletionDate, bool adjustProjectTypeBasedOnTreatmentTypes, ProjectStage projectStageDefault, bool dataDeriveProjectStage, Organization defaultLeadImplementerOrganization, RelationshipType relationshipTypeForDefaultOrganization, bool importAsDetailedLocationInsteadOfTreatments) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GisUploadSourceOrganizationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GisUploadSourceOrganizationName = gisUploadSourceOrganizationName;
            this.RequireCompletionDate = requireCompletionDate;
            this.AdjustProjectTypeBasedOnTreatmentTypes = adjustProjectTypeBasedOnTreatmentTypes;
            this.ProjectStageDefaultID = projectStageDefault.ProjectStageID;
            this.DataDeriveProjectStage = dataDeriveProjectStage;
            this.DefaultLeadImplementerOrganizationID = defaultLeadImplementerOrganization.OrganizationID;
            this.DefaultLeadImplementerOrganization = defaultLeadImplementerOrganization;
            defaultLeadImplementerOrganization.GisUploadSourceOrganizationsWhereYouAreTheDefaultLeadImplementerOrganization.Add(this);
            this.RelationshipTypeForDefaultOrganizationID = relationshipTypeForDefaultOrganization.RelationshipTypeID;
            this.RelationshipTypeForDefaultOrganization = relationshipTypeForDefaultOrganization;
            relationshipTypeForDefaultOrganization.GisUploadSourceOrganizationsWhereYouAreTheRelationshipTypeForDefaultOrganization.Add(this);
            this.ImportAsDetailedLocationInsteadOfTreatments = importAsDetailedLocationInsteadOfTreatments;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GisUploadSourceOrganization CreateNewBlank(ProjectStage projectStageDefault, Organization defaultLeadImplementerOrganization, RelationshipType relationshipTypeForDefaultOrganization)
        {
            return new GisUploadSourceOrganization(default(string), default(bool), default(bool), projectStageDefault, default(bool), defaultLeadImplementerOrganization, relationshipTypeForDefaultOrganization, default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GisCrossWalkDefaults.Any() || GisDefaultMappings.Any() || GisUploadAttempts.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(GisCrossWalkDefaults.Any())
            {
                dependentObjects.Add(typeof(GisCrossWalkDefault).Name);
            }

            if(GisDefaultMappings.Any())
            {
                dependentObjects.Add(typeof(GisDefaultMapping).Name);
            }

            if(GisUploadAttempts.Any())
            {
                dependentObjects.Add(typeof(GisUploadAttempt).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GisUploadSourceOrganization).Name, typeof(GisCrossWalkDefault).Name, typeof(GisDefaultMapping).Name, typeof(GisUploadAttempt).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GisUploadSourceOrganizations.Remove(this);
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

            foreach(var x in GisCrossWalkDefaults.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GisDefaultMappings.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GisUploadAttempts.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GisUploadSourceOrganizationID { get; set; }
        public string GisUploadSourceOrganizationName { get; set; }
        public string ProjectTypeDefaultName { get; set; }
        public string TreatmentTypeDefaultName { get; set; }
        public bool? ImportIsFlattened { get; set; }
        public bool RequireCompletionDate { get; set; }
        public bool AdjustProjectTypeBasedOnTreatmentTypes { get; set; }
        public int ProjectStageDefaultID { get; set; }
        public bool DataDeriveProjectStage { get; set; }
        public int DefaultLeadImplementerOrganizationID { get; set; }
        public int RelationshipTypeForDefaultOrganizationID { get; set; }
        public bool ImportAsDetailedLocationInsteadOfTreatments { get; set; }
        public string ProjectDescriptionDefaultText { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GisUploadSourceOrganizationID; } set { GisUploadSourceOrganizationID = value; } }

        public virtual ICollection<GisCrossWalkDefault> GisCrossWalkDefaults { get; set; }
        public virtual ICollection<GisDefaultMapping> GisDefaultMappings { get; set; }
        public virtual ICollection<GisUploadAttempt> GisUploadAttempts { get; set; }
        public ProjectStage ProjectStageDefault { get { return ProjectStage.AllLookupDictionary[ProjectStageDefaultID]; } }
        public virtual Organization DefaultLeadImplementerOrganization { get; set; }
        public virtual RelationshipType RelationshipTypeForDefaultOrganization { get; set; }

        public static class FieldLengths
        {
            public const int GisUploadSourceOrganizationName = 100;
            public const int ProjectTypeDefaultName = 100;
            public const int TreatmentTypeDefaultName = 100;
            public const int ProjectDescriptionDefaultText = 4000;
        }
    }
}