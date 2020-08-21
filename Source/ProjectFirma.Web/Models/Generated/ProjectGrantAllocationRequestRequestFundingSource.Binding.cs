//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectGrantAllocationRequestRequestFundingSource]
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
    // Table [dbo].[ProjectGrantAllocationRequestRequestFundingSource] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectGrantAllocationRequestRequestFundingSource]")]
    public partial class ProjectGrantAllocationRequestRequestFundingSource : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectGrantAllocationRequestRequestFundingSource()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectGrantAllocationRequestRequestFundingSource(int projectGrantAllocationRequestRequestFundingSourceID, int projectGrantAllocationRequestID, int projectGrantAllocationRequestFundingSourceID) : this()
        {
            this.ProjectGrantAllocationRequestRequestFundingSourceID = projectGrantAllocationRequestRequestFundingSourceID;
            this.ProjectGrantAllocationRequestID = projectGrantAllocationRequestID;
            this.ProjectGrantAllocationRequestFundingSourceID = projectGrantAllocationRequestFundingSourceID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectGrantAllocationRequestRequestFundingSource(int projectGrantAllocationRequestID, int projectGrantAllocationRequestFundingSourceID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectGrantAllocationRequestRequestFundingSourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectGrantAllocationRequestID = projectGrantAllocationRequestID;
            this.ProjectGrantAllocationRequestFundingSourceID = projectGrantAllocationRequestFundingSourceID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectGrantAllocationRequestRequestFundingSource(ProjectGrantAllocationRequest projectGrantAllocationRequest, ProjectGrantAllocationRequestFundingSource projectGrantAllocationRequestFundingSource) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectGrantAllocationRequestRequestFundingSourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectGrantAllocationRequestID = projectGrantAllocationRequest.ProjectGrantAllocationRequestID;
            this.ProjectGrantAllocationRequest = projectGrantAllocationRequest;
            projectGrantAllocationRequest.ProjectGrantAllocationRequestRequestFundingSources.Add(this);
            this.ProjectGrantAllocationRequestFundingSourceID = projectGrantAllocationRequestFundingSource.ProjectGrantAllocationRequestFundingSourceID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectGrantAllocationRequestRequestFundingSource CreateNewBlank(ProjectGrantAllocationRequest projectGrantAllocationRequest, ProjectGrantAllocationRequestFundingSource projectGrantAllocationRequestFundingSource)
        {
            return new ProjectGrantAllocationRequestRequestFundingSource(projectGrantAllocationRequest, projectGrantAllocationRequestFundingSource);
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
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectGrantAllocationRequestRequestFundingSource).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectGrantAllocationRequestRequestFundingSources.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectGrantAllocationRequestRequestFundingSourceID { get; set; }
        public int ProjectGrantAllocationRequestID { get; set; }
        public int ProjectGrantAllocationRequestFundingSourceID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectGrantAllocationRequestRequestFundingSourceID; } set { ProjectGrantAllocationRequestRequestFundingSourceID = value; } }

        public virtual ProjectGrantAllocationRequest ProjectGrantAllocationRequest { get; set; }
        public ProjectGrantAllocationRequestFundingSource ProjectGrantAllocationRequestFundingSource { get { return ProjectGrantAllocationRequestFundingSource.AllLookupDictionary[ProjectGrantAllocationRequestFundingSourceID]; } }

        public static class FieldLengths
        {

        }
    }
}