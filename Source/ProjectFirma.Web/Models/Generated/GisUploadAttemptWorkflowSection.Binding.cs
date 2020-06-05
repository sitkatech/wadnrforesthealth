//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GisUploadAttemptWorkflowSection]
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
    // Table [dbo].[GisUploadAttemptWorkflowSection] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GisUploadAttemptWorkflowSection]")]
    public partial class GisUploadAttemptWorkflowSection : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GisUploadAttemptWorkflowSection()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GisUploadAttemptWorkflowSection(int gisUploadAttemptWorkflowSectionID, string gisUploadAttemptWorkflowSectionName, string gisUploadAttemptWorkflowSectionDisplayName, int sortOrder, bool hasCompletionStatus, int gisUploadAttemptWorkflowSectionGroupingID) : this()
        {
            this.GisUploadAttemptWorkflowSectionID = gisUploadAttemptWorkflowSectionID;
            this.GisUploadAttemptWorkflowSectionName = gisUploadAttemptWorkflowSectionName;
            this.GisUploadAttemptWorkflowSectionDisplayName = gisUploadAttemptWorkflowSectionDisplayName;
            this.SortOrder = sortOrder;
            this.HasCompletionStatus = hasCompletionStatus;
            this.GisUploadAttemptWorkflowSectionGroupingID = gisUploadAttemptWorkflowSectionGroupingID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GisUploadAttemptWorkflowSection(string gisUploadAttemptWorkflowSectionName, string gisUploadAttemptWorkflowSectionDisplayName, int sortOrder, bool hasCompletionStatus, int gisUploadAttemptWorkflowSectionGroupingID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GisUploadAttemptWorkflowSectionID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GisUploadAttemptWorkflowSectionName = gisUploadAttemptWorkflowSectionName;
            this.GisUploadAttemptWorkflowSectionDisplayName = gisUploadAttemptWorkflowSectionDisplayName;
            this.SortOrder = sortOrder;
            this.HasCompletionStatus = hasCompletionStatus;
            this.GisUploadAttemptWorkflowSectionGroupingID = gisUploadAttemptWorkflowSectionGroupingID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GisUploadAttemptWorkflowSection(string gisUploadAttemptWorkflowSectionName, string gisUploadAttemptWorkflowSectionDisplayName, int sortOrder, bool hasCompletionStatus, GisUploadAttemptWorkflowSectionGrouping gisUploadAttemptWorkflowSectionGrouping) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GisUploadAttemptWorkflowSectionID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GisUploadAttemptWorkflowSectionName = gisUploadAttemptWorkflowSectionName;
            this.GisUploadAttemptWorkflowSectionDisplayName = gisUploadAttemptWorkflowSectionDisplayName;
            this.SortOrder = sortOrder;
            this.HasCompletionStatus = hasCompletionStatus;
            this.GisUploadAttemptWorkflowSectionGroupingID = gisUploadAttemptWorkflowSectionGrouping.GisUploadAttemptWorkflowSectionGroupingID;
            this.GisUploadAttemptWorkflowSectionGrouping = gisUploadAttemptWorkflowSectionGrouping;
            gisUploadAttemptWorkflowSectionGrouping.GisUploadAttemptWorkflowSections.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GisUploadAttemptWorkflowSection CreateNewBlank(GisUploadAttemptWorkflowSectionGrouping gisUploadAttemptWorkflowSectionGrouping)
        {
            return new GisUploadAttemptWorkflowSection(default(string), default(string), default(int), default(bool), gisUploadAttemptWorkflowSectionGrouping);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GisUploadAttemptWorkflowSection).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GisUploadAttemptWorkflowSections.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GisUploadAttemptWorkflowSectionID { get; set; }
        public string GisUploadAttemptWorkflowSectionName { get; set; }
        public string GisUploadAttemptWorkflowSectionDisplayName { get; set; }
        public int SortOrder { get; set; }
        public bool HasCompletionStatus { get; set; }
        public int GisUploadAttemptWorkflowSectionGroupingID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GisUploadAttemptWorkflowSectionID; } set { GisUploadAttemptWorkflowSectionID = value; } }

        public virtual GisUploadAttemptWorkflowSectionGrouping GisUploadAttemptWorkflowSectionGrouping { get; set; }

        public static class FieldLengths
        {
            public const int GisUploadAttemptWorkflowSectionName = 50;
            public const int GisUploadAttemptWorkflowSectionDisplayName = 50;
        }
    }
}